using AutoMapper;
using DatingApp.API.IRepositories;
using DatingApp.API.IServices;
using DatingApp.API.ViewModels;
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
        public async Task<UserViewModel> GetUser(int id)
        {
            var user = await this._datingRepository.GetUser(id);
            return _mapper.Map<UserViewModel>(user);
        }
        public async Task<IEnumerable<UserDetailsViewModel>> GetUsers()
        {
            var users = await this._datingRepository.GetUsers();
            return _mapper.Map<List<UserDetailsViewModel>>(users);
        }
    }
}
