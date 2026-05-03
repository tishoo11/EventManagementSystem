using Event_Management_System.Application;
using Event_Management_System.Domain.Entities;
using System.Collections.Generic;

namespace EventManagement.Infrastructure
{
    public class FileEventRepository : IEventRepository
    {
        public List<Event> GetEvents() => FileStorage.Load<Event>("events.json");
        public void SaveEvents(List<Event> list) => FileStorage.Save("events.json", list);

        public List<Ticket> GetTickets() => FileStorage.Load<Ticket>("tickets.json");
        public void SaveTickets(List<Ticket> list) => FileStorage.Save("tickets.json", list);

        public List<string> GetLocations() => FileStorage.Load<string>("locations.json");
        public void SaveLocations(List<string> list) => FileStorage.Save("locations.json", list);

        public List<Organizer> GetOrganizers() => FileStorage.Load<Organizer>("organizers.json");
        public void SaveOrganizers(List<Organizer> list) => FileStorage.Save("organizers.json", list);

        public List<TicketType> GetTicketTypes() => FileStorage.Load<TicketType>("ticketTypes.json");
        public void SaveTicketTypes(List<TicketType> list) => FileStorage.Save("ticketTypes.json", list);
        public List<Attendee> GetAttendees() => FileStorage.Load<Attendee>("attendees.json");

        public void SaveAttendees(List<Attendee> list) => FileStorage.Save("attendees.json", list);
    }
}
