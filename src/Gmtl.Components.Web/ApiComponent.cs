using Gmtl.HandyLib.Operations;
using System;

namespace Gmtl.Components.Web
{
    public abstract class ApiComponent : IApiComponent
    {
        public virtual string[] ComponentInfo => throw new NotImplementedException("Implement in inherited class!");

        public virtual string ComponentName => throw new NotImplementedException();

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