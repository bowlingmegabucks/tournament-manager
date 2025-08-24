# XML Comments Prompt Instructions

When asked to add or update XML comments in C# code, always follow these guidelines, based on Microsoft's official recommendations for API documentation:

## General Principles
- All publicly visible types and their public members should be documented with XML comments.
- At a minimum, use the `<summary>` tag for every type and member.
- Write documentation text in complete sentences ending with full stops.
- Documentation must be well-formed XML.

## Recommended XML Tags

### General Tags
- `<summary>`: Required. Describes the type or member. Displayed in IntelliSense.
- `<remarks>`: Optional. Adds supplemental information, longer explanations, or examples. Can include `<para>`, `<code>`, and other formatting tags.

### Member-Specific Tags
- `<param name="name">`: Describes a method parameter. Required for each parameter in methods.
- `<returns>`: Describes the return value of a method.
- `<exception cref="ExceptionType">`: Documents exceptions that may be thrown.
- `<value>`: Describes the value a property represents.
- `<example>`: Provides a usage or value example. For OpenAPI, use for properties and methods.

### Formatting and Structure
- `<para>`: For paragraphs within `<summary>`, `<remarks>`, or `<returns>`.
- `<list>`, `<item>`, `<listheader>`: For lists or tables.
- `<c>`: Inline code.
- `<code>`: Multi-line code or JSON examples.
- `<b>`, `<i>`, `<u>`, `<br/>`, `<a>`: For formatting and links.

### Reuse and References
- `<inheritdoc>`: Inherit documentation from base/interface.
- `<include>`: Include documentation from an external XML file.
- `<see cref="TypeOrMember">`, `<seealso cref="TypeOrMember">`: Reference other code elements.
- `<see href="url">`, `<seealso href="url">`: Reference external URLs.

### Generics
- `<typeparam name="T">`: Describes a generic type parameter.
- `<typeparamref name="T">`: Refers to a generic type parameter in text.

## Best Practices for API Documentation
- Use `<example>` tags for properties and methods to improve OpenAPI/Swagger output.
- For models, include a JSON example in `<remarks>` using `<code language="json">`.
- If a property is optional or nullable, note this in the `<summary>`.
- Use `<exception>` tags for methods that can throw exceptions.
- Use `<param>` and `<returns>` for all methods.
- Use `<inheritdoc>` for overrides and interface implementations when appropriate.
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
    /// The status of the health check (e.g., "Healthy", "Unhealthy").
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
- Use `<inheritdoc>` to avoid duplicating documentation for overrides and interface implementations.

Always apply these standards unless otherwise specified.
