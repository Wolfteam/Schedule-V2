using Refit;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Subjects.Requests;
using Schedule.Domain.Dto.Subjects.Responses;
using System.Threading.Tasks;

namespace Schedule.Web.Interfaces.Apis
{
    [Headers("Authorization: Bearer")]
    public interface ISubjectApi
    {
        [Get("/api/Subject")]
        Task<PaginatedResponseDto<GetAllSubjectsResponseDto>> GetAllSubjects([Query] GetAllSubjectsRequestDto dto);

        [Post("/api/Subject")]
        Task<ApiResponseDto<GetAllSubjectsResponseDto>> CreateSubject(SaveSubjectRequestDto dto);

        [Put("/api/Subject/{id}")]
        Task<ApiResponseDto<GetAllSubjectsResponseDto>> UpdateSubject(long id, SaveSubjectRequestDto dto);

        [Delete("/api/Subject/{id}")]
        Task<EmptyResponseDto> DeleteSubject(long id);
    }
}
