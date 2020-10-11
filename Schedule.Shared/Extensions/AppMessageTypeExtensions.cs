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
                var v when v == AppMessageType.SchInvalidRequest || v == AppMessageType.IdsInvalidRequest => "Invalid Request",
                var v when v == AppMessageType.SchUnknownErrorOccurred || v == AppMessageType.IdsUnknownErrorOccurred => "Unknown error occurred",
                var v when v == AppMessageType.SchNotFound || v == AppMessageType.IdsNotFound => "The resource you were looking for was not found",
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
