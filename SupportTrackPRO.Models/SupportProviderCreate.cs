using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTrackPRO.Models
{
    public class SupportProviderCreate
    {
        //[Required]
        [Display(Name = "Support Provider Id")]
        public int SupportProviderId { get; set; }
        
        //[Required]
        [Display(Name = "Support Company Id")]
        public int SupportCompanyId { get; set; }

        [Required]
        [Display(Name = "Registered User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Support Track Handle")]
        public string Handle { get; set; }
    }
}
