using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RHStaging.Data;
using RHStaging.Models;
using Microsoft.Extensions.Configuration;
using Humanizer.Localisation;

namespace RHStaging.Pages.Leases
{
    public class IndexModel : PageModel
    {
        private readonly RHStaging.Data.PropMgmtContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(RHStaging.Data.PropMgmtContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Lease> Leases { get;set; } = default!;
        public PaginatedList<LeaseWithOwnerName> LeaseWithOwnerNames { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<LeaseWithOwnerName> leaseWithOwnerNameIQ =
                from lease in _context.Leases
                //join property in _context.Properties on lease.PropertyID equals property.PropertyID
                //join owner in _context.Owners on property.OwnerID equals owner.OwnerID
                //join renter in _context.Renters on lease.RenterID equals renter.RenterID
                select new LeaseWithOwnerName
                {
                    Lease = lease,
                    OwnerName = lease.Owner.FullName,
                    OwnerLastName = lease.Owner.LastName,
                    RenterName = lease.Renter.FirstMidName
                };


            if (!String.IsNullOrEmpty(searchString))
            {
                // can not use o.Property.Owner.FullName as that entity is not in DB column but a computed one
                leaseWithOwnerNameIQ = leaseWithOwnerNameIQ.Where(o => o.OwnerName.ToUpper().Contains(CurrentFilter.ToUpper())
                                  || o.OwnerName.ToUpper().Contains(CurrentFilter.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    leaseWithOwnerNameIQ = leaseWithOwnerNameIQ.OrderByDescending(l => l.OwnerLastName);
                    break;
                case "Date":
                    leaseWithOwnerNameIQ = leaseWithOwnerNameIQ.OrderBy(l => l.Lease.Lease_end_date);
                    break;
                case "date_desc":
                    leaseWithOwnerNameIQ = leaseWithOwnerNameIQ.OrderByDescending(l => l.Lease.Lease_end_date);
                    break;
                default:
                    leaseWithOwnerNameIQ = leaseWithOwnerNameIQ.OrderBy(l => l.OwnerLastName);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 5);

            LeaseWithOwnerNames = await PaginatedList<LeaseWithOwnerName>.CreateAsync(
                leaseWithOwnerNameIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
