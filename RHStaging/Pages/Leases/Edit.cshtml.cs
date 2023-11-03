using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RHStaging.Data;
using RHStaging.Models;

namespace RHStaging.Pages.Leases
{
    public class EditModel : PageModel
    {
        private readonly RHStaging.Data.PropMgmtContext _context;

        public EditModel(RHStaging.Data.PropMgmtContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Lease Lease { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Leases == null)
            {
                return NotFound();
            }

            var lease =  await _context.Leases.FirstOrDefaultAsync(m => m.LeaseID == id);
            if (lease == null)
            {
                return NotFound();
            }
            Lease = lease;
           ViewData["PropertyID"] = new SelectList(_context.Properties, "PropertyID", "PropertyID");
           ViewData["RenterID"] = new SelectList(_context.Renters, "RenterID", "RenterID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Lease).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaseExists(Lease.LeaseID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LeaseExists(int id)
        {
          return _context.Leases.Any(e => e.LeaseID == id);
        }
    }
}
