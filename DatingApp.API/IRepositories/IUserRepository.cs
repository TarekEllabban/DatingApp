using DatingApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.IRepositories
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        bool UserExists(string userName);
    }
}
