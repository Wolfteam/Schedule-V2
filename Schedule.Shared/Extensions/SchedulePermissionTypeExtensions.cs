using Schedule.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Schedule.Shared.Extensions
{
    public static class SchedulePermissionTypeExtensions
    {
        public static string GetPermissionStringValue(this SchedulePermissionType permission)
            => ((int)permission).ToString();

        public static bool IsThisPermissionAllowed(this string permission, string permissionName)
        {
            if (!int.TryParse(permission, out int currentPermission))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(permission),
                    permission,
                    "The current permission couldn't be converted to a valid integer value.");
            }

            if (!int.TryParse(permissionName, out int requiredPermission))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(permissionName),
                    permissionName,
                    "The required permission couldn't be converted to a valid integer value.");
            }

            var exists = new List<int>(Enum.GetValues(typeof(SchedulePermissionType)).Cast<int>())
                .Any(enumValue =>
                    enumValue != 0 &&
                    (enumValue & requiredPermission) == enumValue &&
                    (requiredPermission & currentPermission) == requiredPermission);

            return exists;
        }

        public static bool UserHasThisPermission(this ClaimsPrincipal user, SchedulePermissionType permission)
        {
            var permissionClaim = user?.Claims.SingleOrDefault(x => x.Type == AppConstants.SchedulePermissionsClaim);
            if (permissionClaim == null || string.IsNullOrEmpty(permissionClaim.Value))
                return false;
            return permissionClaim.Value.IsThisPermissionAllowed(((int)permission).ToString());
        }

        public static List<string> GetUserPermissions(this ClaimsPrincipal user)
        {
            var permissions = new List<string>();
            var permissionClaim = user?.Claims.SingleOrDefault(x => x.Type == AppConstants.SchedulePermissionsClaim);
            if (permissionClaim == null || string.IsNullOrEmpty(permissionClaim.Value))
                return permissions;

            var current = (SchedulePermissionType)int.Parse(permissionClaim.Value);

            return current.ToString().Split(',').ToList();
        }

        public static bool IsPermissionsClaimPresent(this IList<Claim> claims)
            => claims.Any(c => c.Type == AppConstants.SchedulePermissionsClaim);

        public static bool IsPermissionsClaimPresent(this IList<Claim> claims, SchedulePermissionType permission)
            => claims.Any(c => c.Type == AppConstants.SchedulePermissionsClaim && c.Value == permission.GetPermissionStringValue());

        public static Claim GetPermissionClaim(this IList<Claim> claims) =>
            claims.First(c => c.Type == AppConstants.SchedulePermissionsClaim);

        public static bool IsPermissionAllowed(this SchedulePermissionType permission)
        {
            return (permission & SchedulePermissionType.All) == permission;
        }

        public static SchedulePermissionType ToPermissionType(this Claim claim)
            => claim.Value.ToPermissionType();

        public static SchedulePermissionType ToPermissionType(this string val)
        {
            if (!int.TryParse(val, out int permission))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(val),
                    val,
                    "The provided permission couldn't be converted to a valid integer value.");
            }

            return (SchedulePermissionType)permission;
        }
    }
}
