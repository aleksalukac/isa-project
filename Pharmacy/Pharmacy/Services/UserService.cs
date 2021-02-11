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

        public async Task<int> Update(AppUser user)
        {
            _context.Update(user);

            return await _context.SaveChangesAsync();
        }

        public bool Exists(string id)
        {
            return _context.AppUsers.Any(e => e.Id == id);
        }

        public async Task<List<AppUser>> GetAllByRole(string roleName)
        {
            return await (from user in _context.tbAppUsers
             join userrole in _context.UserRoles on user.Id equals userrole.UserId
             join role in _context.Roles on userrole.RoleId equals role.Id
             where role.Name == roleName
             select user).ToListAsync();
        }

        public async void Create(AppUser user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
