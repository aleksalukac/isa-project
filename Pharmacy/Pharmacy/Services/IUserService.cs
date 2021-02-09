using Pharmacy.Models.Entities.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public interface IUserService
    {
        public Task<string> GetUserRole(string id);
        public Task<List<AppUser>> GetByList(List<string> idList);
    }
}