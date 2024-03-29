﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ASBDDS.Shared.Models.Database.DataDb;
using GitHub.JPMikkers.DHCP;
using Microsoft.Extensions.Configuration;

namespace ASBDDS.API.Servers.DHCP
{
    public static class DHCPServerHelper
    {
        private static List<OptionItem> CreateDHCPOptions(IConfiguration configuration)
        {
            var dhcpServerIdentifierStr = configuration.GetValue<string>("Networks:Devices:DHCP:ServerIdentifier");
            var dnetGatewayStr = configuration.GetValue<string>("Networks:Devices:DHCP:Gateway");
            var dnetMaskStr = configuration.GetValue<string>("Networks:Devices:DHCP:Mask");
            var dnetBroadcastAddressStr = configuration.GetValue<string>("Networks:Devices:DHCP:Broadcast");
            var dnetDNSArrStr = configuration.GetSection("Networks:Devices:DHCP:DNS").Get<string[]>();
            var dnetDNSArr = new List<IPAddress>();
            foreach(var dnsStr in dnetDNSArrStr)
                dnetDNSArr.Add(IPAddress.Parse(dnsStr));
            
            var dhcpServerIdentifier = IPAddress.Parse(dhcpServerIdentifierStr);
            var dnetGateway =  IPAddress.Parse(dnetGatewayStr);
            var dnetMask =  IPAddress.Parse(dnetMaskStr);
            var dnetBroadcastAddress = IPAddress.Parse(dnetBroadcastAddressStr);
            
            
            var dhcpOptions = new List<OptionItem>();
            dhcpOptions.Add(new OptionItem(mode: OptionMode.Force,
                option: new DHCPOptionRouter()
                {
                    IPAddresses = new[] { dnetGateway }
                }));

            dhcpOptions.Add(new OptionItem(mode: OptionMode.Force,
                option: new DHCPOptionServerIdentifier(dhcpServerIdentifier)));

            dhcpOptions.Add(new OptionItem(mode: OptionMode.Force,
                option: new DHCPOptionTFTPServerName(dhcpServerIdentifierStr)));

            dhcpOptions.Add(new OptionItem(mode: OptionMode.Force,
                option: new DHCPOptionHostName("ASBDDS")));
            
            dhcpOptions.Add(new OptionItem(OptionMode.Default, 
                new DHCPOptionSubnetMask(dnetMask)));
            
            dhcpOptions.Add(new OptionItem(OptionMode.Default, 
                new DHCPOptionBroadcastAddress(dnetBroadcastAddress)));
            
            dhcpOptions.Add(new OptionItem(OptionMode.Default, 
                new DHCPOptionDomainNameServer(){ IPAddresses = dnetDNSArr} ));

            return dhcpOptions;
        }
        public static DHCPServer Create(IConfiguration configuration)
        {
            var dnetIpStr = configuration.GetValue<string>("Networks:Devices:IP");
            var dnetPoolStr = configuration.GetValue<string>("Networks:Devices:DHCP:pool");
            IPAddress dnetIp = IPAddress.Any;
            if(!string.IsNullOrEmpty(dnetIpStr))
                dnetIp = IPAddress.Parse(dnetIpStr);

            var pool = new DHCPPool(dnetPoolStr);
            var leasesManager = new DHCPLeasesManager(pool, TimeSpan.FromDays(7));
            var options = CreateDHCPOptions(configuration);
            return new DHCPServer(dnetIp, 67, leasesManager, options);

        }
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var server = serviceProvider.GetRequiredService<DHCPServer>();
            var context = serviceProvider.GetRequiredService<DataDbContext>();
            var savedLeases = new List<DHCPLease>();
            foreach(var lease in context.DHCPLeases.ToList())
                savedLeases.Add(new DHCPLease()
                {
                    Address = IPAddress.Parse(lease.Address), 
                    MacAddress = lease.MacAddress, 
                    Static = lease.Static, 
                    LeaseTime = server.LeasesManager.LeaseTime
                });
            server.LeasesManager.LoadSavedLeases(savedLeases);
            DHCPServerEventHandler.Init(context, server);
            server.Start();
        }
    }
}
