using RHStaging.Models; 

namespace RHStaging.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PropMgmtContext context) 
        {
            // look for any owners
            if (context.Owners.Any())
            {
                return;   // DB has been seeded
            }

            var owners = new Owner[]
            {
                // TODO: fix display on page for member since (2023-00-31) while input is (2023-10-31)
                new Owner{FirstMidName="趙榮", LastName="張", MemberSince=DateTime.Parse("2023-10-31"), Email="xyz@hmail.com", Phone="123-456-7890"},
                new Owner{FirstMidName="信羊", LastName="黃", MemberSince=DateTime.Parse("2023-1-28"), Email="abc@home.com", Phone="980-765-4321"},
                new Owner{FirstMidName="MingChung", LastName="Chen", MemberSince=DateTime.Parse("2022-10-31")},
                new Owner{FirstMidName="Mozart", LastName="Wolfgang", MemberSince=DateTime.Parse("1995-10-31")},
            };

            context.Owners.AddRange(owners);
            context.SaveChanges();

            var properties = new Property[]
            {
                new Property{ OwnerID = 1, Address="203 123th St", City="Bellevue", NumOfBathRooms=3, NumOfBedRooms=2, PropertyType=PropertyType.SingleFamily, Sqft=1234, State=State.Washington, ZipCode="98006", },
                new Property{ OwnerID = 1, Address="530 Saseme Street", City="Bellevue", NumOfBathRooms=5, NumOfBedRooms=4, PropertyType=PropertyType.SingleFamily, Sqft=5634, State=State.Washington, ZipCode="98006", },
                new Property{ OwnerID = 3, Address="36839 156th St", City="Redmond", NumOfBathRooms=2, NumOfBedRooms=2, PropertyType=PropertyType.Condo, Sqft=984, State=State.Washington, ZipCode="98056" }
            };

            context.Properties.AddRange(properties);
            context.SaveChanges();

            var renters = new Renter[]
            {
                new Renter{FirstMidName="Andy", LastName="Dufrane", MemberSince=DateTime.Parse("2019-10-3")},
                new Renter{FirstMidName="Joyce", LastName="Huang", MemberSince=DateTime.Parse("2009-1-3")},
                new Renter{FirstMidName="Xavier", LastName="Neruda", MemberSince=DateTime.Parse("2014-7-3")},
            };

            context.Renters.AddRange(renters);
            context.SaveChanges();

            var leases = new Lease[]
            {
                new Lease{  RenterID=2, PropertyID=3, Commission_Rate=Commission_rate.Seven
                           , Lease_start_date=DateTime.Parse("2023-03-04"), Lease_end_date=DateTime.Parse("2024-03-04"), Monthly_rent=3000, Security_deposit=3000  },
                new Lease{  RenterID=3, PropertyID=2, Commission_Rate=Commission_rate.Seven
                           , Lease_start_date=DateTime.Parse("2023-12-14"), Lease_end_date=DateTime.Parse("2024-12-13"), Monthly_rent=2700, Security_deposit=2700  },
            };

            context.Leases.AddRange(leases);
            context.SaveChanges();

        }
    }
}
