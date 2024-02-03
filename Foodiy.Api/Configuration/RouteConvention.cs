using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Foodiy.Api.Configuration;

public class RouteConvention : IApplicationModelConvention
{
    private readonly AttributeRouteModel _routePrefix;

    public RouteConvention(IRouteTemplateProvider routeTemplateProvider)
    {
        if (routeTemplateProvider == null)
            throw new ArgumentNullException(nameof(routeTemplateProvider));

        _routePrefix = new AttributeRouteModel(routeTemplateProvider);
    }

    public void Apply(ApplicationModel application)
    {
        if (application == null)
            throw new ArgumentNullException(nameof(application));

        foreach (var controller in  application.Controllers)
            foreach (var selector in controller.Selectors)
                selector.AttributeRouteModel = selector.AttributeRouteModel != null
                    ? AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel)
                    : _routePrefix;
    }
}
