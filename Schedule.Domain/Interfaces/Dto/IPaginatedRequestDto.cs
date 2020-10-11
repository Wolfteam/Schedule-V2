using Schedule.Domain.Enums;

namespace Schedule.Domain.Interfaces.Dto
{
    public interface IPaginatedRequestDto
    {
        int Take { get; set; }
        int Page { get; set; }
        string SearchTerm { get; set; }
        string OrderBy { get; set; }
        bool OrderByAsc { get; set; }
        AppLanguageType Language { get; set; }
    }
}
