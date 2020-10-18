namespace Schedule.Application.Periods.Commands.Delete
{
    public class DeletePeriodCommand : BaseEmptyRequest
    {
        public long Id { get; }

        public DeletePeriodCommand(long id)
        {
            Id = id;
        }
    }
}
