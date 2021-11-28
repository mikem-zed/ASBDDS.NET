﻿using System;

namespace ASBDDS.Shared.Dtos.OperationSystem
{
    public class OperationSystemDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// OS Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// OS Version
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// OS Arch
        /// </summary>
        public string Arch { get; set; }
        /// <summary>
        /// Options in JSON format
        /// Objects array { "Name": "OpName", "Value": "OpValue" }
        /// </summary>
        public string Options { get; set; }

        public string FullName => Name + "-" + Version + "-" + Arch;
    }
}