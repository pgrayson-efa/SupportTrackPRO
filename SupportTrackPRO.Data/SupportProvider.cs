using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTrackPRO.Data
{
    public class SupportProvider
    {
        [Key]
        public int SupportProviderId { get; set; }
        [Required]
        public int SupportCompanyId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Handle { get; set; }
    }
}
