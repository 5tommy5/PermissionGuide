namespace PermissionApi.Models
{
    [Flags]
    public enum PermissionEnum : int
    {
        None = 0,
        Add = 1,
        Update = 1 << 1,
        Delete = 1 << 2,
        Get = 1 << 3,
        Special = 1 << 4
    }
}
