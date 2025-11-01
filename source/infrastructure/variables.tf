variable "subscription_id" {
  description = "The Azure Subscription ID where resources will be deployed"
  type        = string
}

variable "environment" {
  description = "The environment for which the infrastructure is being provisioned (e.g., dev, staging, prod)"
  type        = string
}

variable "location" {
  description = "The Azure region where resources will be deployed"
  type        = string
  default     = "East US"
}

variable "app_service_plan_location" {
  description = "The location for the App Service Plan"
  type        = string
}

variable "app_service_plan_sku_name" {
  description = "The SKU for the App Service Plan"
  type        = string
}

variable "key_vault_purge_protection_enabled" {
  description = "Whether purge protection is enabled for the Key Vault"
  type        = bool
}

variable "log_analytics_workspace_sku" {
  description = "The SKU for the Log Analytics Workspace"
  type        = string
}

variable "log_analytics_workspace_retention_days" {
  description = "The retention period for the Log Analytics Workspace in days"
  type        = number
}

variable "asp_net_core_environment" {
  description = "The ASP.NET Core environment setting (e.g., Development, Staging, Production)"
  type        = string
}

variable "api_megabucks_url_redirect" {
  description = "The URL to redirect API requests to"
  type        = string
}

variable "api_health_check_frequency" {
  description = "The frequency of health checks for the API in seconds"
  type        = number
}

variable "enterprise_app_object_id" {
  description = "The Object ID of the enterprise application to assign Key Vault permissions. Replace with your app's Object ID."
  type        = string
  default     = "e926f6d8-8342-4ae0-a497-9d47c744b024"
}
