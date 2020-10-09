namespace Schedule.Shared.Models.Settings
{
    public class IdentityServerSettings
    {
        public string Authority { get; set; }
        public string ApiName { get; set; }
        public string ApiSecret { get; set; }
        public bool RequireHttpsMetadata { get; set; }
    }
}
