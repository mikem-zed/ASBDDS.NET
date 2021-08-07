using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using ASBDDS.API.Models.Utils;
using GitHub.JPMikkers.DHCP;

using DHCPJPMikkers = GitHub.JPMikkers.DHCP;

namespace ASBDDS.API.Servers.DHCP
{
    public class DHCPServer : DHCPJPMikkers.DHCPServer
    {
        public IPAddress BindAddress { get; set; }
        public string HTTPBootFile { get; set; }

        public DHCPServer(string address) : this(address, "255.255.255.0", 67) { }
        public DHCPServer(string address, string mask, string poolStart, string poolEnd, int port) : this(address, mask, port) 
        {
            this.PoolStart = IPAddress.Parse(poolStart);
            this.PoolEnd = IPAddress.Parse(poolEnd);
        }
        public DHCPServer(string address, string mask, int port) : base(null)
        {
            BindAddress = IPAddress.Parse(address);

            var net = new IPSegment(BindAddress.ToString(), mask);

            this.EndPoint = new IPEndPoint(BindAddress, port);
            this.SubnetMask = IPAddress.Parse(mask);
            this.PoolStart = net.Hosts().First().ToIpAddress();
            this.PoolEnd = net.Hosts().Last().ToIpAddress();
            this.LeaseTime = Utils.InfiniteTimeSpan;
            this.OfferExpirationTime = TimeSpan.FromSeconds(30);

            this.MinimumPacketSize = 576;


            this.OnStatusChange += Dhcpd_OnStatusChange;
            this.OnTrace += Dhcpd_OnTrace;

            Options.Add(new OptionItem(mode: OptionMode.Force,
                option: new DHCPOptionRouter()
                {
                    IPAddresses = new[] { BindAddress }
                }));

            Options.Add(new DHCPJPMikkers.OptionItem(mode: DHCPJPMikkers.OptionMode.Force,
               option: new DHCPJPMikkers.DHCPOptionServerIdentifier(BindAddress)));

            Options.Add(new DHCPJPMikkers.OptionItem(mode: DHCPJPMikkers.OptionMode.Force,
              option: new DHCPJPMikkers.DHCPOptionTFTPServerName(BindAddress.ToString())));

            Options.Add(new DHCPJPMikkers.OptionItem(mode: DHCPJPMikkers.OptionMode.Force,
                 option: new DHCPJPMikkers.DHCPOptionHostName("ASBDDS")));

        }
        private void Dhcpd_OnStatusChange(object sender, DHCPJPMikkers.DHCPStopEventArgs e)
        {
            Trace.WriteLine(e?.Reason);
            Trace.Flush();
        }
        private void Dhcpd_OnTrace(object sender, DHCPJPMikkers.DHCPTraceEventArgs e)
        {
            Trace.WriteLine(e?.Message);
            Trace.Flush();
        }
        public new void Start()
        {
            base.Start();
        }
    }
}
