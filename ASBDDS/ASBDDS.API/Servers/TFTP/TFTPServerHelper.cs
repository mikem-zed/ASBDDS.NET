using Microsoft.Extensions.DependencyInjection;
using System;

namespace ASBDDS.API.Servers.TFTP
{
    public class TFTPServerHelper
    {
        public static void Initialize(IServiceProvider _serviceProvider)
        {
            var server = _serviceProvider.GetRequiredService<TFTPServer>();
            server.Start();
        }
    }
}
