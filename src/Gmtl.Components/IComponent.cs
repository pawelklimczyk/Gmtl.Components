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
    }

    public enum ComponentStatus
    {
        Unknown = 0,
        Initializing = 1,
        Active = 100,
        Error = 1000
    }
}
