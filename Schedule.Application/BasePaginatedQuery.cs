using MediatR;
using Schedule.Shared.Dto;
using Schedule.Shared.Enums;
using Schedule.Shared.Interfaces.Dto;

namespace Schedule.Application
{
    public class BasePaginatedQuery<T> : IRequest<PaginatedResponseDto<T>>, IPaginatedRequestDto
    {
        public int Take { get; set; }
        public int Page { get; set; }
        public string SearchTerm { get; set; }
        public string OrderBy { get; set; }
        public bool OrderByAsc { get; set; } = true;
        public AppLanguageType Language { get; set; }
    }
}
