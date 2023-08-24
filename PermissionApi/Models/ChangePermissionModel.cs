namespace PermissionApi.Models
{
    public class ChangePermissionModel
    {
        public bool IsAdmin { get; set; }
        public bool StandartSubscription { get; set; }
        public bool PremiumSubscription { get; set; }
        public bool UltraSubscription { get; set; }
    }
}
