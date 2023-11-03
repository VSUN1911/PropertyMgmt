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
    public class DetailsModel : PageModel
    {
        private readonly RHStaging.Data.PropMgmtContext _context;

        public DetailsModel(RHStaging.Data.PropMgmtContext context)
        {
            _context = context;
        }

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
    }
}
