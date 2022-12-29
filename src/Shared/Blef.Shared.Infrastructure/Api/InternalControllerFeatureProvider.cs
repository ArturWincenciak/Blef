using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Blef.Shared.Infrastructure.Api;

internal sealed class InternalControllerFeatureProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        if (false == typeInfo.IsClass)
            return false;

        if (typeInfo.IsAbstract)
            return false;

        if (typeInfo.ContainsGenericParameters)
            return false;

        if (typeInfo.IsDefined(typeof(NonControllerAttribute)))
            return false;

        if (false == typeInfo.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase) &&
            false == typeInfo.IsDefined(typeof(ControllerAttribute)))
            return false;

        return true;
    }
}