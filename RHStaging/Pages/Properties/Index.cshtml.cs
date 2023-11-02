using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RHStaging.Data;
using RHStaging.Models;

namespace RHStaging.Pages.Properties
{
    public class IndexModel : PageModel
    {
        private readonly RHStaging.Data.PropMgmtContext _context;

        public IndexModel(RHStaging.Data.PropMgmtContext context)
        {
            _context = context;
        }

        public IList<Property> Properties { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Properties = await _context.Properties
                .Include(o => o.Owner)
                .ThenInclude(o => o.Leases)
                .AsNoTracking()
                .ToListAsync();

            //if (_context.Properties != null)
            //{
            //    Properties = await _context.Properties.ToListAsync();
            //}
        }
    }
}
