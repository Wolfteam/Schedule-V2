using Schedule.Shared.Enums;

namespace Schedule.Shared.Dto
{
    public class PaginatedRequestDto
    {
        public int Take { get; set; }
        public int Page { get; set; }
        public string SearchTerm { get; set; }
        public string OrderBy { get; set; }
        public bool OrderByAsc { get; set; } = true;
        public AppLanguageType Language { get; set; }
    }
}
