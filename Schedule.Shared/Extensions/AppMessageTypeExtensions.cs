using Schedule.Domain.Enums;
using System;
using System.Text.RegularExpressions;

namespace Schedule.Shared.Extensions
{
    public static class AppMessageTypeExtensions
    {
        public static string GetErrorMsg(this AppMessageType msg)
        {
            return msg switch
            {
                AppMessageType.SchApiInvalidRequest => "Invalid Request to the schedule api",
                AppMessageType.SchApiUnknownErrorOccurred => "Unknown error occurred in the schedule api",
                AppMessageType.SchApiNotFound => "The resource you were looking for was not found in the schedule api",
                AppMessageType.IdsInvalidRequest => "Invalid Request to the schedule's identity server",
                AppMessageType.IdsUnknownErrorOccurred => "Unknown error occurred in the schedule's identity server",
                AppMessageType.IdsNotFound => "The resource you were looking for was not found in the schedule's identity server",
                AppMessageType.SchWebInvalidRequest => "Invalid Request to the schedule web api",
                AppMessageType.SchWebUnknownErrorOccurred => "Unknown error occurred in the schedule web api",
                AppMessageType.SchWebNotFound => "The resource you were looking for was not found in the schedule web api",
                AppMessageType.SchWebInvalidUsernameOrPassword => "Invalid username or password",
                _ => throw new ArgumentOutOfRangeException(nameof(msg), msg, null)
            };
        }

        public static string GetErrorCode(this AppMessageType msg)
        {
            string[] split = Regex.Split($"{msg}", "(?<!^)(?=[A-Z])");
            int msgId = (int)msg;
            return $"{split[0].ToUpper()}_{msgId}";
        }
    }
}
