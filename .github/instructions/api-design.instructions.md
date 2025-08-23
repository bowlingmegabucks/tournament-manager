# API Design Guidelines for Single-Module Application

This document standardizes API surface design for a single-module .NET application following Clean Architecture and Domain-Driven Design principles. It enforces consistency for route design, DTO structure, pagination, status codes, and OpenAPI usage.

## Role Definition

You are building HTTP APIs for a .NET application following Clean Architecture and DDD within a single module. Follow RESTful design principles, returning only DTOs (never domain models), and follow consistent conventions for routing, versioning, error handling, and response shape. Use ProblemDetails for all errors, wrap all search results in a paged response, and expose comprehensive OpenAPI metadata. Favor idempotent operations where applicable.

## Route & HTTP Standards

### Route Format

- Use RESTful routes, version via path:

```http
GET /v1/bowlers/123
POST /v1/tournaments
DELETE /v1/locations/456
```

- Use kebab-case for route segments (industry standard).
- Avoid RPC-style routes (/get-by-id, /doAction).

### HTTP Methods

- GET: Read resource(s)
- POST: Create resource
- PUT: Replace resource (idempotent)
- DELETE: Remove resource
- PATCH: Not used

## Request & Response DTOs

- All APIs must use explicit request/response DTOs.
- Never return domain models
- Keep DTOs as anemic as possible, allow hierarchy only where beneficial to consumers.
- Search or paged results must return a PagedResult<T> object.

    ```csharp
    public class PagedResult<T>
    {
        public IReadOnlyList<T> Items { get; init; }
        public int Page { get; init; }
        public int Size { get; init; }
        public int TotalCount { get; init; }
    }
    ```

## Error Handling

- All error responses must follow [RFC 7807](https://datatracker.ietf.org/doc/html/rfc7807) using `ProblemDetails`.

### Standard Extensions

```json
{
    "type": "https://httpstatuses.com/400",
    "title": "Validation Error",
    "status": 400,
    "detail": "One or more validation failures occurred.",
    "instance": "/v1/bowlers",
    "extensions": {
        "errors": {
            "FirstName": ["First name is required."],
            "Email": ["Email must be a valid address."]
        },
        "traceId": "abc123",
        "correlationId": "xyz789",
        "errorCode": "BOWLER_VALIDATION_ERROR"
    }
}
```

## Filtering, Pagination, and Sorting

### Pagination

- Use page and size query parameters

```http
GET /v1/bowlers?page=1&size=20
```

- Wrap results in `PagedResult<T>` as shown above.

### Sorting

- Support `sort=name,-createdAt`
- Default sort order should be explicitly documented per endpoint

### Filtering
- Filtering criteria must be passed in the **request body** (as part of the query command)
- This supports complex filters and avoid URL bloat

## Status Codes

- `200 OK`: Successful query or mutation with return data
- `201 Created`: New resource created; return Location header and response body with identifier and metadata
- `204 No Content`: Successful mutation with no return payload
- `400 Bad Request`: Validation errors or malformed request
- `401 Unauthorized`: Authentication required
- `403 Forbidden`: Authorization failure
- `404 Not Found`: Resource not found
- `409 Conflict`: Resource conflict or business rule violation
- `429 Too Many Requests`: Rate limiting exceeded
- `500 Internal Server Error`: Unhandled server error

## HATEOAS and Hypermedia

- All applicable API responses must include HATEOAS links to guide consumers through valid next actions.
- Hypermedia links should be included using a `_links` property in the response DTO, following HAL-style structure:

```json
{
  "id": "bwl_01HXTK0J2FJH72YXBABR7B1ZNT",
  "firstName": "John",
  "lastName": "Doe",
  "_links": {
    "self": {
      "href": "/v1/bowlers/bwl_01HXTK0J2FJH72YXBABR7B1ZNT",
      "method": "GET"
    },
    "update": {
      "href": "/v1/bowlers/bwl_01HXTK0J2FJH72YXBABR7B1ZNT",
      "method": "PUT"
    },
    "delete": {
      "href": "/v1/bowlers/bwl_01HXTK0J2FJH72YXBABR7B1ZNT",
      "method": "DELETE"
    }
  }
}
```

### Implementation Guidelines

- The `_links` section should be part of all single-resource DTOs and optionally included in each item of a collection.
- Use a small helper or interface (e.g., `ILinkProvider`) to generate route-safe links for the application.
- Each link must include:
    - `href`: relative path to the route
    - `method`: HTTP method used for the action
    - Optional: `rel`, `title`, or `type` for further metadata

### Link Types to Include

- `self`: Link to the resource itself
- `update`: Link to update the resource (if editable)
- `delete`: Link to delete the resource (if deletable)
- `create`: Link to create a new resource in the same collection
- `related`: Links to related resources (e.g., a bowler's tournaments)

Links should reflect the user's authorization and visibility.  Do not include links to actions a user is not permitted to perform.

## Idempotency

- `GET`, `PUT`, `DELETE` must be idempotent
- Support optional `Idempotency-Key` header for retry protection on `PUT`, `DELETE`, etc.
- `POST` operations do not require idempotency unless documented explicitly

## OpenAPI

- Use Microsoft's OpenAPI library
- The application must:
    - Register OpenAPI endpoint metadata for all routes
    - Group endpoints using tags based on domain context (tournaments, bowlers, scores, etc.)
    - Ensure route descriptions and summaries are included
- Expose a comprehensive OpenAPI document covering all API endpoints


These standards ensure uniformity across all HTTP endpoints in the system.  Follow this document whenever implementing or reviewing API contracts.  Deviations must be justified and documented.
