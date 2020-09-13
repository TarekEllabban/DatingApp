using AutoMapper;
using DatingApp.API.IRepositories;
using DatingApp.API.IServices;
using DatingApp.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.API.Services
{
    public class UserService : IUserService
    {
        private IDatingRepository _datingRepository;
        private IMapper _mapper;

        public UserService(IDatingRepository datingRepository, IMapper mapper)
        {
            this._datingRepository = datingRepository;
            this._mapper = mapper;
        }
        public async Task<UserDetailsViewModel> GetUser(int id)
        {
            var user = await this._datingRepository.GetUser(id);
            return _mapper.Map<UserDetailsViewModel>(user);
        }
        public async Task<UserDetailsViewModel> GetUser(string username)
        {
            var user = await this._datingRepository.GetUser(username);
            return _mapper.Map<UserDetailsViewModel>(user);
        }
        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            var users = await this._datingRepository.GetUsers();
            return _mapper.Map<List<UserViewModel>>(users);
        }

        public async Task<bool> UpdateUser(string username, UserForUpdateViewModel userForUpdate)
        {
            var user = await _datingRepository.GetUser(username);
            if(user == null)
            {
                throw new Exception($"user with id {username} is not found");
            }
            _mapper.Map(userForUpdate, user);
            return await _datingRepository.SaveAll();
        }
    }
}
