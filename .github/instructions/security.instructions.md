# Security Best Practices for Single-Module Application

This document outlines security policies and conventions for building and operating secure APIs and infrastructure in a single-module .NET application. These practices apply to both runtime behavior and build-time configuration.

## Role Definition

You are responsible for securing a .NET single-module application that uses ASP.NET Core Identity and HTTP APIs. Follow these guidelines to enforce authentication, authorization, data protection, auditing, and secure defaults across all application layers and environments.

## Authentication

- Authentication is based on JWT tokens issued using ASP.NET Core Identity.
- Tokens are validated at the API layer for:
    - Signature
    - Expiration
    - Issuer
- The application is responsible for validating token claims and scopes to authorize access.

## Authorization

- Authorization is enforced in the API layer only
- Use `[Authorize]` attributes with:
    - Domain-specific policies for feature-specific access
    - Global policies for admin-level access
- Policies may include role-based or claim-based conditions
- Application and Domain layers must remain authorization-agnostic

## Secrets Management

- Production Secrets:
    - Store in Azure Key Vault
    - Access via managed identity or environment-based secret references
- CI/CD Secrets:
    - Store in GitHub Secrets
- Local Development:
    - Use ASP.NET Core's User Secrets for sensitive config values
    - Local resources (e.g., PostgreSQL, Redis) are provisioned via Docker Compose
        - Connection strings stored in `appsettings.Development.json`
        - Exposed credentials are acceptable as they are for dev-only infrastructure

## HTTPS and Data Encryption

- All HTTP Traffic must be served over HTTPS only
    - Enforced via YARP and server configuration
- Sensitive fields (e.g., SSN, financial data) must be encrypted at rest in the database
    - Use field-level encryption (e.g. AES via ValueConverters or PostgreSQL pgcrypto extension)

## Audit Logging

- All auditable actions (e.g., create, update, delete) must write to an AuditTrail table
- Each audit entry must include:
    - User Id (from JWT claims)
    - Timestamp (UTC)
    - Operation type (e.g., UPDATE_BOWLER_ADDRESS)
    - Payload (JSON of attempted state change)

Auditing is the responsibility of the Application or Infrastructure layer depending on the context.

## Security Headers (Middleware)

To reduce surface area for browser-based attacks, the following headers must be set via middleware:

| Header                    | Description                                                       |
|---------------------------|-------------------------------------------------------------------|
| Strict-Transport-Security | Forces HTTPS in modern browsers                                  |
| X-Content-Type-Options    | Prevents MIME-sniffing attacks (`nosniff`)                        |
| Content-Security-Policy   | Controls what scripts/styles are allowed to load                 |
| X-Frame-Options           | Prevents clickjacking by disallowing framing                     |
| Referrer-Policy           | Limits referrer header information to reduce data leakage        |
| Permissions-Policy        | Controls browser feature access (camera, geolocation, etc.)      |

These headers should be injected by a shared cross-cutting middleware in the API host.

## Rate Limiting and Abuse Prevention

- Rate limiting is enforced at the API level
    - Limit per IP, per token, or route
    - Use sliding window or token bucket strategy (configurable)
- Suspicious activity (e.g., brute force attempts) must be:
    - Logged in application logs (with correlation Id)
    - Throttled or blocked with exponential backoff or circuit breaker

## CORS Policy

- CORS configuration is environment-dependent
    - Development: Allow all origins (*)
    - Production: Restrict to known frontend domain(s)
- CORS must be enforced at the API level
    - Configure CORS policies centrally in the application startup


These practices secure the authentication boundary, protect sensitive data, enforce access control, and prepare for compliance needs.  Deviations must be explicitly reviewed, justified and documented.
