using Gmtl.HandyLib.Operations;
using System;
using System.Text;

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

        public string AsHtml(bool isInternalHtml = false)
        {
            var status = GetComponentStatusInfo();

            return AsHtmlInternal(status, isInternalHtml);
        }

        private string AsHtmlInternal(ComponentStatusInfo status, bool isInternalHtml = false)
        {
            var builder = new StringBuilder();

            if (!isInternalHtml)
            {
                builder.AppendLine("<table class=\"api-component-info\">");
            }
            var statusBgColor = "";
            if (status.Status == ComponentStatus.Error) { statusBgColor = "#ff0000"; }

            builder.AppendLine($"<tr><td colspan=\"2\" class=\"api-component-info-head\" style=\"background-color:{statusBgColor};\">{status.Status} - {status.LastUpdate:yyyy-MM-dd HH:mm}</td></tr>");

            foreach (var info in status.ComponentInfo)
            {
                builder.AppendLine($"<tr><td>{info.Key}</td><td>{info.Value}</td></tr>");
            }

            if (status.ChildComponentsInfo?.Count > 0)
            {
                foreach (var component in status.ChildComponentsInfo)
                {
                    builder.AppendLine($"<tr><td colspan=\"2\" class=\"api-component-info-child\">{component.Key}</td></tr>");
                    builder.AppendLine(AsHtmlInternal(component.Value, true));
                }
            }

            if (!isInternalHtml)
            {
                builder.AppendLine("</table>");
            }

            return builder.ToString();
        }
    }
}