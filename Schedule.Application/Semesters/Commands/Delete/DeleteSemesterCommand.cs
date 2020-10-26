namespace Schedule.Application.Semesters.Commands.Delete
{
    public class DeleteSemesterCommand : BaseEmptyRequest
    {
        public long Id { get; }

        public DeleteSemesterCommand(long id)
        {
            Id = id;
        }
    }
}
