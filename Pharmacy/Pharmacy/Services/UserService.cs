using Microsoft.AspNetCore.Identity;
using Pharmacy.Data;
using Pharmacy.Models.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserService(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<string> GetUserRole(string id)
        {
            List<string> userRole = await (from userrole in _context.UserRoles
                                            join role in _context.Roles on userrole.RoleId equals role.Id
                                            where userrole.UserId == id
                                            select role.Name).ToListAsync();

            return userRole.FirstOrDefault();
        }

        public async Task<AppUser> GetById(string id)
        {
            return await _context.tbAppUsers.FindAsync(id);
        }

        public async Task<List<AppUser>> GetByList(List<string> idList)
        {
            List<AppUser> users = await _context.AppUsers
                                          .Where(l => idList.Any(id => id == l.Id))
                                          .ToListAsync();

            return users;
        }

        public async Task<bool> Update(AppUser user)
        {
            _context.Update(user);

            return true;
        }
    }
}
