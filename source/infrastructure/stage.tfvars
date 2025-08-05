environment = "stage"

location = "East US"

app_service_plan_location = "East US 2"
app_service_plan_sku_name = "B2"

log_analytics_workspace_sku            = "PerGB2018"
log_analytics_workspace_retention_days = 30 # make 365 for production

asp_net_core_environment   = "Staging"
api_megabucks_url_redirect = "api.staging.bowlingmegabucks.com"

api_health_check_frequency = 900

key_vault_purge_protection_enabled = false