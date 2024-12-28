using Gmtl.HandyLib.Operations;

namespace Gmtl.Components
{
    public static class ComponentStatusInfoExtensions
    {
        public static ComponentStatusInfo ToComponentStatusInfo(this OperationResult operationResult)
        {
            return new ComponentStatusInfo
            {
                Status = operationResult.IsSuccess ? ComponentStatus.Active : ComponentStatus.Error
            }.AddInfo("Message", operationResult.Message);
        }
    }
}
