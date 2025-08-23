# Cloud Architecture Guidelines for Scalable and Secure Systems

This document establishes core principles and standards for designing modern cloud-native systems. These practices are provider-agnostic and serve as a foundational baseline for all infrastructure and application design decisions. This guidance emphasizes scalability, fault tolerance, cost-efficiency, observability, and secure-by-design architectures.

## Role Definition

You are designing distributed cloud systems intended to be deployed on a major cloud provider (e.g., Azure). You are responsible for architectural decisions that affect availability, scalability, security, resilience, cost, and operational visibility. Follow the principles outlined in this document when evaluating trade-offs, defining service boundaries, or implementing infrastructure patterns.

## Core Principles

- Design for failure (graceful degradation, retries, timeouts, fallback mechanisms)
- Favor managed services unless explicit technical or cost constraints justify otherwise
- Prefer horizontal scaling over vertical
- Automate everything (infrastructure, policy, identity, recovery)
- Embrace least privilege and zero trust security models
- Ensure traceability, observability, and auditability from day one
- Optimize cost early, then tune with real-world usage

## System Design Patterns

### Resiliency

- Use availability zones or multi-region deployments for critical services
- Apply retries with exponential backoff and circuit breakers at integration points
- Use message queues or event streaming for decoupling workloads
- Design for graceful degradation (partial failures shouldn't bring the system down)

### Scalability

- Identify components that can scale independently (e.g., stateless services)
- Use autoscaling groups or serverless compute for burstable loads
- Partition stateful data stores by workload, customer, or domain (bounded contexts)

### Security

- Apply defense-in-depth: network isolation + identity-based access + data encryption
- Use service-to-service authentication and authorization with strong identity (e.g., managed identity)
- Avoid hardcoding secrets; use secure secret stores
- Design for secrets rotation and auditing

### Identity & Access

- Use least privilege access by default (zero trust)
- Minimize human access to infrastructure
- Automate identity provisioning for services (e.g., workloads should never need secrets embedded)
- Delegate authorization decisions to a centralized policy engine when feasible

### Observability

- Every service must emit logs, metrics, and traces
- Use correlation IDs across requests and services
- Define SLIs, SLOs, and error budgets early
- Separate control plane and data plane observability

### Cost Optimization

- Right-size compute and storage resources
- Use reserved instances or autoscaling groups where appropriate
- Deallocate unused resources in non-prod environments
- Monitor per-resource and per-team cost breakdowns

### Disaster Recovery & Availability

- Define RTO (Recovery Time Objective) and RPO (Recovery Point Objective) per service tier
- Choose regional or zone-redundant services based on those targets
- Test failover scenarios regularly
- Replicate data where needed; ensure consistency models are well understood

## Infrastructure Layering

Design infrastructure in layered logical tiers:

- **Core Platform Layer**: Networking, identity, monitoring, secrets, policy
- **Shared Infrastructure Layer**: Shared databases, event buses, message brokers
- **Application Infrastructure Layer**: App-specific compute, databases, caches, storage

Each layer should be independently deployable and versioned.

## Environment Strategy

- Use clearly separated environments: dev, test, staging, prod
- Promote config drift prevention: no manual changes in shared environments
- Avoid using long-lived feature environments unless lifecycle is well-managed

## Data Management & Integration

- Use integration boundaries aligned with business contexts (Domain-Driven Design)
- Prefer async communication between bounded contexts
- Use explicit anti-corruption layers between subsystems
- Ensure data retention, backup, and compliance obligations are met

## Compliance and Risk

- Maintain asset inventory (tagging, CMDB, etc.)
- Use threat modeling techniques (e.g., STRIDE) during design
- Map design decisions to NIST, SOC 2, or ISO 27001 controls as needed
- Enforce resource tagging for cost, ownership, and compliance traceability

## Tooling and Design Aids

- Use diagrams-as-code for architecture diagrams (e.g., Structurizr, Diagrams.net export)
- Capture architecture decisions in Architecture Decision Records (ADRs)
- Review architecture quarterly or when introducing major changes

## Deviation Policy

Any deviation from these guidelines must be justified and documented in `/docs/architecture/decisions`. Include trade-offs, risk mitigation plans, and references to relevant threat models or cost analyses.
