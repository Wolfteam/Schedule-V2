namespace Schedule.Application.Classrooms.Commands.DeleteType
{
    public class DeleteClassroomTypeCommand : BaseEmptyRequest
    {
        public long Id { get; }

        public DeleteClassroomTypeCommand(long id)
        {
            Id = id;
        }
    }
}
