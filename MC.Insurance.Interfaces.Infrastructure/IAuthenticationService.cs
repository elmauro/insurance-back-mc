using MC.Insurance.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MC.Insurance.Interfaces.Infrastructure
{
    public interface IAuthenticationService
    {
        Task<User> Login(string userName, string password);
    }
}
