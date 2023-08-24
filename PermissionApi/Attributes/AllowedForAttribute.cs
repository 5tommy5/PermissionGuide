using PermissionApi.Models;

namespace PermissionApi.Attributes
{
    public class AllowedForAttribute : Attribute
    {
        public IEnumerable<PermissionEnum> AllowedFor { get; set; }
        public AllowedForAttribute(params PermissionEnum[] rights)
        {
            AllowedFor = rights;
        }

    }
}
