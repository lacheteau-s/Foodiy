using Foodiy.Api.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;

namespace Foodiy.Api.UnitTests.Configuration;

public class RouteConventionTests
{
    private readonly RouteConvention _target;
    private const string _prefix = "test";

    private class TestController { };

    public RouteConventionTests()
    {
        _target = new RouteConvention(new RouteAttribute(_prefix));
    }

    [Fact]
    public void Constructor_WithNullParameter_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => new RouteAttribute(null!));
    }

    [Fact]
    public void Apply_WithNullParameter_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => _target.Apply(null!));
    }

    [Fact]
    public void Apply_WithExistingRoute_AddsPrefix()
    {
        var applicationModel = CreateApplicationModel(new AttributeRouteModel { Template = "some/route" });

        _target.Apply(applicationModel);

        var actualRouteModel = applicationModel.Controllers.Single().Selectors.Single().AttributeRouteModel;

        Assert.NotNull(actualRouteModel);
        Assert.Equal($"{_prefix}/some/route", actualRouteModel!.Template);
    }

    [Fact]
    public void Apply_WithoutExistingRoute_PrefixOnly()
    {
        var applicationModel = CreateApplicationModel(null);

        _target.Apply(applicationModel);

        var actualRouteModel = applicationModel.Controllers.Single().Selectors.Single().AttributeRouteModel;

        Assert.NotNull(actualRouteModel);
        Assert.Equal(_prefix, actualRouteModel!.Template);
    }

    public static ApplicationModel CreateApplicationModel(AttributeRouteModel? routeModel)
    {
        var applicationModel = new ApplicationModel();
        var controllerModel = new ControllerModel(typeof(TestController).GetTypeInfo(), Array.Empty<object>());
        var selector = new SelectorModel { AttributeRouteModel = routeModel };

        controllerModel.Selectors.Add(selector);
        applicationModel.Controllers.Add(controllerModel);

        return applicationModel;
    }
}
