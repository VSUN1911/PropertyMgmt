using System;
using System.ComponentModel.DataAnnotations;

namespace RHStaging.Models.PropMgmtViewModels
{
    public class OwnerPropertyInfo
    {
        public string OwnerName { get; set; }
        //public string PropertyAddress { get; set; }                

        public int PropertyCount { get; set; }
    }
}
