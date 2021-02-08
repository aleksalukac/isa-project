using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pharmacy.Data;
using Pharmacy.Models.DTO;
using Pharmacy.Models.Entities;
using Pharmacy.Models.Entities.Users;

namespace Pharmacy.Areas.Identity.Pages.Account.Manage
{
    public class AddAllergicDrugsModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;


        public AddAllergicDrugsModel(
            UserManager<AppUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public List<AllergicDrugsDTO> AlergicDrugs { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            AlergicDrugs = new List<AllergicDrugsDTO>();
            var allDrugs = await _context.tbDrugs.ToListAsync();
            var userAlergic = await _context.AppUsers.Include(x => x.AllergicDrugs).FirstAsync(x => x.Id == user.Id);
            foreach (var item in allDrugs)
            {
                if (userAlergic.AllergicDrugs.Contains(item))
                {
                    AlergicDrugs.Add(new AllergicDrugsDTO
                    {
                        DrugId = item.Id,
                        DrugName = item.Name,
                        Allergic = true
                    });
                }
                else
                {
                    AlergicDrugs.Add(new AllergicDrugsDTO
                    {
                        DrugId = item.Id,
                        DrugName = item.Name,
                        Allergic = false
                    });
                }
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync([Bind("AlergicDrugs")] List<AllergicDrugsDTO> AlergicDrugs)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userAlergic = await _context.AppUsers.Include(x => x.AllergicDrugs).FirstAsync(x => x.Id == user.Id);
            var drugAlergicIds = new List<long>();
            drugAlergicIds = userAlergic.AllergicDrugs.Select(x => x.Id).ToList();

            foreach (var item in AlergicDrugs)
            {
                if (item.Allergic && !drugAlergicIds.Contains(item.DrugId))
                {
                    drugAlergicIds.Add(item.DrugId);
                }
                if(!item.Allergic && drugAlergicIds.Contains(item.DrugId))
                {
                    drugAlergicIds.Remove(item.DrugId);
                }
            }

            user.AllergicDrugs = await _context.tbDrugs.Where(x => drugAlergicIds.Contains(x.Id)).ToListAsync();
            await _userManager.UpdateAsync(user);

            return Page();
        }
    }
}