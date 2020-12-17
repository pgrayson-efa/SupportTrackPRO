using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTrackPRO.Data
{
    public class SupportProduct
    {
        [Key]
        public int SupportProductId { get; set; }
        [Required]
        public int SupportCompanyId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string ModelNumber { get; set; }
        public string Version { get; set; }
        [Required]
        public int WarrentyLengthInDays { get; set; }
    }
}
