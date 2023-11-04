using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RHStaging.Data;
using RHStaging.Models;
using Microsoft.Extensions.Configuration;

namespace RHStaging.Pages.Properties
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

        //public string NameSort { get; set; }
        //public string DateSort { get; set; }

        public string AddressSort { get; set; }
        public string CitySort { get; set; }
        public string OwnerSort { get; set; }
        public string PropertyIDSort { get; set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Property> Properties { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            //NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            AddressSort = String.IsNullOrEmpty(sortOrder) ? "address_desc" : "";
            OwnerSort = sortOrder == "Date" ? "owner_desc" : "Date";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Property> propertiesIQ = from p in _context.Properties
                                         select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                propertiesIQ = propertiesIQ.Where(p => p.Address.ToUpper().Contains(CurrentFilter.ToUpper())
                                    || p.City.ToUpper().Contains(CurrentFilter.ToUpper()));
            }

            switch (sortOrder)
            {
                case "address_desc":
                    propertiesIQ = propertiesIQ.OrderByDescending(p => p.Address);
                    break;

                case "Date":
                    propertiesIQ = propertiesIQ.OrderBy(p => p.OwnerID);
                    break;

                case "owner_desc":
                    propertiesIQ = propertiesIQ.OrderByDescending(p => p.OwnerID);
                    break;

                default:
                    propertiesIQ = propertiesIQ.OrderBy(p => p.PropertyID);
                    break;

                    //case "name_desc":
                    //    propertiesIQ = propertiesIQ.OrderByDescending(p => p.Address);
                    //    break;
                    //case "Date":
                    //    propertiesIQ = propertiesIQ.OrderBy(p => p.City);
                    //    break;
                    //case "date_desc":
                    //    propertiesIQ = propertiesIQ.OrderByDescending(p => p.OwnerID);
                    //    break;
                    //default:
                    //    propertiesIQ = propertiesIQ.OrderBy(p => p.PropertyID);
                    //    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 5);

            Properties = await PaginatedList<Property>.CreateAsync(
                propertiesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);




            //Properties = await _context.Properties
            //    .Include(o => o.Owner)
            //    .ThenInclude(o => o.Leases)
            //    .AsNoTracking()
            //    .ToListAsync();

            //if (_context.Properties != null)
            //{
            //    Properties = await _context.Properties.ToListAsync();
            //}
        }
    }
}
