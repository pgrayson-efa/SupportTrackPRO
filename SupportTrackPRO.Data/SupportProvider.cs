using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTrackPRO.Data
{
    public class SupportProvider
    {
        [Key]
        [ReadOnly(true)]
        [Display(Name = "Support Provider Id")]
        public int SupportProviderId { get; set; }
        [Required]
        [Display(Name = "Support Company Id")]
        public int SupportCompanyId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Handle")]
        public string Handle { get; set; }

    }
}
