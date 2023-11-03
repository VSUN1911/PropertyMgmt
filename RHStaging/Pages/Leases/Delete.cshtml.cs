using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RHStaging.Data;
using RHStaging.Models;

namespace RHStaging.Pages.Leases
{
    public class DeleteModel : PageModel
    {
        private readonly RHStaging.Data.PropMgmtContext _context;

        public DeleteModel(RHStaging.Data.PropMgmtContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Lease Lease { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Leases == null)
            {
                return NotFound();
            }

            var lease = await _context.Leases.FirstOrDefaultAsync(m => m.LeaseID == id);

            if (lease == null)
            {
                return NotFound();
            }
            else 
            {
                Lease = lease;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Leases == null)
            {
                return NotFound();
            }
            var lease = await _context.Leases.FindAsync(id);

            if (lease != null)
            {
                Lease = lease;
                _context.Leases.Remove(Lease);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
