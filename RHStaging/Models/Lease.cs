using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Reflection.Metadata;

namespace RHStaging.Models
{
    public enum Lease_term : int
    {
        TwelveMonth = 12,
        SixMonth = 6,
        Monthly = 1
    }

    public enum Commission_rate : int
    {
        Seven = 7,
        Eight = 8
    }
    public class Lease
    {
        public int LeaseID { get; set; }

        // navigation FK to property|owner|renter
        /*
         * EF Core interprets a property as a foreign key if it's named 
         * <navigation property name><primary key property name>. 
         * For example,StudentID is the foreign key for the Student navigation property, 
         * since the Student entity's primary key is ID. 
         * Foreign key properties can also be named <primary key property name>. 
         * For example, CourseID since the Course entity's primary key is CourseID.
         */
        public int PropertyID { get; set; }
        //public int OwnerID { get; set; }
        public int RenterID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
               ApplyFormatInEditMode = true)]
        [Display(Name = "Lease Start Date")]
        public DateTime Lease_start_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
               ApplyFormatInEditMode = true)]
        [Display(Name = "Lease End Date")]
        public DateTime Lease_end_date { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Security_deposit { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Monthly_rent { get; set; }

        [DisplayFormat(NullDisplayText = "No lease term")]
        public Lease_term? Lease_term { get; set; }

        [DisplayFormat(NullDisplayText = "No Commission Rate")]
        public Commission_rate? Commission_Rate { get; set; }


        public Property Property { get; set; }
        //public Owner Owner { get; set; }
        public Renter Renter { get; set; }

    }
}