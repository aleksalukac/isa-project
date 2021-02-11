using Pharmacy.Models.Entities;
using Pharmacy.Models.Entities.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public interface IUserService
    {
        public Task<string> GetUserRole(string id);
        public Task<List<AppUser>> GetByList(List<string> idList);
        public Task<AppUser> GetById(string id);
        public Task<int> Update(AppUser user);
        bool Exists(string id);
        public Task<List<AppUser>> GetAllByRole(string roleName);
        public void Create(AppUser user);
    }
}