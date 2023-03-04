using Gmtl.HandyLib.Operations;
using System;

namespace Gmtl.Components.Web
{
    public abstract class ApiComponent : IApiComponent
    {
        public virtual string ComponentName => $"Component {GetType().FullName}";

        public virtual ComponentStatusInfo GetComponentStatusInfo()
        {
            throw new NotImplementedException("Implement in inherited class!");
        }

        public virtual OperationResult CheckConfiguration()
        {
            throw new NotImplementedException("Implement in inherited class!");
        }

        public virtual OperationResult HealthCheck()
        {
            throw new NotImplementedException("Implement in inherited class!");
        }
    }
}