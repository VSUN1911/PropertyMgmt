namespace RHStaging.Models
{
    public class Renter
    {
        public int RenterID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime MemberSince { get; set; }

        public ICollection<Lease> Leases { get; set; }
    }
}