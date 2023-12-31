﻿using RHStaging.Models;

namespace RHStaging.Pages.Leases
{
    public class LeaseWithOwnerName
    {
        public Lease Lease { get; set; } // Assuming you have a Lease class.
        public string OwnerName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerFirstMidName { get; set; }
        public string Address { get; set; }
        public string RenterName { get; set; }

        public decimal MonthlyRent { get; set; }

        public Commission_rate Commission_Rate { get; set; }

    }
}