namespace Schedule.Api.IntegrationTests.Builders
{
    public class BaseDtoBuilder<TDto> where TDto : class, new()

    {
        protected readonly TDto Dto = new TDto();

        public TDto Build() => Dto;
    }
}
