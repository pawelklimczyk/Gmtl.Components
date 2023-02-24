using Gmtl.HandyLib.Operations;
using System;

namespace Gmtl.Components
{
    public interface IApiComponent
    {
        OperationResult CheckConfiguration();
        OperationResult HealthCheck();
    }

    public abstract class ApiComponent : IApiComponent
    {
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