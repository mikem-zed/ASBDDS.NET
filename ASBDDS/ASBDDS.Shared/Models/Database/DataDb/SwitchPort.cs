using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ASBDDS.Shared.Models.Database.DataDb
{
    public enum SwitchPortType
    {
        RJ45,
        RJ45_POE,
        RJ45_POEPLUS,
    }

    public class SwitchPort
    {
        public SwitchPort()
        {
            Id = new Guid();
        }

        public Guid Id { get; set; }
        public string Number { get; set; }
        public SwitchPortType Type { get; set; }
        public virtual Switch Switch { get; set; }
    }
}
