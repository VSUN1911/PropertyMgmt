﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHStaging.Models
{
    public enum PropertyType
    {
        SingleFamily = 0,
        Condo = 1,
        Duplex = 2,
    }

    public enum State
    {
        Alabama,
        Alaska,
        Arizona,
        Arkansas,
        California,
        Colorado,
        Connecticut,
        Delaware,
        Florida,
        Georgia,
        Hawaii,
        Idaho,
        Illinois,
        Indiana,
        Iowa,
        Kansas,
        Kentucky,
        Louisiana,
        Maine,
        Maryland,
        Massachusetts,
        Michigan,
        Minnesota,
        Mississippi,
        Missouri,
        Montana,
        Nebraska,
        Nevada,
        NewHampshire,
        NewJersey,
        NewMexico,
        NewYork,
        NorthCarolina,
        NorthDakota,
        Ohio,
        Oklahoma,
        Oregon,
        Pennsylvania,
        RhodeIsland,
        SouthCarolina,
        SouthDakota,
        Tennessee,
        Texas,
        Utah,
        Vermont,
        Virginia,
        Washington,
        WestVirginia,
        Wisconsin,
        Wyoming
    }
    public class Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Prop_ID")]
        public int PropertyID { get; set; }
        public PropertyType? PropertyType { get; set; }

        [ForeignKey("Owner")]
        public int OwnerID { get; set; }

        [ForeignKey("Lease")]
        public int? LeaseID { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public State? State { get; set; }
        public string ZipCode { get; set; }

        [Display(Name = "#BedRooms")]
        public int NumOfBedRooms { get; set; }

        [Display(Name = "#BathRooms")]
        public int NumOfBathRooms { get; set; }
        public int Sqft { get; set; }

        //public ICollection<Lease> Leases { get; set; }
        public Lease Lease { get; set; }
        public Owner Owner { get; set; }
       
        public int? YearBuilt { get; set; }
    }
}