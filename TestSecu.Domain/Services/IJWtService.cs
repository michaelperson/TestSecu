using SecurityTools.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestSecu.Domain.Services
{
    public interface IJWtService
    {
        Task<string> GetToken(int UserID, JwtSettings jwtSetting);
    }
}
