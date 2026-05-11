using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Management_System.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string LocationName { get; set; } 
        public int Capacity { get; set; }
        public string EventType { get; set; }
    }
}
