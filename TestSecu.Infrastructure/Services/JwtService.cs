using SecurityTools;
using SecurityTools.Interfaces;
using SecurityTools.Models;
using System;
using System.Collections.Generic;
using System.Text;
using TestSecu.Domain;
using TestSecu.Domain.Repositories;
using TestSecu.Domain.Services;

namespace TestSecu.Infrastructure.Services
{
    public class JwtService : IJWtService
    {
        private readonly IAccountRepository _accountRepository;

        public JwtService(IAccountRepository accountRepository)
        {
            _accountRepository=accountRepository;
        }
        public async Task<string> GetToken(int UserID, JwtSettings jwtSetting)
        {
            User u = await _accountRepository.GetById(UserID);
            Dictionary<string, string> infos = new Dictionary<string, string>();
            if (u.Id == 2)
                infos.Add("Level", "9");
            else

                infos.Add("Level", "10");
            UserInfo ui = new UserInfo()
            {
                Id = u.Id.ToString(),
                Roles = new List<string> { "Admin", "Manager" },
                MetaData = infos

            };
            return JwtHelper.Generate(jwtSetting, ui as IUserInfo);
        }
    }
}
