using System;
using System.Collections.Generic;
using System.Text;

namespace TestSecu.Domain.Repositories
{
    public interface IAccountRepository
    {
        public Task<bool> Authenticate(string email, string password);
    }
}
