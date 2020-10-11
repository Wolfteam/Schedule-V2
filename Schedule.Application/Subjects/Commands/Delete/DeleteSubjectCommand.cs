namespace Schedule.Application.Subjects.Commands.Delete
{
    public class DeleteSubjectCommand : BaseEmptyRequest
    {
        public long Id { get; }

        public DeleteSubjectCommand(long id)
        {
            Id = id;
        }
    }
}
