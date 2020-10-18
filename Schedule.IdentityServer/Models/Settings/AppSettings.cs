namespace Schedule.IdentityServer.Models.Settings
{
    public class AppSettings
    {
        public string Domain { get; set; }
        public string CertificatePath { get; set; }
        public string CertificatePassword { get; set; }
        public int MaxFailedAccessAttempts { get; set; }
    }
}
