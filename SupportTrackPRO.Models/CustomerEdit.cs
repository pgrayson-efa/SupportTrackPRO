using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTrackPRO.Models
{
    public class CustomerEdit
    {
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        [Required]
        [Display(Name = "Application User Id")]
        public Guid ApplicationUserId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Street Address")]
        public string Address1 { get; set; }
        [Display(Name = "Street Address")]
        public string Address2 { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Display(Name = "Home Phone Number")]
        public string PhoneNumber1 { get; set; }
        [Display(Name = "Cell Phone Number")]
        public string PhoneNumber2 { get; set; }
    }
}
