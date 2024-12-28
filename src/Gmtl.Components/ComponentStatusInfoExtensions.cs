using Gmtl.HandyLib.Operations;

namespace Gmtl.Components
{
    public static class ComponentStatusInfoExtensions
    {
        public static ComponentStatusInfo ToComponentStatusInfo(this OperationResult operationResult)
        {
            if (operationResult.IsSuccess)
            {
                return new ComponentStatusInfo { Status = ComponentStatus.Active };
            }
            else
            {
                return new ComponentStatusInfo { Status = ComponentStatus.Error }.AddInfo("Error", operationResult.Message);
            }
        }
    }
}
