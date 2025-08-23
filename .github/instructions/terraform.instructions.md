---
applyTo: "*.tf; *.tfvars"
---

# Terraform Guidelines for Application Infrastructure

This document defines standards and best practices for using Terraform to provision Azure-based infrastructure for this application. These guidelines apply only to Terraform files (`*.tf`, `*.tfvars`) located under the `/infrastructure` folder in the repository. These standards ensure a consistent, secure, and maintainable Infrastructure-as-Code (IaC) baseline aligned with industry best practices.

## Role Definition

You are writing and maintaining Terraform configuration for provisioning Azure infrastructure. Follow these conventions for organizing modules, managing environment configuration, enforcing security, handling secrets, and maintaining code hygiene. Terraform is not used to deploy application code — it provisions infrastructure only.

## Project Structure

Terraform code lives inside the `/infrastructure` directory of the monorepo:

```
/frontend           # React frontend code
/backend            # ASP.NET Core backend code
/infrastructure     # Terraform configuration
```

The `/infrastructure` folder will contain:

- A **root module** for orchestrating all environments
- **Reusable internal modules** (networking, app service, database, etc.)
- One `*.tfvars` file per environment (e.g., `dev.tfvars`, `prod.tfvars`)

## Module Design

- Use separate modules per concern: `network`, `compute`, `database`, `identity`, etc.
- Modules are scoped to **this application only** — no general-purpose reuse expected.
- All module inputs must be explicitly typed with validation and descriptions.
- Use `locals` for internal composition; keep modules declarative.
- Avoid use of `count` and `for_each` unless needed for iteration or conditional resources.
- Outputs must be well-named and scoped to only what’s required externally.

## Remote State

- Remote state is stored in an **Azure Storage Account**.
- A **single storage account** is used, with **separate containers per environment** (`tfstate-dev`, `tfstate-prod`, etc.)
- Terraform state locking is enabled using blob-level locks.

**Note:** The initial storage account and container creation should be done manually or via a one-time `bootstrap` script. Terraform cannot manage its own backend.

### Example backend config:

```hcl
terraform {
  backend "azurerm" {
    resource_group_name  = "rg-infra-dev-eastus-001"
    storage_account_name = "tfstatemyapp"
    container_name       = "tfstate-dev"
    key                  = "terraform.tfstate"
  }
}

## Environment Management

- All environments are deployed from the **same Terraform codebase**.
- Use `*.tfvars` files for each environment (e.g., dev.tfvars, prod.tfvars)
- Maintain a consistent naming/tagging strategy across environments (see [Azure Platform Guidelines](azure.instructions.md))

## Identity & Secrets

- Terraform will use an Azure service principal or **OIDC Identity** via GitHub Actions to authenticate to Azure.
- System-assigned managed identities will be provisioned for services via Terraform.
- **Secrets** (e.g. DB admin password, SMTP credentials) will be stored in **GitHub repository secrets** and passed as environment variables into the pipeline.

Avoid putting secrets directly into:

- *.tfvars files
- terraform.tfvars.json
- Terraform state outputs (unless hashed or safe to expose)

## Language and Features

- Avoid `null_resource`, `local-exec`, and `remote-exec` unless there is a justification and reviewed need for them
- Prefer data sources over hardcoding resource IDs where possible
- Use `terraform.workspace` only when needed; favor `.tfvars`-driven config for environment separation

## Tooling & Validation

All Terraform code must pass the following checks before being committed or merged:

| Check       | Tool              | Enforcement Location |
|-------------|-------------------|-----------------------|
| Format check| `terraform fmt -check` | Pre-commit / CI       |
| Validation  | `terraform validate`   | Pre-commit / CI       |
| Linting     | `tflint`              | Pre-commit / CI       |

CI pipelines will run these automatically.  Developers must also run them locally.

## CI/CD Integration

- GitHub Actions is used to `plan` and `apply` infrastructure
- `terraform apply` will run automatically when the infrastructure pipeline is triggered
- Manual `apply` is discouraged unless for emergency use - audit all changes through versioned pipelines

## Security

- Default to private endpoints where possible (e.g., PostgreSQL, Key Vault)
- Use HTTPS-only endpoints for all exposed services
- Audit outputs to ensure no secrets or sensitive data are included
- Restrict CI runner permissions via scoped service principals or OIDC tokens

## Deviation Policy

If a workaround or imperative logic (e.g., `local-exec`) is required, document the justification and risks in `/docs/architecture/decisions`.

All deviations from these practices must include a mitigation plan and justification.

