using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TestSecu.Domain;
using TestSecu.Domain.Entities;
using TestSecu.Domain.Repositories;
using TestSecu.Infrastructure.Persistence;

namespace TestSecu.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        ////Fakes users - remplacés par la suite par l'accès DB
        //List<User> users = new List<User>()
        //    {
        //        new User() { Id=1, Email="zorro@tftic.be", Password="Test1234=!" },
        //        new User() { Id=2, Email="tornado@tftic.be", Password="SecureAdmin789!" },

        //    };

       private readonly TestSecuDatacontext _ctx;

        public AccountRepository(string cnstr)
        {
            _ctx = new TestSecuDatacontext(cnstr);
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            if (email is null || password is null) return false;
            Regex reg = new Regex("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");
            if (!reg.IsMatch(email)) return false;
            Regex regPassword = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{10,}$");
            if (!regPassword.IsMatch(password)) return false;


            
             
            return _ctx.Users.FirstOrDefault(m => m.Email == email && m.Password == password) != default;

        }

        public async  Task<User> GetByEmail(string email)
        {
            if (email is null ) return default;
            Regex reg = new Regex("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");
            if (!reg.IsMatch(email)) return default;
            UserEntity ue = _ctx.Users.FirstOrDefault(m => m.Email == email);
            return new User()
            {
                Id = ue.Id,
                Email = ue.Email
            };
        }

        public async Task<User> GetById(int userID)
        {
            if (userID <= 0) return default;
             UserEntity ue = _ctx.Users.FirstOrDefault(m => m.Id == userID);
            return new User()
            {
                Id = ue.Id,
                Email = ue.Email
            };
        }
    }
}
