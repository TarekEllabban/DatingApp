using AutoMapper;
using DatingApp.API.Commands;
using DatingApp.API.Models;
using DatingApp.API.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DatingApp.API.DBContext
{
    public class Seed
    {
        private IConfiguration _config;
        private DatingContext _datingContext;
        private HttpClient _httpClient;
        private IMapper _mapper;


        public Seed(DatingContext datingContext, IMapper mapper, IConfiguration configuration)
        {
            this._config = configuration;
            this._datingContext = datingContext;
            this._httpClient = HttpClientFactory.Create();
            this._httpClient.BaseAddress = new Uri(this._config.GetValue<string>("IdentityBaseUrl", string.Empty));
            this._mapper = mapper;
        }
        public async Task SeedDataAsync()
        {
            var userData = System.IO.File.ReadAllText("DBContext/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<UserForSeedViewModel>>(userData);
            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                var responseString = await _httpClient.PostAsJsonAsync("api/account/register", new RegisterCommand(user.UserName, user.Password));
                var response = JsonConvert.DeserializeObject<ResponseViewModel>(await responseString.Content.ReadAsStringAsync());
                if (response.IsSucceeded)
                {
                    var userToBeAdded = _mapper.Map<User>(user);
                    var jData = JObject.Parse(response.Data);
                    userToBeAdded.IdentityId = jData["IdentityId"].ToString();
                    _datingContext.Users.Add(userToBeAdded);
                }
            }
            _datingContext.SaveChanges();
        }
    }
}
