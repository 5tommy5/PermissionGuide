using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionApi.Entities
{
    public class UserPermissions
    {
        public Guid Id { get; set; }
        public bool IsAdmin { get; set; }
        public bool StandartSubscription { get; set; }
        public bool PremiumSubscription { get; set; }
        public bool UltraSubscription { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
