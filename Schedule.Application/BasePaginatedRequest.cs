using MediatR;
using Schedule.Domain.Dto;
using Schedule.Domain.Interfaces.Dto;

namespace Schedule.Application
{
    public abstract class BasePaginatedRequest<TRequest, TResponse> : BaseRequest<TRequest>, IRequest<PaginatedResponseDto<TResponse>>
        where TRequest : IPaginatedRequestDto
    {
        protected BasePaginatedRequest(TRequest dto) : base(dto)
        {
        }
    }
}
