using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Gmtl.Components
{
    public class ComponentStatusInfo
    {
        public ComponentStatus Status { get; set; }
        public Dictionary<string, object> _componentInfo = new Dictionary<string, object>();
        public ReadOnlyDictionary<string, object> ComponentInfo { get => new ReadOnlyDictionary<string, object>(_componentInfo); }
        public DateTime LastUpdate { get; set; }

        public ComponentStatusInfo AddInfo(string key, object value)
        {
            if (_componentInfo.ContainsKey(key))
            {
                _componentInfo[key] = value;
            }
            else
            {
                _componentInfo.Add(key, value);
            }

            LastUpdate = DateTime.Now;

            return this;
        }

        public ComponentStatusInfo MergeOtherInfo(string prefixKey, ComponentStatusInfo childInfo)
        {
            foreach (var kvp in childInfo._componentInfo)
            {
                _componentInfo.Add($"{prefixKey}_{kvp.Key}", kvp.Value);
            }

            LastUpdate = DateTime.Now;

            return this;
        }
    }
}
