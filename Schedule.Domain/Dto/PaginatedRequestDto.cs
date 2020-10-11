using Schedule.Domain.Enums;
using Schedule.Domain.Interfaces.Dto;

namespace Schedule.Domain.Dto
{
    public class PaginatedRequestDto : IPaginatedRequestDto
    {
        public int Take { get; set; }
        public int Page { get; set; }
        public string SearchTerm { get; set; }
        public string OrderBy { get; set; }
        public bool OrderByAsc { get; set; } = true;
        public AppLanguageType Language { get; set; }
    }
}
