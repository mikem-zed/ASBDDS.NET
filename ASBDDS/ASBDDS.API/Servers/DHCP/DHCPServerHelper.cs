using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using GitHub.JPMikkers.DHCP;
using Microsoft.Extensions.Configuration;

namespace ASBDDS.API.Servers.DHCP
{
    public static class DHCPServerHelper
    {
        private static List<OptionItem> CreateDHCPOptions(IConfiguration configuration)
        {
            var dnetIpStr = configuration.GetValue<string>("Networks:Devices:IP");
            var dnetGatewayStr = configuration.GetValue<string>("Networks:Devices:DHCP:gateway");
            var dnetMaskStr = configuration.GetValue<string>("Networks:Devices:DHCP:mask");
            var dnetDNSArrStr = configuration.GetSection("Networks:Devices:DHCP:dns").Get<string[]>();
            var dnetDNSArr = new List<IPAddress>();
            foreach(var dnsStr in dnetDNSArrStr)
                dnetDNSArr.Add(IPAddress.Parse(dnsStr));
            
            var dnetIp = IPAddress.Parse(dnetIpStr);
            var dnetGateway =  IPAddress.Parse(dnetGatewayStr);
            var dnetMask =  IPAddress.Parse(dnetMaskStr);
            
            
            var dhcpOptions = new List<OptionItem>();
            dhcpOptions.Add(new OptionItem(mode: OptionMode.Force,
                option: new DHCPOptionRouter()
                {
                    IPAddresses = new[] { dnetGateway }
                }));

            dhcpOptions.Add(new OptionItem(mode: OptionMode.Force,
                option: new DHCPOptionServerIdentifier(dnetIp)));

            dhcpOptions.Add(new OptionItem(mode: OptionMode.Force,
                option: new DHCPOptionTFTPServerName(dnetIpStr)));

            dhcpOptions.Add(new OptionItem(mode: OptionMode.Force,
                option: new DHCPOptionHostName("ASBDDS")));
            
            dhcpOptions.Add(new OptionItem(OptionMode.Default, 
                new DHCPOptionSubnetMask(dnetMask)));
            
            dhcpOptions.Add(new OptionItem(OptionMode.Default, 
                new DHCPOptionDomainNameServer(){ IPAddresses = dnetDNSArr} ));

            return dhcpOptions;
        }
        public static DHCPServer Create(IConfiguration configuration)
        {
            var dnetIpStr = configuration.GetValue<string>("Networks:Devices:IP");
            var dnetPoolStr = configuration.GetValue<string>("Networks:Devices:DHCP:pool");
            var dnetIp = IPAddress.Parse(dnetIpStr);

            var pool = new DHCPPool(dnetPoolStr);
            var leasesManager = new DHCPLeasesManager(pool, TimeSpan.FromDays(1));
            var options = CreateDHCPOptions(configuration);
            return new DHCPServer(dnetIp, 67, leasesManager, options);

        }
        public static void Init(IServiceProvider _serviceProvider)
        {
            var server = _serviceProvider.GetRequiredService<DHCPServer>();
            server.LeasesManager.OnAdd += (sender, lease) => Console.WriteLine(DateTime.Now + ": Added lease: " + lease.MacAddress + " State: " + lease.Status +
                                                                               " IP:" + lease.Address.ToString());
            server.LeasesManager.OnRemove += (sender, lease) => Console.WriteLine(DateTime.Now + ": Remove lease: " + lease.MacAddress + " State: " + lease.Status +
                " IP:" + lease.Address.ToString());
            server.LeasesManager.OnLeaseChange += (sender, lease) =>
                Console.WriteLine(DateTime.Now + ": Changes lease: " + lease.MacAddress + " State: " + lease.Status +
                                  " IP:" + lease.Address.ToString());
        }
    }
}
