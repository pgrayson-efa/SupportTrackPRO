using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTrackPRO.Models
{
    public class SupportTicketList
    {
        [Key]
        [Display(Name = "Support Ticket Id")]
        public int SupportTicketId { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Support Provider Id")]
        public int SupportProviderId { get; set; }
        [Display(Name = "Registered Warrenty Id")]
        public int RegisteredWarrantyId { get; set; }
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        [Display(Name = "Reason For Support Request")]
        public string ReasonForSupport { get; set; }
    }
}
