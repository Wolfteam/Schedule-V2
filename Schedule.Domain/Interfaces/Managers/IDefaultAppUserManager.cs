using Schedule.Domain.Enums;

namespace Schedule.Domain.Interfaces.Managers
{
    public interface IDefaultAppUserManager
    {
        string Username { get; }
        string Email { get; }
        string FullName { get; }
        AppLanguageType Language { get; }
        ApplicationType Application { get; }
    }
}
