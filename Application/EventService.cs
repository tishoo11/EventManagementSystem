using Event_Management_System.Application;
using Event_Management_System.Domain.Entities;
using Event_Management_System.Domain.Entities;
using Event_Management_System.Domain.Enums;
using Event_Management_System.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application
{
    public class EventService
    {
        private readonly IEventRepository _repo;
        public EventService(IEventRepository repo) { _repo = repo; }

        // 1 & 7. Създаване и Проверка за свободна локация
        public void CreateEvent(string n, DateTime d, string l, int c, string t)
        {
            var events = _repo.GetEvents();
            if (events.Any(x => x.LocationName == l && x.Date.Date == d.Date))
            {
                Console.WriteLine("Грешка: Локацията е заета за тази дата!"); return;
            }
            events.Add(new Event { Id = events.Count + 1, Name = n, Date = d, LocationName = l, Capacity = c, EventType = t });
            _repo.SaveEvents(events);
            Console.WriteLine("Събитието е създадено!");
        }

        // 2. Редактиране
        public void EditEvent(int id, string newName, int newCap)
        {
            var events = _repo.GetEvents();
            var ev = events.FirstOrDefault(x => x.Id == id);
            if (ev != null) { ev.Name = newName; ev.Capacity = newCap; _repo.SaveEvents(events); }
        }

        // 3. Изтриване
        public void DeleteEvent(int id)
        {
            var events = _repo.GetEvents();
            events.RemoveAll(x => x.Id == id);
            _repo.SaveEvents(events);
        }

        // 4. Търсене
        public List<Event> SearchEvents(string name) => _repo.GetEvents().Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();

        // 5. Филтриране по дата
        public List<Event> FilterByDate(DateTime d) => _repo.GetEvents().Where(x => x.Date.Date == d.Date).ToList();

        // 6. Филтриране по тип
        public List<Event> FilterByType(string t) => _repo.GetEvents().Where(x => x.EventType.ToLower() == t.ToLower()).ToList();
    }
}