using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RHStaging.Data;
using RHStaging.Models;

namespace RHStaging.Pages.Owners
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
            return Page();
        }

        [BindProperty]
        public Owner Owner { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyOwner = new Owner(); 

            if (await TryUpdateModelAsync<Owner>(
                emptyOwner,
                "owner",   // Prefix for form value
                o => o.FirstMidName, o => o.LastName,
                o => o.MemberSince,
                o => o.Email, o => o.Phone
                ))
            {
                _context.Owners.Add(emptyOwner);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
