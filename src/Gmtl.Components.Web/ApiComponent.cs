using Gmtl.HandyLib.Operations;
using System;
using System.Collections;
using System.Linq;
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

        private string AsHtmlInternal(ComponentStatusInfo status, bool isInternalHtml = false, string componentName = "Main")
        {
            var builder = new StringBuilder();
            var cssKey = string.Empty;

            if (!isInternalHtml)
            {
                builder.AppendLine("<table class=\"api-component-info\">");
                cssKey = "head";
            }
            else
            {
                cssKey = "child";
            }

            var statusBgColor = "#ffffff";
            if (status.Status == ComponentStatus.Error) { statusBgColor = "#ff0000"; }

            builder.AppendLine($"<tr><td colspan=\"2\" class=\"api-component-info-{cssKey}\" style=\"background-color:{statusBgColor};\">{componentName}<br />{status.Status} - {status.LastUpdate:yyyy-MM-dd HH:mm}</td></tr>");

            foreach (var info in status.ComponentInfo)
            {
                builder.AppendLine($"<tr><td class=\"api-component-info-{cssKey}-item\">{info.Key}</td><td>{FormatValueWithHtml(info.Value)}</td></tr>");
            }

            if (status.ChildComponentsInfo?.Count > 0)
            {
                foreach (var component in status.ChildComponentsInfo)
                {
                    builder.AppendLine(AsHtmlInternal(component.Value, true, component.Key));
                }
            }

            if (!isInternalHtml)
            {
                builder.AppendLine("</table>");
            }

            return builder.ToString();
        }

        protected string FormatValueWithHtml(object value)
        {
            if (value == null) return string.Empty;

            var builder = new StringBuilder();

            if (value is string str)
            {
                builder.Append(str);
            }
            else if (value is IEnumerable enumerable)
            {
                var items = enumerable.Cast<object>().Select(o => FormatValueWithHtml(o).ToString());
                builder.Append(string.Join("<br />", items));
            }
            else
            {
                builder.Append(value?.ToString());
            }

            return builder.ToString();
        }
    }
}