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

namespace RHStaging.Pages.Owners
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
      
        public PaginatedList<Owner> Owners { get;set; } = default!;

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

            IQueryable<Owner> ownersIQ = from o in _context.Owners
                                         select o; 

            if(!String.IsNullOrEmpty(searchString) )
            {
                ownersIQ = ownersIQ.Where( o => o.LastName.ToUpper().Contains(CurrentFilter.ToUpper())
                                    || o.FirstMidName.ToUpper().Contains(CurrentFilter.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    ownersIQ = ownersIQ.OrderByDescending(o => o.LastName); 
                    break;
                case "Date":
                    ownersIQ = ownersIQ.OrderBy(o => o.MemberSince);
                    break;
                case "date_desc":
                    ownersIQ = ownersIQ.OrderByDescending(o => o.MemberSince);
                    break;
                default:
                    ownersIQ = ownersIQ.OrderBy(o => o.LastName);
                    break; 
            }

            var pageSize = Configuration.GetValue("PageSize", 5);

            Owners = await PaginatedList<Owner>.CreateAsync(
                ownersIQ.AsNoTracking(), pageIndex ?? 1, pageSize); 
        }
    }
}
