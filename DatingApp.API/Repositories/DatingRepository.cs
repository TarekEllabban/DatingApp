using DatingApp.API.DBContext;
using DatingApp.API.IRepositories;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Repositories
{
    public class DatingRepository : IDatingRepository
    {
        private DatingContext _datingContext;

        public DatingRepository(DatingContext datingContext)
        {
            this._datingContext = datingContext;
        }
        public void Add<T>(T entity) where T : class
        {
            this._datingContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            this._datingContext.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            return await this._datingContext.Users.Where(u => u.Id == id).Include(u => u.Photos).FirstOrDefaultAsync();
        }

        public async Task<User> GetUser(string username)
        {
            return await this._datingContext.Users.Where(u => u.UserName == username).Include(u => u.Photos).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await this._datingContext.Users.Include(u => u.Photos).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return (await this._datingContext.SaveChangesAsync()) > 0;
        }
    }
}
