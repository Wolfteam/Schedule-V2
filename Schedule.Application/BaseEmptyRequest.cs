using MediatR;
using Schedule.Domain.Dto;

namespace Schedule.Application
{
    public abstract class BaseEmptyRequest<TRequest> : BaseRequest<TRequest>, IRequest<EmptyResponseDto>
    {
        protected BaseEmptyRequest(TRequest dto) : base(dto)
        {
        }
    }

    public abstract class BaseEmptyRequest : IRequest<EmptyResponseDto>
    {
    }
}
