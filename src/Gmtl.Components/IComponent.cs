using Gmtl.HandyLib.Operations;
using System;
using System.Collections.Generic;

namespace Gmtl.Components
{
    public interface IComponent
    {
        string ComponentName { get; }
        OperationResult CheckConfiguration();
        ComponentStatusInfo GetComponentStatusInfo();
    }

    public class ComponentStatusInfo
    {
        public ComponentStatus Status { get; set; }
        public Dictionary<string, string> ComponentInfo { get; set; }
        public DateTime LastUpdate { get; set; }

        public ComponentStatusInfo MergeOtherInfo(string prefixKey, ComponentStatusInfo childInfo)
        {
            if (ComponentInfo == null) { ComponentInfo = new Dictionary<string, string>(); }

            foreach (var kvp in childInfo.ComponentInfo)
            {
                ComponentInfo.Add($"{prefixKey}_{kvp.Key}", kvp.Value);
            }

            return this;
        }
    }

    public enum ComponentStatus
    {
        Unknown = 0,
        Initializing = 1,
        Active = 100,
        Error = 1000
    }
}
