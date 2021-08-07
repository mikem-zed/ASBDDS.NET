using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASBDDS.Shared.Helpers
{
    public class SwitchManufacturer
    {
        public SwitchManufacturers Enum { get; set; }
        public string Name { get; set; }
        public SwitchManufacturer(SwitchManufacturers manufacturer, string name)
        {
            Enum = manufacturer;
            Name = name;
        }

    }
    public class SwitchModel
    {
        public SwitchManufacturer Manufacturer { get; set; }
        public SwitchModels Enum { get; set; }
        public string Name { get; set; }

        public SwitchModel(SwitchManufacturer manufacturer, SwitchModels model, string name)
        {
            Manufacturer = manufacturer;
            Enum = model;
            Name = name;
        }
    }

    public class SwitchHelper
    {
        private SwitchManufacturer ubiquiti = new SwitchManufacturer(SwitchManufacturers.UBIQUITI, "Ubiquiti");
        private List<SwitchModel> switchModels { get; }
        public SwitchHelper()
        {
            switchModels = new List<SwitchModel>();
            switchModels.Add(new SwitchModel(ubiquiti, SwitchModels.UNIFI_SWITCH_US_24_250W, "UniFi Switch US-24-250W"));
        }

        public SwitchManufacturers? GetManufacturer(SwitchModels model)
        {
            return switchModels.FirstOrDefault(m => m.Enum == model)?.Manufacturer.Enum;
        }

        public SwitchManufacturers? GetManufacturer(string manufacturerName)
        {
            return switchModels.FirstOrDefault(m => m.Manufacturer.Name == manufacturerName)?.Manufacturer.Enum;
        }

        public string GetManufacturer(SwitchManufacturers manufacturer)
        {
            return switchModels.FirstOrDefault(m => m.Manufacturer.Enum == manufacturer)?.Manufacturer.Name;
        }

        public SwitchModels[] GetModels(SwitchManufacturers manufacturer)
        {
            return switchModels.Where(m => m.Manufacturer.Enum == manufacturer).Select(m => m.Enum).ToArray();
        }
        public SwitchModels[] GetModels(string manufacturerName)
        {
            return switchModels.Where(m => m.Manufacturer.Name == manufacturerName).Select(m => m.Enum).ToArray();
        }

        public SwitchModels? GetModel(string name)
        {
            return switchModels.FirstOrDefault(m => m.Name == name)?.Enum;
        }

        public string GetModel(SwitchModels model)
        {
            return switchModels.FirstOrDefault(m => m.Enum == model)?.Name;
        }
        public SwitchManufacturers[] GetManufacturers()
        {
            return switchModels.Select(m => m.Manufacturer.Enum).Distinct().ToArray();
        }
    }

    public enum SwitchManufacturers
    {
        UBIQUITI = 1
    }

    public enum SwitchModels
    {
        UNIFI_SWITCH_US_24_250W = 1
    }
}
