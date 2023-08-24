using PermissionApi.Entities;
using PermissionApi.Models;

namespace PermissionApi.Services
{
    public class PermissionService
    {
        public PermissionEnum GetPermissions(UserPermissions permissions)
        {
            var permissionsList = PermissionEnum.None;

            if(permissions.IsAdmin)
            {
                permissionsList = permissionsList 
                    | PermissionEnum.Get | PermissionEnum.Delete | PermissionEnum.Add 
                    | PermissionEnum.Update | PermissionEnum.Special;
            }
            else if (permissions.UltraSubscription)
            {
                permissionsList = permissionsList
                    | PermissionEnum.Get | PermissionEnum.Delete | PermissionEnum.Add
                    | PermissionEnum.Update;
            }
            else if(permissions.PremiumSubscription)
            {
                permissionsList = permissionsList
                    | PermissionEnum.Get | PermissionEnum.Add
                    | PermissionEnum.Update;
            }
            else if(permissions.StandartSubscription)
            {
                permissionsList = permissionsList
                    | PermissionEnum.Get | PermissionEnum.Add;
            }

            return permissionsList;

        }
    }
}
