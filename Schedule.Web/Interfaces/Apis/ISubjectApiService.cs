using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Subjects.Requests;
using Schedule.Domain.Dto.Subjects.Responses;
using System.Threading.Tasks;

namespace Schedule.Web.Interfaces.Apis
{
    public interface ISubjectApiService
    {
        Task<PaginatedResponseDto<GetAllSubjectsResponseDto>> GetAllSubjects(GetAllSubjectsRequestDto dto);

        Task<ApiResponseDto<GetAllSubjectsResponseDto>> CreateSubject(SaveSubjectRequestDto dto);

        Task<ApiResponseDto<GetAllSubjectsResponseDto>> UpdateSubject(long id, SaveSubjectRequestDto dto);

        Task<EmptyResponseDto> DeleteSubject(long id);
    }
}
