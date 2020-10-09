namespace Schedule.Shared
{
    public static class AppConstants
    {
        public const string MigrationsTableName = "__EFMigrationsHistory";

        public static string GenerateTableSchema(string schema, string entityName)
            => $"{schema ?? "dbo"}_{entityName}";
    }
}
