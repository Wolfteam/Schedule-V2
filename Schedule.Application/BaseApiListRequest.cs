using MediatR;
using Schedule.Domain.Dto;

namespace Schedule.Application
{
    public abstract class BaseApiListRequest<TResponse> : IRequest<ApiListResponseDto<TResponse>>
    {
        protected BaseApiListRequest()
        {
        }
    }

    public abstract class BaseApiListRequest<TRequest, TResponse> : BaseRequest<TRequest>, IRequest<ApiListResponseDto<TResponse>>
    {
        protected BaseApiListRequest(TRequest dto) : base(dto)
        {
        }
    }
}
