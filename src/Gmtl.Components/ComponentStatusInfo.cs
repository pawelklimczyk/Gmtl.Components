using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Gmtl.Components
{
    public class ComponentStatusInfo
    {
        private Dictionary<string, object> _componentInfo = new Dictionary<string, object>();
        private Dictionary<string, ComponentStatusInfo> _childComponents = new Dictionary<string, ComponentStatusInfo>();
        public ComponentStatus Status { get; set; }

        public ReadOnlyDictionary<string, object> ComponentInfo
        {
            get
            {
                if (_componentInfo != null && _componentInfo.Count > 0)
                    return new ReadOnlyDictionary<string, object>(_componentInfo);

                return null;
            }
        }

        public ReadOnlyDictionary<string, ComponentStatusInfo> ChildComponentsInfo
        {
            get
            {
                if (_childComponents != null && _childComponents.Count > 0)
                    return new ReadOnlyDictionary<string, ComponentStatusInfo>(_childComponents);

                return null;
            }
        }
        public DateTime LastUpdate { get; private set; } = DateTime.Now;

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

        public ComponentStatusInfo AddChildInfo(string childComponentKey, ComponentStatusInfo childInfo)
        {
            if (_childComponents.ContainsKey(childComponentKey)) { return this; }

            _childComponents.Add(childComponentKey, childInfo);

            LastUpdate = DateTime.Now;

            return this;
        }
    }

}
