using Event_Management_System.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Management_System.Domain.Entities
{
    public class TicketType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Money Price { get; set; }
    }

}
