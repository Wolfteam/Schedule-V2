using Schedule.Domain.Dto.Teachers.Responses;

namespace Schedule.Application.Teachers.Queries.GetAvailability
{
    public class GetTeacherAvailabilityQuery : BaseApiListRequest<TeacherAvailabilityResponseDto>
    {
        public long TeacherId { get; }

        public GetTeacherAvailabilityQuery(long teacherId)
        {
            TeacherId = teacherId;
        }
    }
}
