namespace Schedule.Application.Classrooms.Commands.Delete
{
    public class DeleteClassroomCommand : BaseEmptyRequest
    {
        public long Id { get; }

        public DeleteClassroomCommand(long id)
        {
            Id = id;
        }
    }
}
