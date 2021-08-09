using ASBDDS.Shared.Models.Database.DataDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBDDS.Shared.Models.Responses
{
    public class SwitchPortAdminResponse
    {
        public SwitchPortAdminResponse()
        {

        }
        public SwitchPortAdminResponse(SwitchPort port)
        {
            Id = port.Id;
            Number = port.Number;
            Type = port.Type;
        }
        /// <summary>
        /// Switch port ID
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// Switch port number
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// Switch port type
        /// </summary>
        public SwitchPortType Type { get; set; }
    }
    
    public class SwitchAdminResponse
    {
        /// <summary>
        /// Switch ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Switch name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Switch model
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Switch manufacturer
        /// </summary>
        public string Manufacturer { get; set; }
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
        /// Switch serial number
        /// </summary>
        public string Serial { get; set; }
        /// <summary>
        /// Switch IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// Router ID
        /// </summary>
        public virtual Guid? RouterId { get; set; }
        /// <summary>
        /// List of switch ports
        /// </summary>
        public virtual List<SwitchPortAdminResponse> Ports { get; set; }

        public SwitchAdminResponse() { }

        public SwitchAdminResponse(Switch _switch)
        {
            Id = _switch.Id;
            Name = _switch.Name;
            Serial = _switch.Serial;
            Ip = _switch.Ip;
            RouterId = _switch.RouterGuid;
            Model = _switch.Model;
            Manufacturer = _switch.Manufacturer;
            AuthMethod = _switch.AuthMethod;
            AccessProtocol = _switch.AccessProtocol;
            Username = _switch.Username;
            Password = "**********";

            Ports = new List<SwitchPortAdminResponse>();
            foreach(var port in _switch.Ports)
            {
                Ports.Add(new SwitchPortAdminResponse(port));
            }
        }
    }
}
