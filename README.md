# Bowling Megabucks Tournament Manager

## Overview

This solution manages competitive bowling tournaments (multi-division, multi-squad) including registrations, lane assignments, scoring, side sweepers, reporting, and operational tooling. A modernization roadmap is in progress: current WinForms (MVP) → interim API-backed (Clean Architecture, DDD, CQRS + SignalR) → full web client.

## Ubiquitous Language (Source of Truth)

The following terms constitute the canonical domain vocabulary. Any newly introduced or redefined term MUST be added or updated here first; other documents (e.g., `FUNCTIONAL_SPEC.md`) mirror this table but are not authoritative.

| Term | Definition | Key Invariants / Notes | Open Questions |
|------|------------|------------------------|----------------|
| Tournament | Container for divisions, squads, registrations, sweepers, lane assignments, scores, results reporting. | Immutable identity; temporal span (start/end dates). | Do we model status (Planned, Active, Closed)? |
| Division | Competitive grouping (e.g., Scratch, Handicap bracket, age/gender). | May define handicap formula variables. | Are divisions mutable mid-tournament? |
| Squad | Scheduled play block within a tournament. | Uniqueness: (TournamentId, StartDateTime). | Can squads overlap timewise? |
| Registration | Bowler’s participation record for a tournament + division + optional squads + sweepers. | One per (TournamentId, BowlerId). Append extends; update modifies. | Can Division change after scores posted? |
| Bowler | Participant (name, gender, DOB, average, USBC ID, etc.). | USBC ID optional except in handicap divisions requiring it. | Average source & lock rules? |
| Sweeper | Optional side competition a registration may enter. | May have independent result computation. | Lifecycle states? Payout rules? |
| Super Sweeper | Special/high-tier sweeper enrollment (flag). | At most one per registration. | Pricing/fee distinct? |
| Lane Assignment | Placement of a registration into a lane slot (e.g., 22B) for a squad. | One active lane slot per bowler per squad. | Are swaps atomic? |
| Lane Slot | An addressable position (e.g., numeric pair + side) that may hold 0..1 registration. | Tied to skip pattern logic. | Are sides always A/B? |
| Skip Pattern | Algorithm describing how lanes are advanced between games (Same vs Staggered). | Affects recap sheet sequencing. | Additional patterns needed? |
| Score | Game-level or series-level numeric result linked to registration & lane slot. | Immutable after lock/finalization. | When do we lock? |
| Result Report | Generated artifact summarizing standings (seeding, at-large, squad, sweeper). | Derived only; never authoritative source of data. | Archive retention policy? |

### Change Control Process

1. Propose new or changed term (state intent + preliminary definition).
2. SME confirms or refines.
3. Update this table (add revision marker in History).
4. Reflect change in `FUNCTIONAL_SPEC.md` & any impacted code symbols (DTOs, commands, events).

If a term used in conversation is NOT in this table or its meaning conflicts, the assistant will pause implementation and request clarification before proceeding.

## Modernization Roadmap Snapshot

| Phase | Focus | Key Artifacts |
|-------|-------|---------------|
| Current | WinForms MVP + in-process domain | Presenters, Repositories |
| Interim | API (DDD + CQRS) + SignalR; WinForms consumes API | Commands, Queries, Events, Hub |
| Future | Web UI (SPA or similar) | Reuse contracts; replace WinForms |

### ASP.NET Core API Setup Checklist

Not all of these items need to be completed before work begins.  This is just an exhaustive list of things that should be considered during the setup process.

#### Phase 1: Foundation & Development Experience

- [x] **Global Exception Handling** - Centralized error handling with problem details
- [x] **Health Checks** - Basic endpoint for monitoring application status
- [x] **OpenAPI/Swagger Documentation** - API documentation and testing interface
- [x] **Request/Response Logging** - Structured logging for observability
- [ ] **Model Validation** - Automatic validation with problem details responses

#### Phase 2: API Design & Standards

- [x] **API Versioning** - Support for multiple API versions
- [ ] **CORS Configuration** - Cross-origin request handling
- [ ] **Content Negotiation** - Support for multiple response formats (JSON, XML)
- [ ] **Request Size Limits** - Protect against large payload attacks
- [ ] **Response Compression** - Gzip/Brotli compression for better performance

#### Phase 3: Security

- [ ] **Authentication** - JWT, API Keys, or OAuth2 integration
- [ ] **Authorization** - Role/policy-based access control
- [ ] **Rate Limiting** - Protect against abuse and DDoS
- [ ] **Security Headers** - HSTS, CSP, X-Frame-Options, etc.
- [ ] **Input Sanitization** - XSS and injection prevention

#### Phase 4: Performance & Reliability

- [ ] **Caching** - Response caching and distributed caching
- [ ] **Circuit Breaker Pattern** - Resilience for external dependencies
- [ ] **Retry Policies** - Automatic retry for transient failures
- [ ] **Connection Pooling** - Efficient database and HTTP connections
- [ ] **Background Services** - Long-running tasks and scheduled jobs

#### Phase 5: Observability & Monitoring

- [ ] **OpenTelemetry** - Distributed tracing and metrics
- [ ] **Application Insights** - Azure monitoring (if using Azure)
- [x] **Structured Logging** - JSON logging with correlation IDs
- [ ] **Performance Counters** - Custom metrics and KPIs
- [x] **Health Check UI** - Visual health monitoring dashboard

#### Phase 6: Production Readiness

- [x] **Configuration Management** - Environment-specific settings
- [ ] **Secrets Management** - Azure Key Vault or similar
- [ ] **Graceful Shutdown** - Proper application lifecycle handling
- [ ] **Container Support** - Docker containerization
- [ ] **Environment Detection** - Different behaviors per environment

#### Phase 7: Advanced Features

- [ ] **API Gateway Integration** - If using microservices architecture
- [ ] **Message Queuing** - Async processing with Service Bus/RabbitMQ
- [ ] **Event Sourcing** - If implementing CQRS/Event Sourcing
- [ ] **Multi-tenancy** - If supporting multiple tenants
- [ ] **Localization** - Multi-language support

#### Phase 8: Testing & Quality

- [ ] **Integration Tests** - API endpoint testing
- [ ] **Load Testing** - Performance and scalability validation
- [ ] **Contract Testing** - API contract validation
- [ ] **Security Testing** - Vulnerability scanning
- [ ] **Chaos Engineering** - Resilience testing

#### Phase 9: Deployment & DevOps

- [ ] **CI/CD Pipeline** - Automated build, test, and deployment
- [ ] **Blue-Green Deployment** - Zero-downtime deployments
- [ ] **Feature Flags** - Runtime feature toggling
- [ ] **Database Migrations** - Automated schema updates
- [ ] **Infrastructure as Code** - Terraform/ARM templates

#### Phase 10: Maintenance & Operations

- [ ] **API Documentation Versioning** - Keep docs in sync with API versions
- [ ] **Deprecation Strategy** - Plan for API lifecycle management
- [ ] **Performance Monitoring** - Continuous performance tracking
- [ ] **Capacity Planning** - Resource usage monitoring and scaling
- [ ] **Incident Response** - Monitoring, alerting, and runbooks

---

#### Priority Recommendations

**Start with Phase 1-2** for a functional development environment.

**Phase 3** is critical before any production deployment.

**Phase 4-5** should be implemented before handling significant load.

**Phase 6+** can be added incrementally based on specific requirements.

---

#### Implementation Notes

- Implement health checks early - they're required for most deployment platforms
- OpenAPI documentation improves developer experience significantly
- Security should never be an afterthought - implement early
- Observability (logging, tracing, metrics) is crucial for troubleshooting production issues
- Performance features like caching and compression provide immediate user experience improvements

This checklist follows the principle of building a solid foundation first, then adding layers of functionality, security, and operational capabilities in a logical order.

## Documentation Map

| File | Purpose |
|------|---------|
| `README.md` | Canonical domain vocabulary + high-level orientation. |
| `.github/instructions/functional-spec.instructions.md` | Interpreted functional description, migration planning, lane assignment deep dive. |

## Revision History (Excerpt)

| Date | Change | Sections |
|------|--------|----------|
| 2025-09-07 | Introduced Ubiquitous Language table & roadmap | README initial expansion |

## Contributing to Vocabulary

Open a PR or provide clarified definitions via discussion; changes are merged only after SME confirmation.
