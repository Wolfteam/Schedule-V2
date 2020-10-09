namespace Schedule.Application.Interfaces.Managers
{
    public interface IAppUserManager
    {
        long Id { get; }
        string Username { get; }
        string Email { get; }
        string FullName { get; }
    }
}
