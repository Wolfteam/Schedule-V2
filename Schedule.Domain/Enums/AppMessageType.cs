namespace Schedule.Domain.Enums
{
    public enum AppMessageType
    {
        SchApiInvalidRequest = 100,
        SchApiUnknownErrorOccurred = 101,
        SchApiNotFound = 102,
        SchApiResourceAlreadyExists = 104,
        SchApiResourceCantBeDeletedIsBeingUsed = 105,

        IdsInvalidRequest = 1000,
        IdsUnknownErrorOccurred = 1001,
        IdsNotFound = 1002,

        SchWebInvalidRequest = 2000,
        SchWebUnknownErrorOccurred = 2001,
        SchWebNotFound = 2002,
        SchWebInvalidUsernameOrPassword = 2003
    }
}
