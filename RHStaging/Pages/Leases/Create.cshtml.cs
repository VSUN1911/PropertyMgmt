using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RHStaging.Data;
using RHStaging.Models;

namespace RHStaging.Pages.Leases
{
    public class CreateModel : PageModel
    {
        private readonly RHStaging.Data.PropMgmtContext _context;

        public CreateModel(RHStaging.Data.PropMgmtContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PropertyID"] = new SelectList(_context.Properties, "PropertyID", "PropertyID");
        ViewData["RenterID"] = new SelectList(_context.Renters, "RenterID", "RenterID");
            return Page();
        }

        [BindProperty]
        public Lease Lease { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Leases.Add(Lease);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
