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

namespace RHStaging.Pages.Renters
{
    public class EditModel : PageModel
    {
        private readonly RHStaging.Data.PropMgmtContext _context;

        public EditModel(RHStaging.Data.PropMgmtContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Renter Renter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Renters == null)
            {
                return NotFound();
            }

            var renter =  await _context.Renters.FirstOrDefaultAsync(m => m.RenterID == id);
            if (renter == null)
            {
                return NotFound();
            }
            Renter = renter;
           ViewData["LeaseID"] = new SelectList(_context.Leases, "LeaseID", "LeaseID");
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

            _context.Attach(Renter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RenterExists(Renter.RenterID))
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

        private bool RenterExists(int id)
        {
          return _context.Renters.Any(e => e.RenterID == id);
        }
    }
}
