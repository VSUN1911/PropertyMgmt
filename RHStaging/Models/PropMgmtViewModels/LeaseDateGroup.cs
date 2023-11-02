using System;
using System.ComponentModel.DataAnnotations; 


namespace RHStaging.Models.PropMgmtViewModels
{
    public class LeaseDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime LeaseStartDate { get; set; }

        public DateTime LeaseEndDate { get; set; }
        public int PropertyCount { get; set; }
    }
}
