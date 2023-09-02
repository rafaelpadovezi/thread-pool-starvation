using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.RegularExpressions;

namespace AspnetTemplate.Configuration;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
        options.DocumentFilter<RemoveVersionFromPath>();
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "Sample API",
            Version = description.ApiVersion.ToString()
        };
        return info;
    }

    private class RemoveVersionFromPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (swaggerDoc == null)
            {
                throw new ArgumentNullException(nameof(swaggerDoc));
            }

            var replacements = new OpenApiPaths();

            foreach (var (key, value) in swaggerDoc.Paths)
            {
                replacements.Add(Regex.Replace(key, "^/v\\d*/", "/"), value);
            }

            swaggerDoc.Paths = replacements;

            swaggerDoc.Servers = new List<OpenApiServer> { new() { Url = "/v" + swaggerDoc.Info.Version } };
        }
    }
}