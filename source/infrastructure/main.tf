terraform {
  required_version = ">= 1.6.0"

  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.23.0"
    }
  }

  backend "azurerm" {}
}

provider "azurerm" {
  features {
    resource_group {
      prevent_deletion_if_contains_resources = true
    }
  }

  use_oidc = true
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
  location            = azurerm_resource_group.resource_group.location
  resource_group_name = azurerm_resource_group.resource_group.name

  os_type  = "Linux"
  sku_name = var.app_service_plan_sku_name
}

resource "azurerm_log_analytics_workspace" "log_analytics_workspace" {
  name                = "log-trn-mgr-${var.environment}"
  location            = azurerm_resource_group.resource_group.location
  resource_group_name = azurerm_resource_group.resource_group.name

  sku               = var.log_analytics_workspace_sku
  retention_in_days = var.log_analytics_workspace_retention_days
}

resource "azurerm_application_insights" "application_insights" {
  name                = "ai-trn-mgr-${var.environment}"
  location            = azurerm_resource_group.resource_group.location
  resource_group_name = azurerm_resource_group.resource_group.name

  application_type = "web"
  workspace_id     = azurerm_log_analytics_workspace.log_analytics_workspace.id
}

resource "azurerm_key_vault" "app_key_vault" {
  name                = "kv-trn-mgr-${var.environment}"
  location            = azurerm_resource_group.resource_group.location
  resource_group_name = azurerm_resource_group.resource_group.name

  sku_name  = "standard"
  tenant_id = data.azurerm_client_config.current.tenant_id

  purge_protection_enabled   = var.key_vault_purge_protection_enabled
  soft_delete_retention_days = 90
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
  location            = azurerm_resource_group.resource_group.location
  resource_group_name = azurerm_resource_group.resource_group.name
  service_plan_id     = azurerm_service_plan.app_service_plan.id

  site_config {
    always_on           = true
    http2_enabled       = true
    ftps_state          = "Disabled"
    minimum_tls_version = "1.2"

    application_stack {
      dotnet_version = "9.0"
    }
  }

  app_settings = {
    ASPNETCORE_ENVIRONMENT                = var.asp_net_core_environment
    APPLICATIONINSIGHTS_CONNECTION_STRING = azurerm_application_insights.application_insights.connection_string
    APPINSIGHTS_CLOUD_ROLE_NAME           = "api-trn-mgr-${var.environment}"

    WEBSITE_RUN_FROM_PACKAGE       = "1"
    WEBSITE_ENABLE_SERVICE_STORAGE = "false"
  }

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_role_assignment" "web_app_kv_secrets_user" {
  scope                = azurerm_key_vault.app_key_vault.id
  role_definition_name = "Key Vault Secrets User"
  principal_id         = azurerm_linux_web_app.api.identity[0].principal_id
}