using Event_Management_System.Application;
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
        public void CreateEvent(string n, string d, string l, int c, string t)
        {
            var events = _repo.GetEvents();

            if (events.Any(x => x.LocationName == l && x.Date == d))
            {
                Console.WriteLine("Грешка: Локацията е заета за тази дата!");
                return;
            }

            events.Add(new Event
            {
                Id = events.Count > 0 ? events.Max(x => x.Id) + 1 : 1,
                Name = n,
                Date = d, 
                LocationName = l,
                Capacity = c,
                EventType = t
            });

            _repo.SaveEvents(events);
            Console.WriteLine("Събитието е създадено успешно!");
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
            Console.WriteLine("Събитието е изтрито!");
        }

        // 4. Търсене
        public List<Event> SearchEvents(string name) => _repo.GetEvents().Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();

        // 5. Филтриране по дата
        public List<Event> FilterByDate(string searchDate)
        {
            return _repo.GetEvents()
                        .Where(e => e.Date == searchDate)
                        .ToList();
        }

        // 6. Филтриране по тип
        public List<Event> FilterByType(string t) => _repo.GetEvents().Where(x => x.EventType.ToLower() == t.ToLower()).ToList();

        // 8. Проверка за капацитет
        public int GetFreeSpots(int eventId)
        {
            var ev = _repo.GetEvents().FirstOrDefault(x => x.Id == eventId);
            if (ev == null) return 0;
            int taken = _repo.GetTickets().Count(t => t.EventId == eventId && t.IsValid);
            return ev.Capacity - taken;
        }

        // 9 & 11. Регистрация и Генериране на билет
        public void RegisterAttendee(int eventId, int attendeeId)
        {
            // Проверка за свободни места (Услуга 8)
            if (GetFreeSpots(eventId) <= 0)
            {
                Console.WriteLine("Няма свободни места!");
                return;
            }

            var tickets = _repo.GetTickets();

            // Генериране на уникален код (Услуга 11)
            string code = "TICK" + new Random().Next(1000, 9999);

            // Създаване на билета с подаденото AttendeeId
            tickets.Add(new Ticket
            {
                Id = tickets.Count + 1,
                EventId = eventId,
                AttendeeId = attendeeId,
                Code = code,
                Category = TicketCategory.Standard,
                IsValid = true
            });

            _repo.SaveTickets(tickets);

            Console.WriteLine($"Успешна регистрация! Код: {code}");
        }

        // 10. Отмяна
        public void CancelRegistration(string code)
        {
            var tickets = _repo.GetTickets();
            var t = tickets.FirstOrDefault(x => x.Code == code);
            if (t != null) { t.IsValid = false; _repo.SaveTickets(tickets); Console.WriteLine("Отменена!"); }
        }

        // 12. Валидност
        public void CheckTicket(string code)
        {
            bool valid = _repo.GetTickets().Any(x => x.Code == code && x.IsValid);
            Console.WriteLine(valid ? "Билетът е ВАЛИДЕН!" : "НЕВАЛИДЕН билет!");
        }

        // 13. Управление на типове билети
        public void AddTicketType(string name, decimal amount)
        {
            var types = _repo.GetTicketTypes();
            types.Add(new TicketType { Id = types.Count + 1, Name = name, Price = new Money { Amount = amount } });
            _repo.SaveTicketTypes(types);
        }

        // 14. Добавяне локация
        public void AddLocation(string name)
        {
            var locs = _repo.GetLocations(); locs.Add(name); _repo.SaveLocations(locs);
        }

        // 15. Редактиране локация
        public void EditLocation(string oldName, string newName)
        {
            var locs = _repo.GetLocations();
            int idx = locs.IndexOf(oldName);
            if (idx != -1) { locs[idx] = newName; _repo.SaveLocations(locs); }
        }

        // 16. Заетост локация
        public void ShowLocationOccupancy(string locationName)
        {
            var events = _repo.GetEvents()
                .Where(e => e.LocationName.Equals(locationName, StringComparison.OrdinalIgnoreCase))
                .OrderBy(e => e.Date) 
                .ToList();

            if (events.Count == 0)
            {
                Console.WriteLine($"Няма планирани събития за локация: {locationName}");
                return;
            }

            Console.WriteLine($"--- График за {locationName} ---");
            foreach (var e in events)
            {
                Console.WriteLine($"{e.Date} | {e.Name} ({e.EventType})");
            }
        }

        // 17. Добавяне организатор
        public void AddOrganizer(string name)
        {
            var orgs = _repo.GetOrganizers(); orgs.Add(new Organizer { Id = orgs.Count + 1, Name = name }); _repo.SaveOrganizers(orgs);
        }

        // 18. Участници по събитие
        public void PrintAttendees(int eventId)
        {
            var tickets = _repo.GetTickets().Where(x => x.EventId == eventId && x.IsValid);
            var attendees = _repo.GetAttendees();

            foreach (var t in tickets)
            {
                var a = attendees.FirstOrDefault(x => x.Id == t.AttendeeId);
                string name = a != null ? a.Name : "Неизвестен";
                Console.WriteLine($"- {name} (Код: {t.Code})");
            }
        }

        // 19. Предстоящи събития
        public void PrintUpcomingEvents()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");

            var upcoming = _repo.GetEvents()
                                .Where(e => string.Compare(e.Date, today) >= 0)
                                .OrderBy(e => e.Date) 
                                .ToList();

            foreach (var e in upcoming)
            {
                Console.WriteLine($"{e.Date} - {e.Name}");
            }
        }

        // 20. Най-посещавани
        public void PrintPopular()
        {
            var events = _repo.GetEvents();
            foreach (var e in events)
            {
                int count = _repo.GetTickets().Count(t => t.EventId == e.Id && t.IsValid);
                Console.WriteLine($"{e.Name} -> {count} продадени билета");
            }
        }

        public void PrintLocationOccupancy(string n)
        {
            var events = _repo.GetEvents()
                .Where(x => x.LocationName.Equals(n, StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x.Date) 
                .ToList();

            if (!events.Any()) { Console.WriteLine("Няма събития."); return; }

            foreach (var e in events)
                Console.WriteLine($"{e.Date} - {e.Name}");
        }

        public void PrintUpcoming()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");

            var upcoming = _repo.GetEvents()
                .Where(x => string.Compare(x.Date, today) >= 0) // Сравняваме по азбучен ред
                .OrderBy(x => x.Date)
                .ToList();

            foreach (var e in upcoming)
                Console.WriteLine($"{e.Date} - {e.Name}");
        }
    }


}