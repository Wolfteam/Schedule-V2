namespace Schedule.Application.Priorities.Commands.Delete
{
    public class DeletePriorityCommand : BaseEmptyRequest
    {
        public long Id { get; }

        public DeletePriorityCommand(long id)
        {
            Id = id;
        }
    }
}
