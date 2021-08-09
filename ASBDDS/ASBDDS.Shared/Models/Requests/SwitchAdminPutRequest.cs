using ASBDDS.Shared.Models.Database.DataDb;
using ASBDDS.Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Requests
{
    public class SwitchAdminPutRequest
    {
        /// <summary>
        /// Switch model
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Switch manufacturer
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// Switch serial
        /// </summary>
        public string Serial { get; set; }
        /// <summary>
        /// Switch name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Switch IP address
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// User name for logging in switch
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Password for logging in switch
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Switch access protocol
        /// </summary>
        public SwitchAccessProtocol AccessProtocol { get; set; }
        /// <summary>
        /// Switch authentification method
        /// </summary>
        public SwitchAuthMethod AuthMethod { get; set; }
        /// <summary>
        /// List of ports on the switch
        /// </summary>
        public virtual List<SwitchPortAdminResponse> Ports { get; set; }
    }
}
