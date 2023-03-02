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
        public string Info => throw new NotImplementedException("Implement in inherited class!");

        public OperationResult CheckConfiguration()
        {
            throw new NotImplementedException("Implement in inherited class!");
        }

        public OperationResult HealthCheck()
        {
            throw new NotImplementedException("Implement in inherited class!");
        }
    }
}