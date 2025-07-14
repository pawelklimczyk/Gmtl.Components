﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Gmtl.Components
{
    public class ComponentStatusInfo
    {
        private readonly Dictionary<string, object> _componentInfo = new Dictionary<string, object>();
        private readonly Dictionary<string, ComponentStatusInfo> _childComponents = new Dictionary<string, ComponentStatusInfo>();
        public ComponentStatus Status { get; set; }

        public ReadOnlyDictionary<string, object> ComponentInfo
        {
            get
            {
                return new ReadOnlyDictionary<string, object>(_componentInfo);
            }
        }

        public ReadOnlyDictionary<string, ComponentStatusInfo> ChildComponentsInfo
        {
            get
            {
                return new ReadOnlyDictionary<string, ComponentStatusInfo>(_childComponents);
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
