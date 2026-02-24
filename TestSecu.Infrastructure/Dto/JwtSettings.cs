using System.Security.Cryptography.X509Certificates;

namespace TestSecu.Infrastructure.Dto
{
    public class JwtSettings
    {
        /// <summary>
        /// Correspond au scope d'utilisation du token
        /// </summary>
        public required string Audience { get; set; }

        /// <summary>
        /// Correspond au "fournisseur" du token
        /// </summary>
        public required string Issuer { get; set;  }

        /// <summary>
        /// Correspond au temps de validité du token exprimé en minute
        /// </summary>
        public required int LifeTime { get; set; }

        /// <summary>
        /// Correspond à la clé secrete
        /// </summary>
        public required string SecretKey { get; set; }
    }
}
