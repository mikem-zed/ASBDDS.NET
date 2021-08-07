using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASBDDS.Shared.Helpers
{
    public class DeviceManufacturer
    {
        public DeviceManufacturers Enum { get; set; }
        public string Name { get; set; }
        public DeviceManufacturer(DeviceManufacturers manufacturer, string name)
        {
            Enum = manufacturer;
            Name = name;
        }

    }
    public class DeviceModel
    {
        public DeviceManufacturer Manufacturer { get; set; }
        public DeviceModels Enum { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// We save firmware and early bootloaders for base device model
        /// </summary>
        public string SystemBaseModel { get; set; }

        public DeviceModel(DeviceManufacturer manufacturer, DeviceModels model, string name, string systemBaseModel)
        {
            Manufacturer = manufacturer;
            Enum = model;
            Name = name;
            SystemBaseModel = systemBaseModel;
        }
    }

    public class DeviceHelper
    {
        private List<DeviceModel> devicesModels { get; }
        public DeviceHelper()
        {
            var raspberry = new DeviceManufacturer(DeviceManufacturers.RASPBERRY, "Raspberry");
            devicesModels = new List<DeviceModel>();
            devicesModels.Add(new DeviceModel(raspberry, DeviceModels.PI4_MODEL_B_2GB, "Pi 4 Model B 2GB", "rpi4"));
            devicesModels.Add(new DeviceModel(raspberry, DeviceModels.PI4_MODEL_B_4GB, "Pi 4 Model B 4GB", "rpi4"));
            devicesModels.Add(new DeviceModel(raspberry, DeviceModels.PI4_MODEL_B_8GB, "Pi 4 Model B 8GB", "rpi4"));
        }

        public DeviceManufacturers? GetManufacturer(DeviceModels model)
        {
            return devicesModels.FirstOrDefault(m => m.Enum == model)?.Manufacturer.Enum;
        }

        public DeviceManufacturers? GetManufacturer(string manufacturerName)
        {
            return devicesModels.FirstOrDefault(m => m.Manufacturer.Name == manufacturerName)?.Manufacturer.Enum;
        }

        public string GetManufacturer(DeviceManufacturers manufacturer)
        {
            return devicesModels.FirstOrDefault(m => m.Manufacturer.Enum == manufacturer)?.Manufacturer.Name;
        }

        public DeviceModels[] GetModels(DeviceManufacturers manufacturer)
        {
            return devicesModels.Where(m => m.Manufacturer.Enum == manufacturer).Select(m => m.Enum).ToArray();
        }
        public DeviceModels[] GetModels(string manufacturerName)
        {
            return devicesModels.Where(m => m.Manufacturer.Name == manufacturerName).Select(m => m.Enum).ToArray();
        }

        public DeviceModels? GetModel(string name)
        {
            return devicesModels.FirstOrDefault(m => m.Name == name)?.Enum;
        }

        public string GetModel(DeviceModels model)
        {
            return devicesModels.FirstOrDefault(m => m.Enum == model)?.Name;
        }

        public string GetSystemBaseModel(DeviceModels model)
        {
            return devicesModels.FirstOrDefault(m => m.Enum == model)?.SystemBaseModel;
        }
        public string GetSystemBaseModel(string modelName)
        {
            return devicesModels.FirstOrDefault(m => m.Name == modelName)?.SystemBaseModel;
        }

        public DeviceManufacturers[] GetManufacturers()
        {
            return devicesModels.Select(m => m.Manufacturer.Enum).Distinct().ToArray();
        }
    }

    public enum DeviceManufacturers
    {
        RASPBERRY = 1
    }

    public enum DeviceModels
    {
        PI4_MODEL_B_2GB = 1,
        PI4_MODEL_B_4GB,
        PI4_MODEL_B_8GB,
    }
}
