using System.Collections.Generic;

namespace Schedule.Shared.Dto
{
    public class ApiListResponseDto<T> : ApiResponseDto<List<T>>
    {
        public ApiListResponseDto()
        {
            Result = new List<T>();
        }

        public ApiListResponseDto(List<T> results) : base(results)
        {
        }
    }
}
