using Gmtl.HandyLib.Operations;

namespace Gmtl.Components.Web
{
    public interface IApiComponent : IComponent
    {
        OperationResult HealthCheck();
    }
}