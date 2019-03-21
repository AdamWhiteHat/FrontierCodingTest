namespace FrontierCodingTest
{
    // I am pulling the endpoint URI from the web.config.
    // This allows for a different configuration for different builds (debug, release).
    // It also allows the URI to be updated in production without requiring a code deploy.
    public static class Settings
    {
        public static string Endpoint_Accounts = SettingsReader.ReadValue<string>("Endpoint.Accounts");
    }
}