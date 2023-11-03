using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace RHStaging.Models
{
    public class Owner
    {
        [Display(Name = "Owner_ID")]
        public int OwnerID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        //[RegularExpression(@"^[\p{L}-]+$")]  // TODO: RegEx for unicode 
        [RegularExpression(@"^[a-zA-Z]*$")]
        public string LastName { get; set; }

        [Column("FirstName")]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Member Since")]
        public DateTime MemberSince { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        [RegularExpression(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(15)]
        [Display(Name = "Phone")]
        [RegularExpression(@"^\s*\(?\d{3}\)?[-\s]?\d{7}\s*$")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public ICollection<Lease> Leases { get; set; }
        public ICollection<Property> Properties { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }
        
        /*
         * 
         * ^ and $: Anchors the pattern to the start and end of the string, ensuring the entire string is matched.
            \s*: Matches zero or more whitespace characters (spaces, tabs, or line breaks) at the beginning and end of the string.
            \(?\d{3}\)?: Matches an optional area code enclosed in parentheses. It consists of three digits. The ? makes the parentheses and area code as a whole optional.
            [-\s]?: Matches an optional separator, which can be either a hyphen or a space. The ? makes the separator optional.
            \d{7}: Matches seven digits for the main phone number.
            \s*: Matches zero or more whitespace characters at the end of the string.
        */

    }
}
