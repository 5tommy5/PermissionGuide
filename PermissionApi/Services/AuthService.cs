using Microsoft.EntityFrameworkCore;
using PermissionApi.Entities;
using PermissionApi.Models;

namespace PermissionApi.Services
{
    public class AuthService
    {
        private readonly ApplicationContext _context;
        private readonly TokenService _tokenService;
        private readonly PermissionService _permissionService;
        public AuthService(ApplicationContext context, TokenService tokenService, PermissionService permissionService)
        {
            _context = context;
            _tokenService = tokenService;
            _permissionService = permissionService;
        }
        public async Task<TokenModel> SignIn(SignInModel model)
        {
            var user = await _context.Users.Include(x=> x.Permissions).FirstOrDefaultAsync(x=> x.Email == model.Email);

            if (user.Password == model.Password)
            {
                return _tokenService.CreateToken(user.Id.ToString(), _permissionService.GetPermissions(user.Permissions));
            }

            throw new ArgumentException("Could not find user.");
        }

        public async Task<TokenModel> SignUp(SignUpModel model)
        {
            var user = new User
            {
                Email = model.Email,
                Password = model.Password,
                Name = model.Name,
            };

            await _context.Users.AddAsync(user);

            var permission = new UserPermissions
            {
                StandartSubscription = true,
                UserId = user.Id
            };
            user.Permissions = permission;

            await _context.Permissions.AddAsync(permission);

            await _context.SaveChangesAsync();

            return _tokenService.CreateToken(user.Id.ToString(), _permissionService.GetPermissions(user.Permissions));
        }
    }
}
