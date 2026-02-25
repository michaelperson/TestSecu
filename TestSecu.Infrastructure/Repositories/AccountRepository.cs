using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TestSecu.Domain;
using TestSecu.Domain.Repositories;

namespace TestSecu.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<bool> Authenticate(string email, string password)
        {
            if (email is null || password is null) return false;
            Regex reg = new Regex("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");
            if (!reg.IsMatch(email)) return false;
            Regex regPassword = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{10,}$");
            if (!regPassword.IsMatch(password)) return false;


            //Fakes users
            List<User> users = new List<User>()
            {
                new User() { Id=1, Email="zorro@tftic.be", Password="Test1234=!" },
                new User() { Id=2, Email="tornado@tftic.be", Password="SecureAdmin789!" },

            };

            return users.FirstOrDefault(m => m.Email == email && m.Password == password) != default;

        }
    }
}
