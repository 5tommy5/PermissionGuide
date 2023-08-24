using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PermissionApi.Models;

namespace PermissionApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly UserClaims _claims;
        public UserController(ApplicationContext context, UserClaims claims)
        {
            _context = context;
            _claims = claims;
        }

        [HttpPost]
        public async Task ChangeSubsription(ChangePermissionModel model)
        {
            var permissions = await _context.Permissions.FirstOrDefaultAsync(x=> x.UserId == _claims.Id);

            permissions.StandartSubscription = model.StandartSubscription;
            permissions.PremiumSubscription = model.PremiumSubscription;
            permissions.UltraSubscription = model.UltraSubscription;
            permissions.IsAdmin = model.IsAdmin;

            await _context.SaveChangesAsync();
        }
    }
}
