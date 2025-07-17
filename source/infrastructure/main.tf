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

data "azurerm_key_vault" "kv-environment" {
    name                = "kv-megabucks-${var.environment}"
    resource_group_name = "rg-infrastructure"
}

data "azurerm_key_vault_secret" "db-connection"{
    name         = "bowlingm-nec-tournament-db-connection-string"
    key_vault_id = data.azurerm_key_vault.kv-environment.id
}