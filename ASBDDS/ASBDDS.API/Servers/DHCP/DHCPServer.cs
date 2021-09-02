using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using ASBDDS.API.Models.Utils;
using ASBDDS.Shared.Models.Database.DataDb;
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
        public IPAddress BindAddress { get; }

        public DHCPServer(string host, int port, IDHCPLeasesManager leasesManager, DHCPPool pool) : base(IPAddress.Parse(host), port, leasesManager, pool)
        {
            BindAddress = IPAddress.Parse(host);
            MinimumPacketSize = 576;
            LeaseTime = TimeSpan.FromMinutes(5);
            leasesManager.LeaseTime = LeaseTime;

            OnStatusChange += _OnStatusChange;
            OnTrace += _OnTrace;

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

        protected override void ProcessingReceiveMessage(DHCPMessage sourceMsg, DHCPMessage targetMsg)
        {
            var arch = sourceMsg.GetArch();
            var bootMode = sourceMsg.GetBootMode();
            var clientMac = "";
            foreach (var macPart in sourceMsg.ClientHardwareAddress)
                clientMac += (clientMac.Length > 1 ? "-" : null) + macPart.ToString("X").ToLower();

            var bootFile = clientMac + "/";
            switch (bootMode)
            {
                case BootMode.PXE:
                    switch (arch)
                    {
                        case "aarch64":
                            bootFile += "uboot.bin";
                            break;
                    }
                    break;
                case BootMode.IPXE:
                    break;
                case BootMode.HTTP:
                    break;
                case BootMode.OTHER:
                    bootFile += "uboot.bin";
                    break;
            }

            targetMsg.BootFileName = bootFile;
            targetMsg.NextServerIPAddress = BindAddress;
        }

        public new void Start()
        {
            base.Start();
        }
    }

    public static class DHCPMessageExtensions
    {
        // https://www.iana.org/assignments/dhcpv6-parameters/dhcpv6-parameters.xhtml#processor-architecture 
        private static string[] archTypes = {
            "x86 BIOS",
            "NEC/PC98 (DEPRECATED)",
            "Itanium",
            "DEC Alpha (DEPRECATED)",
            "Arc x86 (DEPRECATED)",
            "Intel Lean Client (DEPRECATED)",
            "x86 UEFI",
            "x64 UEFI",
            "EFI Xscale (DEPRECATED)",
            "EBC",
            "ARM 32-bit UEFI",
            "ARM 64-bit UEFI",
            "PowerPC Open Firmware",
            "PowerPC ePAPR",
            "POWER OPAL v3",
            "x86 uefi boot from http",
            "x64 uefi boot from http",
            "ebc boot from http",
            "arm uefi 32 boot from http",
            "arm uefi 64 boot from http",
            "pc/at bios boot from http",
            "arm 32 uboot",
            "arm 64 uboot",
            "arm uboot 32 boot from http",
            "arm uboot 64 boot from http",
            "RISC-V 32-bit UEFI",
            "RISC-V 32-bit UEFI boot from http",
            "RISC-V 64-bit UEFI",
            "RISC-V 64-bit UEFI boot from http",
            "RISC-V 128-bit UEFI",
            "RISC-V 128-bit UEFI boot from http",
            "s390 Basic",
            "s390 Extended",
        };

        public static string GetArch(this DHCPMessage message)
        {
            byte archByte = 0;
            try
            {

                archByte = message.Options
                    .Where(x => x.OptionType == TDHCPOption.ClientSystemArchitectureType)
                    .Cast<DHCPOptionGeneric>()
                    .Select(x => x.Data[1])
                    .First();
            }
            catch {}

            // Rpi4 bootloader not set ClientSystemArchitectureType DHCP request option, but we have 97 DHCP option for determinate arch, model, serial and mac address.
            if (archByte == 0)
            {
                var clientIdentifier = message.Options
                    .Where(x => x.OptionType == (TDHCPOption)97)
                    .Cast<DHCPOptionGeneric>().FirstOrDefault();
                if (clientIdentifier != null)
                {
                    var asciiClientIdentifier = Encoding.ASCII.GetString(clientIdentifier.Data);
                    if (asciiClientIdentifier.Contains("rpi", StringComparison.OrdinalIgnoreCase))
                        archByte = 0x16; // arm 64 uboot
                }
            }
            var archType = archTypes[archByte];
            var arch = "amd64";

            if (archType.Contains("arm", StringComparison.OrdinalIgnoreCase))
            {
                if (archType.Contains("64", StringComparison.OrdinalIgnoreCase))
                    arch = "aarch64";
                else
                    arch = "arm";
            }

            return arch;
        }

        public static string GetVendorClass(this DHCPMessage message)
        {
            var sb = new StringBuilder();
            var strings = message.Options
             .Where(x => x.OptionType == TDHCPOption.VendorClassIdentifier)
             .Cast<DHCPOptionVendorClassIdentifier>()
             .Select(x => Encoding.ASCII.GetString(x.Data));

            try
            {
                foreach (var s in strings)
                {
                    sb.AppendLine(s);
                }
            }
            catch { }
            return sb.ToString();
        }

        public static BootMode GetBootMode(this DHCPMessage message)
        {
            BootMode bootMode;
            
            if (IsPXE(message))
                bootMode = BootMode.PXE;
            else if (IsIPXE(message))
                bootMode = BootMode.IPXE;
            else if (IsHTTP(message))
                bootMode = BootMode.HTTP;
            else
                bootMode = BootMode.OTHER;

            return bootMode;
        }
        
        public static bool IsHTTP(this DHCPMessage message) => GetVendorClass(message).Contains("HTTPClient");
        public static bool IsPXE(this DHCPMessage message) => GetVendorClass(message).Contains("PXEClient");
        public static bool IsIPXE(this DHCPMessage message)
        {
            try
            {
                return message.Options
                    .Where(x => x.OptionType == (TDHCPOption)77)
                    .Cast<DHCPOptionGeneric>()
                    .Select(x => Encoding.ASCII.GetString(x.Data))
                    .Any(x => x.Equals("iPXE", StringComparison.InvariantCultureIgnoreCase));
            }
            catch { return false; }
        }
    }
}
