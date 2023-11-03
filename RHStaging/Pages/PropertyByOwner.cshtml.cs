
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
    public class PropertyByOwner : PageModel
    {
        private readonly PropMgmtContext _context;
        public IList<OwnerPropertyInfo> PropsGroupbyOwner { get; set; }

        public PropertyByOwner(PropMgmtContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            IQueryable<OwnerPropertyInfo> data =
                from owner in _context.Owners
                join property in _context.Properties on owner.OwnerID equals property.OwnerID
                group property by owner into ownerGroup
                select new OwnerPropertyInfo()
                {
                    //OwnerID = .,
                    OwnerName = ownerGroup.Key.FullName,

                    PropertyCount = ownerGroup.Count()
                };

            PropsGroupbyOwner = await data.AsNoTracking().ToListAsync();
        }
    }
}