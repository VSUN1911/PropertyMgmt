using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RHStaging.Models
{
    public class Renter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RenterID { get; set; }

        [ForeignKey("Lease")]
        public int? LeaseID { get; set; }

        public string LastName { get; set; }

        [Display(Name = "FirstName")]
        public string FirstMidName { get; set; }
        public DateTime MemberSince { get; set; }

        public Lease Lease { get; set; }
    }
}