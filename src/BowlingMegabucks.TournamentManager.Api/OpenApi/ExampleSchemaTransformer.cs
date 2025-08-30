using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace BowlingMegabucks.TournamentManager.Api.OpenApi;

/// <summary>
/// Schema transformer that extracts examples from XML documentation comments and adds them to OpenAPI schemas.
/// </summary>
[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Instantiated by dependency injection container.")]
internal sealed class ExampleSchemaTransformer
    : IOpenApiSchemaTransformer
{
    private static readonly Dictionary<string, XDocument> s_xmlDocCache = [];

    /// <summary>
    /// Transforms the OpenAPI schema by adding examples from XML documentation.
    /// </summary>
    /// <param name="schema">The schema to transform.</param>
    /// <param name="context">The transformation context.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A completed task.</returns>
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        if (context.JsonTypeInfo.Type is not null && schema is IDictionary<string, object> schemaDict)
        {
            AddExamplesToSchema(schemaDict, context.JsonTypeInfo.Type);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Adds examples from XML documentation to the OpenAPI schema.
    /// </summary>
    /// <param name="schema">The OpenAPI schema dictionary to modify.</param>
    /// <param name="type">The type to extract examples from.</param>
    private static void AddExamplesToSchema(IDictionary<string, object> schema, Type type)
    {
        XDocument? xmlDoc = GetXmlDocumentation(type.Assembly);
        if (xmlDoc is null) return;

        // Add examples to properties
        if (schema.TryGetValue("properties", out object? propertiesObj) &&
            propertiesObj is IDictionary<string, object> properties)
        {
            foreach (PropertyInfo property in type.GetProperties())
            {
                string memberName = $"P:{type.FullName}.{property.Name}";
                string? example = GetExampleFromXml(xmlDoc, memberName);

                if (!string.IsNullOrEmpty(example) &&
                    properties.TryGetValue(GetJsonPropertyName(property), out object? propertySchemaObj) &&
                    propertySchemaObj is IDictionary<string, object> propertySchema)
                {
                    propertySchema["example"] = ConvertExampleToJsonValue(example, property.PropertyType);
                }
            }
        }
    }

    /// <summary>
    /// Gets the JSON property name for a property, considering JsonPropertyName attributes.
    /// </summary>
    /// <param name="property">The property to get the name for.</param>
    /// <returns>The JSON property name.</returns>
    private static string GetJsonPropertyName(PropertyInfo property)
    {
        // Check for JsonPropertyName attribute
        JsonPropertyNameAttribute? jsonPropertyNameAttr = property.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
        if (jsonPropertyNameAttr is not null)
        {
            return jsonPropertyNameAttr.Name;
        }

        // Default to camelCase property name
        return char.ToLowerInvariant(property.Name[0]) + property.Name[1..];
    }

    /// <summary>
    /// Converts an example string to an appropriate JSON value based on the target type.
    /// </summary>
    /// <param name="example">The example string from XML documentation.</param>
    /// <param name="targetType">The target type to convert the example to.</param>
    /// <returns>A JSON-compatible value.</returns>
    private static object ConvertExampleToJsonValue(string example, Type targetType)
    {
        try
        {
            if (targetType == typeof(string))
                return example;

            if (targetType == typeof(int) && int.TryParse(example, CultureInfo.InvariantCulture, out int intValue))
                return intValue;

            if (targetType == typeof(decimal) && decimal.TryParse(example, CultureInfo.InvariantCulture, out decimal decimalValue))
                return decimalValue;

            if (targetType == typeof(bool) && bool.TryParse(example, out bool boolValue))
                return boolValue;

            if (targetType == typeof(Guid) && Guid.TryParse(example, out Guid guidValue))
                return guidValue.ToString();

            if (targetType == typeof(DateOnly) && DateOnly.TryParse(example, CultureInfo.InvariantCulture, out DateOnly dateValue))
                return dateValue.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (targetType == typeof(DateTime) && DateTime.TryParse(example, CultureInfo.InvariantCulture, out DateTime dateTimeValue))
                return dateTimeValue.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);

            // Default to string representation
            return example;
        }
        catch (ArgumentException)
        {
            // If conversion fails, return as string
            return example;
        }
        catch (FormatException)
        {
            // If conversion fails, return as string
            return example;
        }
        catch (OverflowException)
        {
            // If conversion fails, return as string
            return example;
        }
    }

    /// <summary>
    /// Gets the XML documentation for an assembly.
    /// </summary>
    /// <param name="assembly">The assembly to get documentation for.</param>
    /// <returns>The XML documentation, or null if not found.</returns>
    private static XDocument? GetXmlDocumentation(Assembly assembly)
    {
        string? assemblyName = assembly.GetName().Name;
        if (string.IsNullOrEmpty(assemblyName)) return null;

        if (s_xmlDocCache.TryGetValue(assemblyName, out XDocument? cachedDoc))
            return cachedDoc;

        try
        {
            string xmlPath = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml");
            if (!File.Exists(xmlPath)) return null;

            var doc = XDocument.Load(xmlPath);
            s_xmlDocCache[assemblyName] = doc;
            return doc;
        }
        catch (FileNotFoundException)
        {
            return null;
        }
        catch (DirectoryNotFoundException)
        {
            return null;
        }
        catch (UnauthorizedAccessException)
        {
            return null;
        }
        catch (System.Xml.XmlException)
        {
            return null;
        }
    }

    /// <summary>
    /// Extracts the example value from XML documentation for a specific member.
    /// </summary>
    /// <param name="xmlDoc">The XML documentation.</param>
    /// <param name="memberName">The member name to look for.</param>
    /// <returns>The example value, or null if not found.</returns>
    private static string? GetExampleFromXml(XDocument xmlDoc, string memberName)
    {
        try
        {
            XElement? member = xmlDoc.Descendants("member")
                .FirstOrDefault(x => string.Equals(x.Attribute("name")?.Value, memberName, StringComparison.Ordinal));

            return member?.Element("example")?.Value?.Trim();
        }
        catch (ArgumentNullException)
        {
            return null;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }
}
