using MediatR;
using Schedule.Domain.Dto;

namespace Schedule.Application
{
    public abstract class BaseApiRequest<TResponse> : IRequest<ApiResponseDto<TResponse>>
    {
        protected BaseApiRequest()
        {
        }
    }

    public abstract class BaseApiRequest<TRequest, TResponse> : BaseRequest<TRequest>, IRequest<ApiResponseDto<TResponse>>
    {
        protected BaseApiRequest(TRequest dto) : base(dto)
        {
        }
    }
}
