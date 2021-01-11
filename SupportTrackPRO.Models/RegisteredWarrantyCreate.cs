using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTrackPRO.Models
{
    public class RegisteredWarrantyCreate
    {
        [Display(Name = "Registered Warrenty Id")]
        public int RegisteredWarrentyId { get; set; }

        [Display(Name = "Support Company")]
        public int SupportCompanyId { get; set; }

        [Display(Name = "Product")]
        public int SupportProductId { get; set; }

        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Display(Name = "Date Of Warrenty Registration")]
        public DateTimeOffset DateRegistered { get; set; }
    }
}
