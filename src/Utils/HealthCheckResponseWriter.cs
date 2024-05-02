using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Mime;
using System.Text.Json;

namespace fastfood_products.Utils;

public static class HealthCheckResponseWriter
{
    public static Task WriteResponse(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json; charset=utf-8";

        var options = new JsonWriterOptions { Indented = true };

        using var json = new Utf8JsonWriter(context.Response.Body, options);
        json.WriteStartObject();
        json.WriteString("status", report.Status.ToString().ToLowerInvariant());
        json.WriteStartObject("results");

        foreach (var result in report.Entries)
        {
            json.WriteStartObject(result.Key);
            json.WriteString("status", result.Value.Status.ToString().ToLowerInvariant());
            json.WriteString("description", result.Value.Description);
            json.WriteString("data", result.Value.Data.ToString());
            json.WriteEndObject();
        }

        json.WriteEndObject();
        json.WriteEndObject();
        json.Flush();

        return Task.CompletedTask;
    }
}
