namespace Schedule.Application.Careers.Commands.Delete
{
    public class DeleteCareerCommand : BaseEmptyRequest
    {
        public long Id { get; }

        public DeleteCareerCommand(long id)
        {
            Id = id;
        }
    }
}
