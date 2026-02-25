using Microsoft.IdentityModel.Tokens;
using SecurityTools.Interfaces; 
using SecurityTools.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecurityTools
{
    public static class JwtHelper
    {
        public static string Generate(JwtSettings settings,IUserInfo info )
        {
            // Obtenir la clé de sécurité
            // Ajouter le nuget Microsoft.IdentityModel.Tokens
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));

            //Créer ma signature à partir de la clé de sécurité
            SigningCredentials credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            //L'ajout de claims à mon token
            List<Claim> claims = new List<Claim> {
               //Ajouter l'id de mon use en tant que claims
               // en utilisant le nuget System.IdentityModel.Tokens.Jwt
               new Claim(JwtRegisteredClaimNames.Sub, info.Id)
            };

            //Ajouter les roles
            foreach (string role in info.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            //Ajoute les customs claims
            //!!!!!! PAS DE DONNEES SENSIBLES
            foreach (KeyValuePair<string,string> meta in info.MetaData)
            {
                claims.Add(new Claim(meta.Key, meta.Value));
            }

            //Générer finalement le token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(settings.LifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
