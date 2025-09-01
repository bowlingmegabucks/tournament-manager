# Create API documentation for this endpoint

## Instructions

You are creating comprehensive API documentation for a REST endpoint in the tournament manager application. Before generating the documentation, ask the user to confirm or provide any missing details about the endpoint to ensure accuracy and completeness.

After gathering all necessary information, generate markdown documentation that includes all the information a developer would need to successfully call and integrate with this endpoint.

## Confirmation Process

Before generating the documentation, ask the user to confirm or clarify:

### Endpoint Details

- HTTP method (GET, POST, PUT, DELETE, PATCH)
- Complete URL path including any path parameters
- Purpose and business function of the endpoint

### Request Specifications

- Required vs optional query parameters
- Request body structure (for POST/PUT operations)
- Content-Type requirements
- Any special validation rules or constraints

### Response Specifications

- Success response structure and status codes
- Error scenarios and their corresponding status codes
- Any pagination or filtering capabilities

### Security & Business Rules

- Authentication/authorization requirements
- Rate limiting policies
- Idempotency behavior (for POST/PUT/DELETE)
- Any domain-specific business rules or constraints

### Additional Context

- Related endpoints or workflows
- Performance considerations
- Special notes for developers

Only proceed with documentation generation after receiving confirmation or clarification on these points.

## Documentation Structure

The documentation should include the following sections:

### Endpoint Overview

- HTTP method and URL path
- Brief description of what the endpoint does
- Primary use case or purpose

### Authentication & Authorization

- Authentication requirements (if any)
- Required permissions or roles
- Authorization header format

### Request Details

#### URL Parameters

- Path parameters (if any) with descriptions and examples
- Query parameters (if any) with descriptions, types, required/optional status

#### Request Headers

- Required headers (Content-Type, Authorization, etc.)
- Optional headers with descriptions

#### Request Body

- Schema/structure of the request payload (if applicable)
- Required vs optional fields
- Data types and validation rules
- Example request body in JSON format

### Response Details

#### Success Response

- HTTP status code(s) for successful requests
- Response body schema/structure
- Description of returned fields
- Example success response in JSON format

#### Error Responses

- Common error status codes (400, 401, 403, 404, 422, 500)
- Error response format (following ProblemDetails RFC 7807)
- Example error responses for different scenarios

### Code Examples

- cURL example showing a complete request
- Optional: examples in other languages (C#, JavaScript, etc.)

### Notes & Considerations

- Rate limiting information (if applicable)
- Idempotency behavior
- Performance considerations
- Any special business rules or constraints
- Related endpoints or workflows

## Formatting Guidelines

- Use heading levels: `##` for main sections, `###` for subsections, and avoid `#` (H1) headings.
- Use fenced code blocks for all code and JSON examples. Specify the language for syntax highlighting (e.g., `json`, `bash`).
- Use markdown tables for structured data (parameters, headers, etc.), with proper alignment and no tab characters.
- Limit line length to 80 characters for readability. Use soft line breaks for long paragraphs.
- Use blank lines to separate sections and improve readability. Avoid excessive whitespace.
- Include realistic example data that matches the tournament domain.
- Follow the API design standards from the project's instruction files.
- Ensure all examples are valid and properly formatted JSON.

## Domain Context

Remember this is for a bowling tournament management system that handles:

- Tournaments and tournament registration
- Bowlers and their information
- Squads (groups of bowlers competing together)
- Scores and scoring systems
- Divisions (different competition categories)
- Lane assignments
- Sweepers (side competitions)

Generate documentation that is clear, comprehensive, and developer-friendly while following REST API best practices and the project's established conventions.
