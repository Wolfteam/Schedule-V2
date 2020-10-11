namespace Schedule.Shared
{
    public static class AppConstants
    {
        public const string MigrationsTableName = "__EFMigrationsHistory";
        public const string TestingUrl = "https://localhost";
        public const string TestingEnvironment = "Testing";

        public const string SchedulePermissionsClaim = "permissions.schedule";
        public const string LanguageClaim = "language";

        public static string GenerateTableSchema(string schema, string entityName)
            => $"{schema ?? "dbo"}_{entityName}";
    }
}
