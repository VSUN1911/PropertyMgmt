using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RHStaging.Data;
using RHStaging.Models;

namespace RHStaging.Pages.Owners
{
    public class DetailsModel : PageModel
    {
        private readonly RHStaging.Data.PropMgmtContext _context;

        public DetailsModel(RHStaging.Data.PropMgmtContext context)
        {
            _context = context;
        }

      public Owner Owner { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Owners == null)
            {
                return NotFound();
            }

            //var owner = await _context.Owners.FirstOrDefaultAsync(m => m.ID == id);

            Owner = await _context.Owners                
                .Include(o => o.Properties)
                .ThenInclude(o => o.Leases)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.OwnerID == id); 

            if (Owner == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
