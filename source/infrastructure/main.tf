terraform {
  required_version = ">= 1.6.0"

  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.38.1"
    }
  }

  backend "azurerm" {}
}

provider "azurerm" {
  features {
    resource_group {
      prevent_deletion_if_contains_resources = true
    }

    key_vault {
      purge_soft_delete_on_destroy = false
    }
  }

  use_oidc        = true
  subscription_id = var.subscription_id
}

data "azurerm_client_config" "current" {}

data "azurerm_key_vault" "key_vault_environment" {
  name                = "kv-megabucks-${var.environment}"
  resource_group_name = "rg-infrastructure"
}

data "azurerm_key_vault_secret" "db_connection" {
  name         = "bowlingm-nec-tournament-db-connection-string"
  key_vault_id = data.azurerm_key_vault.key_vault_environment.id
}

data "azurerm_key_vault_secret" "encryption_key" {
  name         = "encryption-key"
  key_vault_id = data.azurerm_key_vault.key_vault_environment.id
}

data "azurerm_key_vault_secret" "api_key" {
  name         = "api-key"
  key_vault_id = data.azurerm_key_vault.key_vault_environment.id
}

resource "azurerm_resource_group" "resource_group" {
  name     = "rg-trn-mgr-${var.environment}"
  location = var.location
}

resource "azurerm_service_plan" "app_service_plan" {
  name                = "asp-trn-mgr-${var.environment}"
  location            = var.app_service_plan_location
  resource_group_name = azurerm_resource_group.resource_group.name

  os_type  = "Linux"
  sku_name = var.app_service_plan_sku_name
}

resource "azurerm_log_analytics_workspace" "log_analytics_workspace" {
  name                = "log-trn-mgr-${var.environment}"
  location            = azurerm_service_plan.app_service_plan.location
  resource_group_name = azurerm_resource_group.resource_group.name

  sku               = var.log_analytics_workspace_sku
  retention_in_days = var.log_analytics_workspace_retention_days
}

resource "azurerm_application_insights" "application_insights" {
  name                = "ai-trn-mgr-${var.environment}"
  location            = azurerm_service_plan.app_service_plan.location
  resource_group_name = azurerm_resource_group.resource_group.name

  application_type = "web"
  workspace_id     = azurerm_log_analytics_workspace.log_analytics_workspace.id
}

resource "azurerm_key_vault" "app_key_vault" {
  name                = "kv-megabks-trn-mgr-${var.environment}"
  location            = azurerm_service_plan.app_service_plan.location
  resource_group_name = azurerm_resource_group.resource_group.name

  sku_name  = "standard"
  tenant_id = data.azurerm_client_config.current.tenant_id

  purge_protection_enabled   = var.key_vault_purge_protection_enabled
  soft_delete_retention_days = 90

  enable_rbac_authorization = true
}

resource "azurerm_monitor_diagnostic_setting" "app_key_vault_diagnostics" {
  name               = "kv-diagnostics-${var.environment}"
  target_resource_id = azurerm_key_vault.app_key_vault.id
  log_analytics_workspace_id = azurerm_log_analytics_workspace.log_analytics_workspace.id

  enabled_log {
    category = "AuditEvent"
  }

  enabled_metric {
    category = "AllMetrics"
  }
}

resource "azurerm_role_assignment" "terraform_kv_secrets_user" {
  scope                = azurerm_key_vault.app_key_vault.id
  role_definition_name = "Key Vault Secrets Officer"
  principal_id         = data.azurerm_client_config.current.object_id
}

resource "azurerm_key_vault_secret" "secret_encryption_key" {
  name         = "EncryptionKey"
  value        = data.azurerm_key_vault_secret.encryption_key.value
  key_vault_id = azurerm_key_vault.app_key_vault.id
}

resource "azurerm_key_vault_secret" "secret_api_key" {
  name         = "Authentication--ApiKey"
  value        = data.azurerm_key_vault_secret.api_key.value
  key_vault_id = azurerm_key_vault.app_key_vault.id
}

resource "azurerm_key_vault_secret" "secret_db_connection_string" {
  name         = "ConnectionStrings--Default"
  value        = data.azurerm_key_vault_secret.db_connection.value
  key_vault_id = azurerm_key_vault.app_key_vault.id
}

resource "azurerm_linux_web_app" "api" {
  name                = "api-trn-mgr-${var.environment}"
  location            = azurerm_service_plan.app_service_plan.location
  resource_group_name = azurerm_resource_group.resource_group.name
  service_plan_id     = azurerm_service_plan.app_service_plan.id

  https_only = true

  site_config {
    always_on           = true
    http2_enabled       = true
    ftps_state          = "Disabled"
    minimum_tls_version = "1.2"

    application_stack {
      dotnet_version = "9.0"
    }

    health_check_path                 = "/health"
    health_check_eviction_time_in_min = 5
  }

  app_settings = {
    ASPNETCORE_ENVIRONMENT                = var.asp_net_core_environment
    APPLICATIONINSIGHTS_CONNECTION_STRING = azurerm_application_insights.application_insights.connection_string
    APPINSIGHTS_CLOUD_ROLE_NAME           = "api-trn-mgr-${var.environment}"

    WEBSITE_RUN_FROM_PACKAGE       = "1"
    WEBSITE_ENABLE_SERVICE_STORAGE = "false"

    KEYVAULT_URL = azurerm_key_vault.app_key_vault.vault_uri
  }

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_monitor_diagnostic_setting" "app_service_diagnostics" {
  name                       = "app-service-diagnostics-${var.environment}"
  target_resource_id         = azurerm_linux_web_app.api.id
  log_analytics_workspace_id = azurerm_log_analytics_workspace.log_analytics_workspace.id

  enabled_log {
    category = "AppServiceHTTPLogs"
  }
  enabled_log {
    category = "AppServiceConsoleLogs"
  }
  enabled_log {
    category = "AppServiceAuditLogs"
  }
  enabled_log {
    category = "AppServiceAppLogs"
  }

  enabled_metric {
    category = "AllMetrics"
  }
}

resource "azurerm_app_service_custom_hostname_binding" "api_custom_domain" {
  hostname            = var.api_megabucks_url_redirect
  app_service_name    = azurerm_linux_web_app.api.name
  resource_group_name = azurerm_resource_group.resource_group.name
}

resource "azurerm_app_service_managed_certificate" "api_custom_domain_managed_cert" {
  custom_hostname_binding_id = azurerm_app_service_custom_hostname_binding.api_custom_domain.id
}

resource "azurerm_app_service_certificate_binding" "api_custom_domain_cert_binding" {
  hostname_binding_id = azurerm_app_service_custom_hostname_binding.api_custom_domain.id
  certificate_id      = azurerm_app_service_managed_certificate.api_custom_domain_managed_cert.id
  ssl_state           = "SniEnabled"
}

resource "azurerm_role_assignment" "web_app_kv_secrets_user" {
  scope                = azurerm_key_vault.app_key_vault.id
  role_definition_name = "Key Vault Secrets User"
  principal_id         = azurerm_linux_web_app.api.identity[0].principal_id
}

# Assign Key Vault Secrets User to the enterprise application
resource "azurerm_role_assignment" "enterprise_app_kv_secrets_user" {
  scope                = azurerm_key_vault.app_key_vault.id
  role_definition_name = "Key Vault Secrets User"
  principal_id         = var.enterprise_app_object_id
}

# Create a client secret for the enterprise app
resource "azuread_application_password" "enterprise_app_client_secret" {
  application_object_id = var.enterprise_app_object_id
  display_name          = "${var.environment}"
  end_date              = "2025-12-31T23:59:59Z" # Expires at end of current year
}

resource "azurerm_application_insights_standard_web_test" "api_health_check" {
  name                    = "api-health-check-${var.environment}"
  location                = azurerm_service_plan.app_service_plan.location
  resource_group_name     = azurerm_resource_group.resource_group.name
  application_insights_id = azurerm_application_insights.application_insights.id

  description   = "Tournament Manager API Health Check"
  enabled       = true
  frequency     = var.api_health_check_frequency
  timeout       = 30  # Timeout after 30 seconds
  retry_enabled = true

  geo_locations = [
    "us-fl-mia-edge", #Central US
    "us-va-ash-azr",  #East US
    "us-ca-sjc-azr",  #West US
    #"us-il-ch1-azr", #North Central US
    "us-tx-sn1-azr",  #South Central US
  ]

  request {
    url = "https://${var.api_megabucks_url_redirect}/health"
  }

  validation_rules {
    expected_status_code = 200
  }
}