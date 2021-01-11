using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTrackPRO.Data
{
    public class RegisteredWarranty
    {
        [Key]
        [Display(Name = "Registered Warranty Id")]
        public int RegisteredWarrantyId { get; set; }
        [Display(Name = "Support Company Id")]
        public int SupportCompanyId { get; set; }
        [Display(Name = "Support Product Id")]
        public int SupportProductId { get; set; }
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }
        [Display(Name = "Date Of Warrenty Registration")]
        public DateTimeOffset DateRegistered { get; set; }
    }
}
