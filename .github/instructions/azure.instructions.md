# Azure Platform Guidelines for Application Infrastructure

This document defines Azure-specific infrastructure design and operational standards. It ensures consistency, security, and scalability across environments deployed within a single Azure subscription using Terraform and GitHub Actions.

## Role Definition

You are building and managing application infrastructure on Azure using Infrastructure as Code (IaC) with Terraform. Follow these platform-specific conventions to enforce consistency in naming, tagging, resource scoping, identity usage, observability, and service selection. These guidelines are optimized for small-to-mid scale production deployments with flexibility for future growth.

## Resource Group & Environment Strategy

- Each environment (e.g., dev, staging, prod) must be deployed to its own **resource group**.
- All environments will reside within a **single Azure subscription**.
- A separate resource group may be created for **shared infrastructure** (e.g., Terraform state storage, Key Vault, monitoring components).

## Naming Conventions

Apply consistent, environment-aware, region-aware, and role-aware names for all Azure resources. See [Defining your naming convention](https://learn.microsoft.com/en-us/azure/cloud-adoption-framework/ready/azure-best-practices/resource-naming) and [Naming rules and restrictions for Azure resources](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/resource-name-rules) for more details.


### Format

`<resource-abbreviation>-<app-name>-<module>-<environment>-<region>[-<ordinal>]`

Where:
- `<resource-abbreviation>`: Short code for the resource type (e.g., `rg`, `app`, `kv`, `pg`)
- `<app-name>`: Application or project name
- `<module>`: Module or component name (if applicable)
- `<environment>`: Environment (e.g., `dev`, `prod`)
- `<region>`: Azure region (e.g., `eastus`)
- `<ordinal>`: Optional, for multiple instances


### Examples

| Resource Type     | Example                           |
|-------------------|-----------------------------------|
| Resource Group     | `rg-myapp-api-dev-eastus-001`     |
| App Service        | `app-myapp-api-dev-eastus-001`    |
| Key Vault          | `kv-myapp-core-dev-eastus-001`    |
| PostgreSQL Server  | `pg-myapp-data-dev-eastus-001`    |

### Rules

- Use lowercase and hyphen-separated names (kebab-case)
- Abbreviate resource types (`rg`, `kv`, `app`, `pg`, etc.)
- Include ordinal if multiple instances exist per environment

## Tagging Standards

All resources must include the following **mandatory tags**:

| Tag Name         | Description                         |
|------------------|-------------------------------------|
| `environment`     | `dev`, `staging`, `prod`, etc.      |
| `owner`           | Team or individual owner            |
| `costCenter`      | Cost tracking reference             |
| `module`          | App module name (e.g., `locations`) |

Tags must be defined in a shared Terraform variable set and enforced consistently across modules.

See [Defining your tagging strategy](https://learn.microsoft.com/en-us/azure/cloud-adoption-framework/ready/azure-best-practices/resource-tagging) for more details.

## Identity & Access Management

- Use **system-assigned managed identities** by default for service-to-service authentication (e.g., App Service accessing Key Vault or PostgreSQL).
- RBAC must be used for resource access control:
  - Grant identities **least privilege** permissions at the **resource group** or **resource level**
  - Avoid subscription-wide Contributor roles unless explicitly justified
- End-user identity is handled via **ASP.NET Core Identity** in the application layer (not Azure AD B2C or Entra ID).

## Regional Strategy

- Default deployment region is `East US`
- Guidance should remain region-agnostic unless a feature or resource is region-specific
- Do not hardcode regions—pass as variables to Terraform modules

## Availability & Redundancy

- Availability Zones are **not required initially**, but infrastructure should be designed to allow future zone redundancy (e.g., don't deploy to zone-locked SKUs)
- Active-active multi-region is **not used** at this time
- RTO and RPO objectives should be defined per critical component (see [cloud-architecture.instructions.md](cloud-architecture.instructions.md) for more details)

## Recommended Azure Services

| Concern          | Service                              | Notes                                       |
|------------------|--------------------------------------|---------------------------------------------|
| App hosting       | Azure App Service (Linux Plan)       | Use Premium or Basic plans depending on scale |
| Identity (infra)  | Managed Identity (System-Assigned)   | Used for App ↔ DB, App ↔ Key Vault, etc.     |
| Secrets           | Azure Key Vault                      | Use RBAC access model                       |
| Database          | Azure Database for PostgreSQL - Flexible Server | Use private access if possible |
| Messaging         | Azure Service Bus                    | For cross-module or eventual consistency needs |
| Observability     | Application Insights + Log Analytics | Enable structured logging and telemetry     |
| Static Assets     | Azure Storage (Static Website)       | Optional, for hosting frontend if decoupled |

Avoid using Azure Functions or AKS unless justified for workload-specific scalability or orchestration needs.

## Observability & Monitoring

- Enable **Application Insights** with distributed tracing
- Use **Log Analytics** for central log aggregation
- Correlation ID should be passed and preserved in all services
- Future enhancements may include alert rules, workbooks, and dashboards

## CI/CD Integration

- Use **GitHub Actions** for deploying infrastructure and application artifacts
- Store Terraform state in an Azure Storage Account backend (see [terraform.instructions.md](terraform.instructions.md) for details)
- Secrets for CI/CD should be stored in GitHub Secrets or Azure Key Vault, not in repo

## Security Guidelines

- All services must use **private access endpoints** when possible (Key Vault, PostgreSQL)
- Enforce HTTPS-only endpoints for App Services and other public resources
- Resource firewall rules must restrict access to known IP ranges (e.g., GitHub Action IPs, company ranges)
- Rotate secrets periodically; leverage Key Vault for shared secret distribution

## Azure Policy & Compliance

- Azure Policy is **not yet in use**, but designs should consider policy compatibility (e.g., tag enforcement, naming restrictions)
- Follow Microsoft Cloud Security Benchmark as an evolving reference standard
- Security considerations should be documented per component

## Deviation Policy

All deviations from this guidance must be justified in `/docs/architecture/decisions`, including use case, risk mitigation, and alternative controls if security or cost implications are involved.
