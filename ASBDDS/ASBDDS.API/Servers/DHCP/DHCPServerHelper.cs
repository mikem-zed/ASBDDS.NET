using Microsoft.Extensions.DependencyInjection;
using System;

namespace ASBDDS.API.Servers.DHCP
{
    public static class DHCPServerHelper
    {
        public static void Initialize(IServiceProvider _serviceProvider)
        {
            var server = _serviceProvider.GetRequiredService<DHCPServer>();
            server.Start();
        }
    }
}
