using Event_Management_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Management_System.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int AttendeeId { get; set; }
        public string Code { get; set; } // Уникалният код на билета
        public TicketCategory Category { get; set; } // Ползва Enum
        public bool IsValid { get; set; } // Дали е активен или анулиран
    }
}
