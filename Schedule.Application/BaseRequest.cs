using System;

namespace Schedule.Application
{
    public abstract class BaseRequest<TDto>
    {
        public TDto Dto { get; }

        protected BaseRequest(TDto dto)
        {
            Dto = dto ?? throw new ArgumentNullException("Dto can't be null");
        }
    }
}
