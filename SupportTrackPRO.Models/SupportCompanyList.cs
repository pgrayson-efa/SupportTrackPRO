using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTrackPRO.Models
{
    public class SupportCompanyList
    {
        [Display(Name = "Support Company Id")]
        public int SupportCompanyId { get; set; }

        [Display(Name = "Support Company Name")]
        public string CompanyName { get; set; }
    }
}
