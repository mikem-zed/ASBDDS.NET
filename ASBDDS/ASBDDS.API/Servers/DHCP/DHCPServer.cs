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
    public enum BootMode
    {
        PXE,
        IPXE,
        HTTP,
        OTHER = 99
    }

    public class DHCPServer : DHCPJPMikkers.DHCPServer
    {
        public DHCPServer(IPAddress host, int port, IDHCPLeasesManager leasesManager, List<OptionItem> options) : base(host, port, leasesManager, options)
        {
            bindAddress = host;
            OnStatusChange += _OnStatusChange;
            OnTrace += _OnTrace;
        }

        private IPAddress bindAddress;
        private static void _OnStatusChange(object sender, DHCPJPMikkers.DHCPStopEventArgs e)
        {
            Trace.WriteLine(e?.Reason);
            Trace.Flush();
        }
        private static void _OnTrace(object sender, DHCPJPMikkers.DHCPTraceEventArgs e)
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
