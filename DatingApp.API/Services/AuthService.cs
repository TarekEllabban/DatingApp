using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DatingApp.API.Commands;
using DatingApp.API.IServices;
using DatingApp.API.Models;
using DatingApp.API.ViewModels;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DatingApp.API.Services
{
    public class AuthService : IAuthService
    {
        private HttpClient _httpClient;
        private IUserRepository _userRepository;
        private IConfiguration _configuration;
        public AuthService(HttpClient httpClient, IUserRepository userRepository, IConfiguration configuration)
        {
            this._httpClient = httpClient;
            this._userRepository = userRepository;
            this._configuration = configuration;
        }
        public async Task<ResponseViewModel> RegisterAsync(RegisterCommand registerCommand)
        {
            ResponseViewModel response = new ResponseViewModel();
            if (!string.IsNullOrEmpty(registerCommand.Username) && !string.IsNullOrEmpty(registerCommand.Password))
            {
                registerCommand.Username = registerCommand.Username.ToLower();
                response.IsSucceeded = !_userRepository.UserExists(registerCommand.Username);
                if (response.IsSucceeded)
                {
                    var responseString = await _httpClient.PostAsJsonAsync("api/account/register", registerCommand);
                    response = JsonConvert.DeserializeObject<ResponseViewModel>(await responseString.Content.ReadAsStringAsync());
                    if (response.IsSucceeded)
                    {
                        JObject jData = JObject.Parse(response.Data);
                        await _userRepository.Add(new User() { IdentityId = jData["IdentityId"].ToString(), UserName = registerCommand.Username });
                    }
                }
                else
                {
                    response.Message = "User is already exist";
                }

            }
            else
            {
                response.IsSucceeded = false;
                response.Message = "Invalid Input";
            }
            return response;
        }
        public async Task<ResponseViewModel> LoginAsync(LoginCommand loginCommand)
        {
            ResponseViewModel response = new ResponseViewModel();
            if (!string.IsNullOrEmpty(loginCommand.Username) && !string.IsNullOrEmpty(loginCommand.Password))
            {
                loginCommand.Username = loginCommand.Username.ToLower();
                response.IsSucceeded = _userRepository.UserExists(loginCommand.Username);
                if (response.IsSucceeded)
                {
                    var disco = await _httpClient.GetDiscoveryDocumentAsync(_httpClient.BaseAddress.ToString());
                    response.IsSucceeded = !disco.IsError;
                    if (response.IsSucceeded)
                    {
                        var tokenResponse = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
                        {
                            Address = disco.TokenEndpoint,

                            ClientId = _configuration["IdentityClientId"],
                            ClientSecret = _configuration["IdentityClientSecret"],
                            Scope = "DatingApp.API",
                            UserName = loginCommand.Username,
                            Password = loginCommand.Password
                        });
                        response.IsSucceeded = !tokenResponse.IsError;
                        if (response.IsSucceeded)
                        {
                            response.Data = tokenResponse.Json["access_token"].ToString();
                        }
                        else
                        {
                            response.Message = "Invalid Username or password";
                        }
                    }
                    else
                    {
                        response.Message = "An error has been occured";
                    }
                }
                else
                {
                    response.Message = "Invalid Username or password";
                }
            }
            else
            {
                response.IsSucceeded = false;
                response.Message = "Invalid Input";
            }
            return response;
        }

    }
}
