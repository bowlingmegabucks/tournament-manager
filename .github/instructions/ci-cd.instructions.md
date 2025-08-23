# CI/CD Pipeline Guidelines for Single-Module Application

This document defines the CI/CD standards for the tournament manager application, covering backend and infrastructure workflows. It ensures consistent automation across build, test, deployment, and observability phases using GitHub Actions, Terraform, and Azure.

## Role Definition

You are designing a CI/CD pipeline for a single-module .NET application composed of:
- An ASP.NET Core backend API (deployed to Azure App Service on Linux)
- Infrastructure provisioned via Terraform (Azure-focused)

The pipeline must be efficient, secure, observable, and enforce quality standards (tests, coverage, code analysis). Environments include `dev` (active) and `prod` (future), with a trunk-based branching strategy. OIDC will be used for GitHub → Azure authentication.

## Repository Structure

Use a monorepo with the following layout:

```
/src            # .NET application projects (API, Domain, Application, Infrastructure)
/tests          # Test projects
/infrastructure # Terraform infrastructure definitions
```

Workflows should use `paths` filtering to avoid unnecessary job execution.

## Workflow Design

Workflows reside in `.github/workflows/` and include:

### Backend

- **Trigger**: Push/PR to `src/**` or `tests/**`
- **Jobs**:
  - Lint using `dotnet format`
  - Run unit, integration, and e2e tests (xUnit, NSubstitute, FluentAssertions)
  - Run SonarQube analysis
  - `dotnet publish` build output
  - Build Docker image and push to container registry
  - Deploy to Azure App Service (on `main` branch)

### Infrastructure

- **Trigger**: Push/PR to `infrastructure/**`
- **Jobs**:
  - `terraform fmt`, `validate`, and `plan`
  - Manual approval gate for `terraform apply` (only for `main`)
  - Use per-environment `tfvars` and Azure backend state in Blob Storage

## Environment Strategies

- **Development**: Auto-deploy on `feature/*` branch merges (no approval)
- **Production**: Manual approval, slot-based staging, then swap
- **Trunk Strategy**:
  - `main` = production branch
  - Feature branches for development

## Artifact & Deployment Strategy

- **Backend**:
  - Build and publish output via `dotnet publish`
  - Create Docker container image
  - Deploy container to Azure App Service
  - Deploy ZIP to App Service via Kudu API
  - Tag deployments using Git SHA
  - Production deployments must be versioned using [Semantic Versioning](https://semver.org/)
  - Each production tag should follow the format `v<major>.<minor>.<patch>` (e.g., `v1.0.0`)
  - Tags trigger the production deployment pipeline when pushed to `main`
  - Tag format is enforced via workflow condition: tags must match `v\d+\.\d+\.\d+`
  - Mismatched or malformed tags will fail the pipeline early with a clear message
- **Rollback**:
  - Dev: Manual redeploy if needed
  - Prod: On health check failure post-slot swap, auto-rollback to previous slot version

## Secrets & Security

- Use GitHub → Azure OIDC (no SP secrets)
- Store secret values (e.g., DB connection strings) in Azure Key Vault
- Terraform config uses GitHub environment secrets for temporary bootstrap values
- Avoid storing secrets directly in GitHub unless bootstrapping

## Monitoring & Observability

- Integrate App Insights into backend
- Forward deployment events to App Insights using REST API
- Create alert rules via Terraform for key metrics (availability, failure rate)

## Best Practices

- Run only necessary jobs using `paths` filtering
- Use GitHub environments to isolate `dev` and `prod`
- Enforce quality gates (SonarQube, code coverage ≥50%, increasing to 80%)
- Use `cache` and `matrix` strategies for fast pipelines
- Use separate workflows for frontend, backend, and infrastructure
- Use reusable workflow templates where feasible

## Deviations & Exception Process

Any deviation from this CI/CD specification must be documented in the PR description and reviewed by the project maintainer. Temporary overrides must include a TODO and corresponding GitHub issue for resolution.
