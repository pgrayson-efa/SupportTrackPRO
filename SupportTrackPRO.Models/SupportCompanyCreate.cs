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
        public string CompanyName { get; set; }
        [Required]
        public string MainPhoneNumber { get; set; }
        [Required]
        public string StreetAddress1 { get; set; }
        [Required]
        public string StreetAddress2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
    }
}
