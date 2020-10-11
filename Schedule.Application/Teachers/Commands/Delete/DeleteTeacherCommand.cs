namespace Schedule.Application.Teachers.Commands.Delete
{
    public class DeleteTeacherCommand : BaseEmptyRequest
    {
        public long Id { get; }

        public DeleteTeacherCommand(long id)
        {
            Id = id;
        }
    }
}
