using Schedule.Domain.Enums;
using System;

namespace Schedule.Shared.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public AppMessageType ErrorMessageId { get; }

        private InvalidRequestException()
            : base()
        {
            ErrorMessageId = AppMessageType.SchApiInvalidRequest;
        }

        private InvalidRequestException(string message)
            : base(message)
        {
            ErrorMessageId = AppMessageType.SchApiInvalidRequest;
        }

        private InvalidRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidRequestException(string message, AppMessageType errorMessageId = AppMessageType.SchApiInvalidRequest)
            : base(message)
        {
            ErrorMessageId = errorMessageId;
        }
    }
}
