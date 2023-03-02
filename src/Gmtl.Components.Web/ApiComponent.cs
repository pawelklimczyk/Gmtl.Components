using Gmtl.HandyLib.Operations;
using System;

namespace Gmtl.Components.Web
{
    public interface IApiComponent
    {
        string Info { get; }
        OperationResult CheckConfiguration();
        OperationResult HealthCheck();
    }

    public abstract class ApiComponent : IApiComponent
    {
        public virtual string Info => throw new NotImplementedException("Implement in inherited class!");

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