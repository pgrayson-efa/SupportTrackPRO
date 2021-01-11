using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTrackPRO.Models
{
    public class SupportProductList
    {
        [Display(Name = "Support Product Id")]
        public int SupportProductId { get; set; }

        [Display(Name = "Support Company Id")]
        public int SupportCompanyId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; }

        [Display(Name = "Version")]
        public string Version { get; set; }

        [Display(Name = "Warranty Length In Days")]
        public int WarrantyLengthInDays { get; set; }
    }
}
