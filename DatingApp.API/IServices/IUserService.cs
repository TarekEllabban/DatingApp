using DatingApp.API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.API.IServices
{
    public interface IUserService
    {
        Task<UserDetailsViewModel> GetUser(int id);
        Task<UserDetailsViewModel> GetUser(string email);
        Task<IEnumerable<UserViewModel>> GetUsers();
        Task<bool> UpdateUser(string username, UserForUpdateViewModel userForUpdate);
    }
}
