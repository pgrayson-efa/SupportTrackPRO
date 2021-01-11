using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTrackPRO.Models
{
    public class SupportCompanyCreate
    {
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Main Phone Number")]
        public string MainPhoneNumber { get; set; }
        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress1 { get; set; }
        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress2 { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [Display(Name = "State")]
        public string State { get; set; }
        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
    }
}
