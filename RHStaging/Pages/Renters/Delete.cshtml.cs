using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RHStaging.Data;
using RHStaging.Models;

namespace RHStaging.Pages.Renters
{
    public class DeleteModel : PageModel
    {
        private readonly RHStaging.Data.PropMgmtContext _context;

        public DeleteModel(RHStaging.Data.PropMgmtContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Renter Renter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Renters == null)
            {
                return NotFound();
            }

            var renter = await _context.Renters.FirstOrDefaultAsync(m => m.RenterID == id);

            if (renter == null)
            {
                return NotFound();
            }
            else 
            {
                Renter = renter;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Renters == null)
            {
                return NotFound();
            }
            var renter = await _context.Renters.FindAsync(id);

            if (renter != null)
            {
                Renter = renter;
                _context.Renters.Remove(Renter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
