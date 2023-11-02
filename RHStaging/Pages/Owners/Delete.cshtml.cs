using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RHStaging.Data;
using RHStaging.Models;
using Microsoft.Extensions.Logging; 

namespace RHStaging.Pages.Owners
{
    public class DeleteModel : PageModel
    {
        private readonly RHStaging.Data.PropMgmtContext _context;
        private readonly ILogger _logger;

        public DeleteModel(RHStaging.Data.PropMgmtContext context,
                           ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger; 
        }

        [BindProperty]
        public Owner Owner { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null || _context.Owners == null)
            {
                return NotFound();
            }

            Owner = await _context.Owners
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.OwnerID == id);

            if (Owner == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {ID} failed. Try again!", id);
            }

            return Page();
        }

        //    var owner = await _context.Owners.FirstOrDefaultAsync(m => m.OwnerID == id);

        //    if (owner == null)
        //    {
        //        return NotFound();
        //    }
        //    else 
        //    {
        //        Owner = owner;
        //    }
        //    return Page();
        //}

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners.FindAsync(id);

            if (owner == null) 
            {
                return NotFound(); 
            }

            try
            {
                _context.Owners.Remove(owner);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index"); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessage);

                return RedirectToPage("./Delete",
                    new { id, saveChangeError = true });
            }

            //if (owner != null)
            //{
            //    Owner = owner;
            //    _context.Owners.Remove(Owner);
            //    await _context.SaveChangesAsync();
            //}

            //return RedirectToPage("./Index");
        }
    }
}
