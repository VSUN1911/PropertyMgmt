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

namespace RHStaging.Pages.Owners
{
    public class EditModel : PageModel
    {
        private readonly RHStaging.Data.PropMgmtContext _context;

        public EditModel(RHStaging.Data.PropMgmtContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Owner Owner { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Owners == null)
            {
                return NotFound();
            }

            Owner = await _context.Owners.FindAsync(id); 

            if (Owner == null)
            {
                return NotFound();
            }
            return Page(); 

            //var owner =  await _context.Owners.FirstOrDefaultAsync(m => m.OwnerID == id);
            //if (owner == null)
            //{
            //    return NotFound();
            //}
            //Owner = owner;
            //return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var ownerToUpdate = await _context.Owners.FindAsync(id); 

            if (ownerToUpdate == null)
            {
                return NotFound(); 
            }

            if (await TryUpdateModelAsync<Owner>(
                ownerToUpdate,
                "owner",
                o => o.FirstMidName, o => o.LastName, 
                o => o.MemberSince, o => o.Phone, o => o.Email))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index"); 
            }

            return Page(); 

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_context.Attach(Owner).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!OwnerExists(Owner.OwnerID))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return RedirectToPage("./Index");
        }

        private bool OwnerExists(int id)
        {
          return _context.Owners.Any(e => e.OwnerID == id);
        }
    }
}
