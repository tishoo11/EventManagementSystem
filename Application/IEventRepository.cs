using Event_Management_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Management_System.Application
{
    public interface IEventRepository
    {
        List<Event> GetEvents(); void SaveEvents(List<Event> list);
        List<Ticket> GetTickets(); void SaveTickets(List<Ticket> list);
        List<string> GetLocations(); void SaveLocations(List<string> list);
        List<Organizer> GetOrganizers(); void SaveOrganizers(List<Organizer> list);
        List<TicketType> GetTicketTypes(); void SaveTicketTypes(List<TicketType> list);
    }
}
