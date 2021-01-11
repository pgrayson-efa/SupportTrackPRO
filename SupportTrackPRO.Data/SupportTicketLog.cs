using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTrackPRO.Data
{
    public class SupportTicketLog
    {
        public int SupportTicketLogId { get; set; }
        public int SupportTicketId { get; set; }
        public DateTimeOffset LogEntryDate { get; set; }
        public string LogEntryType { get; set; }
        public string Note { get; set; }
    }
}
