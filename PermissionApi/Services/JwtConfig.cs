using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PermissionApi.Services
{
    public class JwtConfig
    {
        private static readonly JwtConfig _instance = new JwtConfig();
        public static JwtConfig Current => _instance;

        private JwtConfig()
        {

        }
        public string SecretKey => "mysupersecret_secretkey!9871231111";
        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public string SigningAlgorithm => SecurityAlgorithms.HmacSha256;

        public string Issuer => "TestApp";
        public string Audience => "http://127.0.0.1";

        public TimeSpan Lifetime => TimeSpan.FromMinutes(30);
    }
}
