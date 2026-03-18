



//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.IdentityModel.Tokens;
//using RentAPlaceAPI.Models;

//namespace RentAPlaceAPI.Helpers
//{
//    public static class JwtHelper
//    {
//        public static string GenerateJwtToken(User user)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();

//            var key = Encoding.UTF8.GetBytes("ThisIsMySuperSecretJWTKeyForRentAPlaceApplication12345");

//            var claims = new[]
//            {
//                new Claim("Id", user.Id.ToString()),
//                new Claim(ClaimTypes.Role, user.Role)
//            };

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(claims),

//                Expires = DateTime.UtcNow.AddHours(5),

//                SigningCredentials = new SigningCredentials(
//                    new SymmetricSecurityKey(key),
//                    SecurityAlgorithms.HmacSha256Signature)
//            };

//            var token = tokenHandler.CreateToken(tokenDescriptor);

//            return tokenHandler.WriteToken(token);
//        }
//    }
//}



using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RentAPlaceAPI.Models;

namespace RentAPlaceAPI.Helpers
{
    public static class JwtHelper
    {
        public static string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes("ThisIsMySuperSecretJWTKeyForRentAPlaceApplication12345");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddHours(5),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}