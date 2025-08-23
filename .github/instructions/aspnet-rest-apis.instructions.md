# ASP.NET REST API Design Guidelines

## Role Definition

You are building and maintaining HTTP APIs using ASP.NET Core Minimal APIs for a single-module .NET application. These guidelines ensure consistency, testability, discoverability, and evolvability across APIs, enforcing RESTful principles, versioning strategy, structured error responses, and consistent DTO modeling.

## Minimal API-First Approach

- Use ASP.NET Core Minimal APIs as the default.
- Define each endpoint in a dedicated file within the module's `Endpoints` project.
- Register endpoints via internal extension methods (e.g., `MapBowlerEndpoints(this IEndpointRouteBuilder builder)`).
- Controllers are discouraged.

## RESTful Routing Conventions

- Use `resource-oriented URLs`: `GET /v1/bowlers/{id}`
- Routes should be **kebab-case** (e.g., `v1/tournament-results`).
- HTTP Verbs:
    - `GET` for retrieval
    - `POST` for creation
    - `PUT` for updates
    - `DELETE` for deletion
    - `PATCH` is not supported in this system.

## Versioning Strategy

- Use path-based versioning (e.g., `/v1/bowlers`)
- Each versioned route group should use `.MapGroup()`
- Versioning is per endpoint group, not per entire application

## DTO and Response Conventions

- Never expose domain models via the API.
- Use anemic DTOs:
  - Flat structure where possible
  - Hierarchy only if necessary (e.g., nested aggregates)
- Response DTOs should never return Entity DTO directly.  There should be a wrapper around the Entity DTO to allow for additional metadata or links.
- Command responses:
    - POST --> return 201 Created + payload (e.g. id)
    - PUT or DELETE --> return 204 No Content
- Query responses:
    - Single result --> 200 OK or 404 Not Found
    - Multiple results --> 200 OK with `PagedResult<T>`
    - PagedResult Format

    ```json
    {
        "items": [{ /* T */}],
        "totalCount": 100,
        "page": 1,
        "pageSize": 10
    }
    ```
    - `page` and `size` are passed as query string parameters (e.g., `?page=1&size=10`)
    - Filtering is passed in the request body
    - Sorting is passed as a comma-separated list in the query string (e.g., `?sort=name,-age`)

## Error Handling

- All error responses MUST conform to __RFC 7807__:
    - Content-Type: `application/problem+json`

Required ProblemDetails Extensions

```json
{
    "type": "https://httpstatuses.com/404",
    "title": "Not Found",
    "status": 404,
    "detail": "Bowler with ID '123' not found.",
    "instance": "/v1/bowlers/123",
    "errors": {
            "firstName": [
                {
                    "message": "First name is required.",
                    "code": "Bowler.FirstNameRequired"
                }
            ]
        },
    "correlationId": "123e4567-e89b-12d3-a456-426614174000"
}
```

- 404s must return structured problem details.
- All validation and business errors must provide:
    - A code in the format: Entity.PropertyRule (e.g., `Bowler.FirstNameRequired`).

## HATEOAS Support

- All single-resource responses should include `_links` following HAL-style structure:

```json
{
    "id": "123",
    "name": "John Doe",
    "_links": {
        "self": { "href": "/v1/bowlers/123" },
        "update": { "href": "/v1/bowlers/123", "method": "PUT" },
        "delete": { "href": "/v1/bowlers/123", "method": "DELETE" },
    }
}
```

- Avoid over-linking; include only the links needed for clients to navigate or take action.

## OpenAPI / Swagger Documentation

- Use `Microsoft.OpenAPI` libraries (not Swashbuckle)
- OpenAPI definitions should:
    - Group endpoints by module tag
    - Use consistent naming for routes, parameters, and schemas
    - Document all request models, response types, and HTTP status codes

## Idempotency

- `Create (POST)` is not required to be idempotent.
- `Update (PUT)` and `Delete (DELETE)` **must be idempotent**;
- Clients may pass an optional idempotency key header if future support is desired.
