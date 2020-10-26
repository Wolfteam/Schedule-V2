using Schedule.Domain.Enums;

namespace Schedule.Shared.Exceptions
{
    public class ResourceAlreadyExistsException : InvalidRequestException
    {
        public ResourceAlreadyExistsException(
            string message,
            AppMessageType errorMessageId = AppMessageType.SchApiResourceAlreadyExists)
            : base(message, errorMessageId)
        {
        }
    }
}
