namespace Schedule.Shared.Interfaces.Dto
{
    public interface IPaginatedResponseDto
    {
        int Take { get; set; }
        int TotalRecords { get; set; }
        int CurrentPage { get; set; }
        int Records { get; }
        int TotalPages { get; }
    }
}
