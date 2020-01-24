using DatingApp.API.Commands;
using DatingApp.API.ViewModels;
using System.Threading.Tasks;

namespace DatingApp.API.IServices
{
    public interface IAuthService
    {
        Task<ResponseViewModel> RegisterAsync(RegisterCommand registerCommand);
        Task<ResponseViewModel> LoginAsync(LoginCommand loginCommand);

    }
}
