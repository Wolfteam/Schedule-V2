using Schedule.Domain.Interfaces.Dto;

namespace Schedule.Api.IntegrationTests.Builders
{
    public class BasePaginatedRequestDtoBuilder<TBuilder, TDto> : BaseDtoBuilder<TDto>
        where TBuilder : class
        where TDto : class, IPaginatedRequestDto, new()
    {
        public TBuilder WithDefaults()
        {
            return WithPage();
        }

        public TBuilder WithPage(int take = 10, int page = 1)
        {
            Dto.Take = take;
            Dto.Page = page;
            return this as TBuilder;
        }
    }
}
