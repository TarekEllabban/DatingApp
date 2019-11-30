using DatingApp.API.Commands;
using DatingApp.API.Models;
using DatingApp.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Services
{
    public interface IAuthService
    {
        Task<ResponseViewModel> RegisterAsync(RegisterCommand registerCommand);
        Task<ResponseViewModel> LoginAsync(LoginCommand loginCommand);

    }
}
