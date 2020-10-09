using MediatR;
using Schedule.Shared.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application
{
    public abstract class BasePaginatedHandler<TPaginatedQuery, TResponse> : IRequestHandler<TPaginatedQuery, PaginatedResponseDto<TResponse>>
        where TPaginatedQuery : BasePaginatedQuery<TResponse>
    {
        public abstract Task<PaginatedResponseDto<TResponse>> Handle(TPaginatedQuery request, CancellationToken cancellationToken);
    }
}
