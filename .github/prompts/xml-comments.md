# XML Comments Prompt Instructions

When asked to add or update XML comments in C# code, always follow these guidelines, based on Microsoft's official recommendations and ASP.NET Core OpenAPI integration:

## General Principles

- All publicly visible types and their public members should be documented with XML comments.
- At a minimum, use the `<summary>` tag for every type and member.
- Write documentation text in complete sentences ending with full stops.
- Documentation must be well-formed XML.

## OpenAPI XML Documentation Support in ASP.NET Core

- ASP.NET Core automatically extracts XML documentation comments to populate OpenAPI (Swagger) docs if `<GenerateDocumentationFile>true</GenerateDocumentationFile>` is set in the project file.
- XML comments are detected in the application assembly and referenced assemblies with XML documentation enabled.
- No extra code is needed—just ensure XML docs are generated and the OpenAPI/Swagger package is used.

## Supported XML Tags for OpenAPI

The following tags are supported and recommended for API documentation:

- `<summary>`: Required. Describes the type or member. Displayed in IntelliSense and OpenAPI docs.
- `<remarks>`: Optional. Adds supplemental information, longer explanations, or examples. Can include `<para>`, `<code>`, and other formatting tags.
- `<param name="name">`: Describes a method parameter. Required for each parameter in methods.
- `<returns>`: Describes the return value of a method.
- `<response code="XXX">`: Documents HTTP responses for endpoints (e.g., `<response code="404">Not found</response>`).
- `<example>`: Provides a usage or value example. For OpenAPI, use for types, properties, and methods.
- `<deprecated>`: Marks a member as deprecated.
- `<exception cref="ExceptionType">`: Documents exceptions that may be thrown.
- `<value>`: Describes the value a property represents.
- `<inheritdoc>`: Inherit documentation from base/interface. **For interface implementations, use only `<inheritdoc />` and do not duplicate documentation.**
- `<include>`: Include documentation from an external XML file.
- `<see cref="TypeOrMember">`, `<seealso cref="TypeOrMember">`: Reference other code elements.
- `<see href="url">`, `<seealso href="url">`: Reference external URLs.
- `<para>`, `<list>`, `<item>`, `<listheader>`, `<langword>`, `<code>`, `<b>`, `<i>`, `<u>`, `<br/>`, `<a>`: For formatting and structure. **Use `<langword>` for C# language keywords instead of `<c>`.**
- `<typeparam name="T">`, `<typeparamref name="T">`: For generics.

## Best Practices for API Documentation

- Use `<example>` tags for types, properties, and methods to improve OpenAPI/Swagger output.
- For models, include a JSON example in `<remarks>` using `<code language="json">`.
- If a property is optional or nullable, note this in the `<summary>`.
- Use `<exception>` tags for methods that can throw exceptions.
- Use `<param>` and `<returns>` for all methods.
- Use `<response>` tags for endpoints to document HTTP status codes.
- Use `<inheritdoc>` for overrides and interface implementations. **For interface implementations, use only `<inheritdoc />` and do not add other tags.**
- Use `<see>` and `<seealso>` for cross-references.

## Example

```csharp
/// <summary>
/// Represents the result of a single health check, including its status, description, and execution duration.
/// </summary>
/// <remarks>
/// <para>Example:</para>
/// <code language="json">
/// {
///   "status": "Healthy",
///   "description": "Database connection is healthy.",
///   "duration": "00:00:00.1234567"
/// }
/// </code>
/// </remarks>
public sealed record HealthCheckDetail
{
    /// <summary>
    /// The status of the health check (e.g., <langword>Healthy</langword>, <langword>Unhealthy</langword>).
    /// </summary>
    /// <example>Healthy</example>
    public required string Status { get; init; }

    /// <summary>
    /// A description providing additional details about the health check result.
    /// </summary>
    /// <example>Database connection is healthy.</example>
    public required string? Description { get; init; }

    /// <summary>
    /// The time taken to execute the health check.
    /// </summary>
    /// <example>00:00:00.1234567</example>
    public required TimeSpan Duration { get; init; }
}
```

## Additional Notes

- Always use well-formed XML.
- Use `<paramref>`, `<typeparamref>`, and `cref` attributes for references.
- Use `<inheritdoc>` to avoid duplicating documentation for overrides and interface implementations. **For interface implementations, use only `<inheritdoc />`.**
- ASP.NET Core OpenAPI integration automatically includes XML comments from referenced projects with XML docs enabled.
- Use `<response>` and `<example>` tags to maximize OpenAPI/Swagger discoverability.

Always apply these standards unless otherwise specified.
