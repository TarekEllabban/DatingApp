using DatingApp.API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.API.IServices
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser(int id);
        Task<IEnumerable<UserDetailsViewModel>> GetUsers();
    }
}
