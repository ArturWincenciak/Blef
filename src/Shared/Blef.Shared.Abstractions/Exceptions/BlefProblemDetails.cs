using Microsoft.AspNetCore.Mvc;

namespace Blef.Shared.Abstractions.Exceptions;

public class BlefProblemDetails : ValidationProblemDetails
{
    public BlefProblemDetails WithTraceId(string traceId)
    {
        Extensions.Add("traceId", traceId);
        return this;
    }

    public BlefProblemDetails WithActivityId(string activityId)
    {
        Extensions.Add("activityId", activityId);
        return this;
    }

    public BlefProblemDetails WithError(string code, string[] values)
    {
        Errors.Add(code, values);
        return this;
    }

    public BlefProblemDetails WithErrors(IDictionary<string, string[]> errors)
    {
        foreach (var error in errors)
        {
            Errors.Add(error.Key, error.Value);
        }

        return this;
    }
}