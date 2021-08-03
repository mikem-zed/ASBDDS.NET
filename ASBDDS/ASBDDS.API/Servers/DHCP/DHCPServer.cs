using System;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

using Singulink.Net.Dhcp;

namespace ASBDDS.API.Servers.DHCP
{
    //public class DHCPServer : Singulink.Net.Dhcp.DhcpServer
    //{
    //    private static readonly IPAddress SubnetMask = IPAddress.Parse("255.255.0.0");

    //    // Your implementation of mappings between IP addresses and MAC addresses - could be
    //    // an in-memory dictionary, database lookup, or some other custom mechanism of 
    //    // mapping values:
    //    private IPAddressMap _clientMap = new IPAddressMap();

    //    private readonly object _syncRoot = new object();

    //    public event Action<PhysicalAddress> DiscoverReceived = delegate { };
    //    public event Action Disconnected = delegate { };

    //    public DHCPServer(IPAddress listeningAddress) : base(listeningAddress, SubnetMask) { }

    //    public override void Start()
    //    {
    //        base.Start();
    //    }

    //    public override void Stop()
    //    {
    //        base.Stop();
    //    }

    //    public PhysicalAddress? GetMacAddress(IPAddress ip)
    //    {
    //        lock (_syncRoot)
    //        {
    //            if (_clientMap.TryGetValue(ip, out PhysicalAddress mac))
    //                return mac;
    //        }

    //        return null;
    //    }

    //    protected override DhcpDiscoverResult? OnDiscoverReceived(DhcpMessage message)
    //    {
    //        IPAddress ip;
    //        PhysicalAddress mac = message.ClientMacAddress;

    //        lock (_syncRoot)
    //        {
    //            if (!_clientMap.TryGetValue(mac, out ip))
    //            {
    //                try
    //                {
    //                    ip = _clientMap.GetNextAvailableIPAddress();
    //                }
    //                catch
    //                {
    //                    //_log.Error("No more IP addresses available.");
    //                    return null;
    //                }

    //                _clientMap[mac] = ip;
    //            }
    //        }

    //        DiscoverReceived.Invoke(mac);
    //        return DhcpDiscoverResult.CreateOffer(message, ip, uint.MaxValue);
    //    }

    //    protected override DhcpRequestResult? OnRequestReceived(DhcpMessage message)
    //    {
    //        //_log.Debug(message);

    //        var ip = message.Options.RequestedIPAddress;
    //        var mac = message.ClientMacAddress;

    //        if (ip == null)
    //            return DhcpRequestResult.CreateNoAcknowledgement(message, "No requested IP address provided");

    //        lock (_syncRoot)
    //        {
    //            if (!_clientMap.Contains(mac, ip))
    //                return DhcpRequestResult.CreateNoAcknowledgement(message, "No matching offer found.");
    //        }

    //        return DhcpRequestResult.CreateAcknowledgement(message, ip, uint.MaxValue);
    //    }

    //    protected override void OnReleaseReceived(DhcpMessage message)
    //    {
    //        //_log.Debug(message);
    //    }

    //    protected override void OnDeclineReceived(DhcpMessage message)
    //    {
    //        //_log.Debug(message);

    //        var ip = message.Options.RequestedIPAddress;
    //        var mac = message.ClientMacAddress;

    //        if (ip != null)
    //        {
    //            //_log.DebugFormat("Purge requested for client record: {0} / {1}.", mac, ip);

    //            lock (_syncRoot)
    //            {
    //                if (_clientMap.Remove(mac, ip))
    //                {
    //                    //_log.DebugFormat("Purged client record: {0} / {1}.", mac, ip);
    //                }
    //            }
    //        }
    //    }

    //    protected override void OnInformReceived(DhcpMessage message)
    //    {
    //        //_log.Debug(message);
    //    }

    //    protected override void OnResponseSent(DhcpMessage message)
    //    {
    //        //_log.Debug(message);
    //    }

    //    protected override void OnMessageError(Exception ex)
    //    {
    //        //_log.Error("Bad message recieved.", ex);
    //    }

    //    protected override void OnSocketError(SocketException ex)
    //    {
    //        //_log.Error("Socket error.", ex);
    //        Disconnected.Invoke();
    //    }
    //}
}
