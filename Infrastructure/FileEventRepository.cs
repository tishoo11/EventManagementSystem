using Event_Management_System.Application;
using Event_Management_System.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EventManagement.Infrastructure
{
    public class FileEventRepository : IEventRepository
    {
        private readonly string _eventsPath = "events.json";
        private readonly string _ticketsPath = "tickets.json";
        private readonly string _attendeesPath = "attendees.json";

        private readonly JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = true };

        public List<Event> GetEvents() => Load<Event>(_eventsPath);
        public void SaveEvents(List<Event> list) => Save(_eventsPath, list);

        public List<Ticket> GetTickets() => Load<Ticket>(_ticketsPath);
        public void SaveTickets(List<Ticket> list) => Save(_ticketsPath, list);

        public List<Attendee> GetAttendees() => Load<Attendee>(_attendeesPath);
        public void SaveAttendees(List<Attendee> list) => Save(_attendeesPath, list);

        private List<T> Load<T>(string path)
        {
            if (!File.Exists(path)) return new List<T>();
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        private void Save<T>(string path, List<T> list)
        {
            string json = JsonSerializer.Serialize(list, _options);
            File.WriteAllText(path, json);
        }

        public List<string> GetLocations() => new List<string>();
        public void SaveLocations(List<string> list) { }
        public List<Organizer> GetOrganizers() => new List<Organizer>();
        public void SaveOrganizers(List<Organizer> list) { }
        public List<TicketType> GetTicketTypes() => new List<TicketType>();
        public void SaveTicketTypes(List<TicketType> list) { }
    }
}