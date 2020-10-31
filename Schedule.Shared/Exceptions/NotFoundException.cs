using Schedule.Domain.Enums;
using System;

namespace Schedule.Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public AppMessageType ErrorMessageId { get; }

        private NotFoundException()
            : base()
        {
            ErrorMessageId = AppMessageType.SchApiNotFound;
        }

        public NotFoundException(string message)
            : base(message)
        {
            ErrorMessageId = AppMessageType.SchApiNotFound;
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorMessageId = AppMessageType.SchApiNotFound;
        }

        public NotFoundException(string msg, AppMessageType errorMessageId)
            : base(msg)
        {
            ErrorMessageId = errorMessageId;
        }

        public NotFoundException(string name, long id, AppMessageType errorMessageId = AppMessageType.SchApiNotFound)
            : base($"Entity = {name} associated to id = {id} was not found.")
        {
            ErrorMessageId = errorMessageId;
        }
    }
}
