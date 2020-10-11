using Schedule.Domain.Interfaces.Dto;
using System;

namespace Schedule.Domain.Dto
{
    public class PaginatedResponseDto<T> : ApiListResponseDto<T>, IPaginatedResponseDto
    {
        public int Take { get; set; }
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int Records =>
            Result.Count;
        public int TotalPages
        {
            get
            {
                if (Take <= 0)
                    return 0;
                double pages = (double)TotalRecords / Take;
                return (int)Math.Ceiling(pages);
            }
        }
    }
}
