﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace fastfood_products.API.HealthCheck;

public static class HealthCheckResponseWriter
{
    public static Task WriteResponse(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json; charset=utf-8";

        JsonWriterOptions options = new() { Indented = true };

        using Utf8JsonWriter json = new(context.Response.Body, options);
        json.WriteStartObject();
        json.WriteString("status", report.Status.ToString().ToLowerInvariant());
        json.WriteStartObject("results");

        foreach (KeyValuePair<string, HealthReportEntry> result in report.Entries)
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
