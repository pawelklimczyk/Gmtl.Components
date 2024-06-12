Use ApiEndpoints.ApiInformationEndpoint to define endpoint name in application

## Sample


public class InfoController : Controller
{
    private readonly CustomComponent _component;

    public InfoController(CustomComponent component)
    {
        _component = component;
    }

    [HttpGet(ApiEndpoints.ApiInformationEndpoint)]
    public IActionResult Index() => new HLJsonResult(_component.GetComponentStatusInfo());
}

public class GeneralApiComponent : ApiComponent
{
    private readonly IEnumerable<IApiComponent> _apiComponents;

    public GeneralApiComponent(IEnumerable<IApiComponent> apiComponents)
    {
        _apiComponents = apiComponents;
    }

    public override ComponentStatusInfo GetComponentStatusInfo()
    {
        var statusInfo = new ComponentStatusInfo
        {
            Status = ComponentStatus.Active
        };

        statusInfo.AddInfo("AppTitle", HandyLib.HLSoftwareMetrics.Title);
        statusInfo.AddInfo("AppDescription", HandyLib.HLSoftwareMetrics.Description);
        statusInfo.AddInfo("AppVersion", HandyLib.HLSoftwareMetrics.Version);
        statusInfo.AddInfo("ASPNETCORE_ENVIRONMENT", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

        foreach (var service in _apiComponents)
            statusInfo.AddChildInfo(service.ComponentName, service.GetComponentStatusInfo());


        return statusInfo;
    }
}

In Builder add:

builder.Services.RegisterAllImplementationsOfIApiComponent(typeof(GeneralApiComponent));