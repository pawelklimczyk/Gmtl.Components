using Gmtl.HandyLib.Operations;

namespace Gmtl.Components
{
    public interface IComponent
    {
        string ComponentName { get; }
        string[] ComponentInfo { get; }
        OperationResult CheckConfiguration();
    }
}
