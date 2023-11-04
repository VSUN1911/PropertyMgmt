using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RHStaging.Data;
using RHStaging.Models;

namespace RHStaging.Pages.Renters
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
        public IList<Renter> Renters { get;set; } = default!;

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

            IQueryable<Renter> rentersIQ = from r in _context.Renters
                                         select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                rentersIQ = rentersIQ.Where(r => r.LastName.ToUpper().Contains(CurrentFilter.ToUpper())
                                    || r.FirstMidName.ToUpper().Contains(CurrentFilter.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    rentersIQ = rentersIQ.OrderByDescending(o => o.LastName);
                    break;
                case "Date":
                    rentersIQ = rentersIQ.OrderBy(o => o.MemberSince);
                    break;
                case "date_desc":
                    rentersIQ = rentersIQ.OrderByDescending(o => o.MemberSince);
                    break;
                default:
                    rentersIQ = rentersIQ.OrderBy(o => o.LastName);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 5);

            Renters = await PaginatedList<Renter>.CreateAsync(
                rentersIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
