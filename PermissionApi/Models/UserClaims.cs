namespace PermissionApi.Models
{
    public class UserClaims
    {
        public Guid Id { get; set; }
        public PermissionEnum Permission { get; set; }
    }
}
