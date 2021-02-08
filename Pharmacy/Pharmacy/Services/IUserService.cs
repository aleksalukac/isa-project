using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public interface IUserService
    {
        public Task<string> GetUserRole(string id);
    }
}