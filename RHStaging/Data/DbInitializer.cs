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

            #region Create mock up owners 
            var 趙榮 = new Owner
            {
                LastName = "張",
                FirstMidName = "趙榮",
                MemberSince = DateTime.Parse("2023-10-31"),
                Email = "xyz@hmail.com",
                Phone = "123-456-7890"
            };

            var 信羊 = new Owner
            {
                LastName = "黃",
                FirstMidName = "信羊",                
                MemberSince = DateTime.Parse("2023-1-28"),
                Email = "abc@home.com",
                Phone = "980-765-4321"
            };

            var MingChung = new Owner
            {
                FirstMidName = "MingChung",
                LastName = "Chen",
                MemberSince = DateTime.Parse("2022-10-31")
            };

            var Mozart = new Owner
            {
                FirstMidName = "Mozart",
                LastName = "Wolfgang",
                MemberSince = DateTime.Parse("1995-10-31")
            };

            var owners = new Owner[]
            {
                趙榮,
                信羊,
                MingChung,
                Mozart,
            };

            context.AddRange(owners);
            #endregion

            #region Create Mock up Properties
            var prop1 = new Property
            {
                //OwnerID = 1,
                Owner = 趙榮,
                Address = "203 123th St",
                City = "Bellevue",
                NumOfBathRooms = 3,
                NumOfBedRooms = 2,
                PropertyType = PropertyType.SingleFamily,
                Sqft = 1234,
                State = State.Washington,
                ZipCode = "98006",                 
            };

            var prop2 = new Property
            {
                //OwnerID = 1,
                Owner = MingChung,
                Address = "530 Saseme Street",
                City = "Bellevue",
                NumOfBathRooms = 5,
                NumOfBedRooms = 4,
                PropertyType = PropertyType.SingleFamily,
                Sqft = 5634,
                State = State.Washington,
                ZipCode = "98006",
            };

            var prop3 = new Property
            {
                Owner = 趙榮,
                Address = "36839 156th St",
                City = "Redmond",
                NumOfBathRooms = 2,
                NumOfBedRooms = 2,
                PropertyType = PropertyType.Condo,
                Sqft = 984,
                State = State.Washington,
                ZipCode = "98056"
            };

            var prop4 = new Property
            {
                Owner = Mozart,
                Address = "255 Bluepoint Street",
                City = "Kirkland",
                NumOfBathRooms = 5,
                NumOfBedRooms = 4,
                PropertyType = PropertyType.SingleFamily,
                Sqft = 4634,
                State = State.Washington,
                ZipCode = "98026",
            };

            var propertyes = new Property[]
                { prop1, prop2, prop3 };

            context.AddRange(propertyes);
            #endregion

            #region Mock up Renters 
            var Dufrane = new Renter
            {
                FirstMidName = "Andy",
                LastName = "Dufrane",
                MemberSince = DateTime.Parse("2019-10-3")
            };

            var Huang = new Renter
            {
                FirstMidName = "Joyce",
                LastName = "Huang",
                MemberSince = DateTime.Parse("2009-1-3")
            };

            var Neruda = new Renter
            {
                FirstMidName = "Xavier",
                LastName = "Neruda",
                MemberSince = DateTime.Parse("2014-7-3")
            };

            var renters = new Renter[]
                {Dufrane, Huang, Neruda };

            context.AddRange(renters);
            #endregion

            #region Mock up Leases
            var lease1 = new Lease
            {
                Owner = 趙榮,
                Renter = Dufrane,
                Property = prop3,   // TODO: make sure property belongs to owner 
                Commission_Rate = Commission_rate.SevenPercent,
                Lease_start_date = DateTime.Parse("2023-03-04"),
                Lease_end_date = DateTime.Parse("2024-03-04"),
                Monthly_rent = 3000,
                Security_deposit = 3000,
            };

            var lease2 = new Lease
            {
                //RenterID = 3,
                //PropertyID = 2,
                Owner = Mozart,
                Property = prop4,
                Renter = Neruda,
                Commission_Rate = Commission_rate.EightPercent,
                Lease_start_date = DateTime.Parse("2023-12-14"),
                Lease_end_date = DateTime.Parse("2024-12-13"),
                Monthly_rent = 2700,
                Security_deposit = 2700
            };

            var leases = new Lease[] { lease1, lease2 };

            context.AddRange(leases);
            #endregion

            context.SaveChanges();
        }
    }
}
