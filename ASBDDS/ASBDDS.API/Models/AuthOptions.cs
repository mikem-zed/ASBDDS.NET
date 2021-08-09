using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ASBDDS.API.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "ASBDDS API"; // издатель токена
        public const string AUDIENCE = "ASBDDS CLIENT"; // потребитель токена
        const string KEY = "MySuperSecretCegthFdnjvfnb223";   // ключ для шифрации
        public const int LIFETIME = 60; // время жизни токена - 60 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}