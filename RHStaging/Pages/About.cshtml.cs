using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RHStaging.Models.PropMgmtViewModels;
using RHStaging.Data;
using RHStaging.Models; 
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RHStaging.Pages
{
    public class AboutModel : PageModel
    {
        private readonly PropMgmtContext _context; 

        public AboutModel(PropMgmtContext context)
        {
            _context = context;
        }

        public IList<LeaseDateGroup> Properties { get; set; } 
        public async Task OnGetAsync()
        {
            IQueryable<LeaseDateGroup> data =
                from property in _context.Leases
                group property by property.Lease_end_date into dateGroup
                select new LeaseDateGroup()
                {
                    LeaseEndDate = dateGroup.Key,
                    PropertyIDList = dateGroup.ToList().Select(i => i.PropertyID).ToArray(),
                    LeaseIDList = dateGroup.ToList().Select(l => l.LeaseID).ToArray(),
                    PropertyCount = dateGroup.Count()
                };

            Properties = await data.AsNoTracking().ToListAsync(); 
        }
    }
}
