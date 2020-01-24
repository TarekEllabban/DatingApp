using DatingApp.API.DBContext;
using DatingApp.API.IRepositories;
using DatingApp.API.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DatingContext _datingContext;
        public UserRepository(DatingContext datingContext)
        {
            this._datingContext = datingContext;
        }
        public async Task<User> Add(User user)
        {
            await _datingContext.AddAsync(user);
            await SaveChanges();
            return user;
        }

        public bool UserExists(string userName)
        {
            return _datingContext.Users.Where(u => u.UserName == userName).Any();
        }

        #region Private Methods
        private async Task SaveChanges()
        {
            await _datingContext.SaveChangesAsync();
        }
        #endregion
    }
}
