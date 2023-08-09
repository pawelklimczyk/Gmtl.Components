using Gmtl.HandyLib.Operations;

namespace Gmtl.Components
{
    public interface IComponent
    {
        string ComponentName { get; }
        OperationResult CheckConfiguration();
        ComponentStatusInfo GetComponentStatusInfo();
    }

    public enum ComponentStatus
    {
        Unknown = 0,
        Initializing = 1,
        Active = 100,
        Error = 1000
    }
}
