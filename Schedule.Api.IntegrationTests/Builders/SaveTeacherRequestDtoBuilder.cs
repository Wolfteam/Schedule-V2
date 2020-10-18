using Schedule.Domain.Dto.Teachers.Requests;
using System;

namespace Schedule.Api.IntegrationTests.Builders
{
    public class SaveTeacherRequestDtoBuilder : BaseDtoBuilder<SaveTeacherRequestDto>
    {
        public SaveTeacherRequestDtoBuilder WithDefaults(string firstName, string lastName, long priorityId)
        {
            Dto.IdentifierNumber = (int)DateTimeOffset.UtcNow.Ticks;
            Dto.FirstName = firstName;
            Dto.FirstLastName = lastName;
            Dto.PriorityId = priorityId;
            return this;
        }
    }
}
