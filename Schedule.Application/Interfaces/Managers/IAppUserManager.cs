using Schedule.Domain.Interfaces.Managers;

namespace Schedule.Application.Interfaces.Managers
{
    public interface IAppUserManager : IDefaultAppUserManager
    {
        long SchoolId { get; }
    }
}
