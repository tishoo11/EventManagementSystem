using System;
using Event_Management_System.Domain.Entities;
using EventManagement.Application;

namespace EventManagement.ConsoleUI
    {
        public class EventUI
        {
            private readonly EventService _s;
            public EventUI(EventService s) { _s = s; }

            public void Run()
            {
                while (true)
                {
                    Console.WriteLine("\n=== EVENT MANAGEMENT SYSTEM ===");
                    Console.WriteLine("1. Създай събитие       11. Добави тип билет");
                    Console.WriteLine("2. Редактирай събитие   12. Добави локация");
                    Console.WriteLine("3. Изтрий събитие       13. Редактирай локация");
                    Console.WriteLine("4. Търси по име         14. Заетост на локация");
                    Console.WriteLine("5. Филтър по дата       15. Добави организатор");
                    Console.WriteLine("6. Филтър по тип        16. Справка: Участници");
                    Console.WriteLine("7. Провери капацитет    17. Справка: Предстоящи");
                    Console.WriteLine("8. Купи билет           18. Справка: Най-посещавани");
                    Console.WriteLine("9. Отмени билет");
                    Console.WriteLine("10. Провери валидност");
                    Console.WriteLine("0. ИЗХОД");
                    Console.Write("Избор: ");

                    string c = Console.ReadLine();
                    if (c == "0") break;

                if (c == "1")
                {
                    Console.Write("Име: "); string n = Console.ReadLine();

                    Console.Write("Дата (гггг-мм-дд): ");
                    string inputDate = Console.ReadLine();
                    string formattedDate;

                    try
                    {
                        formattedDate = DateTime.Parse(inputDate).ToString("yyyy-MM-dd");
                    }
                    catch
                    {
                        Console.WriteLine("Невалидна дата! Използваме днешната по подразбиране.");
                        formattedDate = DateTime.Now.ToString("yyyy-MM-dd");
                    }

                    Console.Write("Име локация: "); string l = Console.ReadLine();
                    Console.Write("Капацитет: "); int cap = int.Parse(Console.ReadLine());
                    Console.Write("Тип: "); string t = Console.ReadLine();

                    _s.CreateEvent(n, formattedDate, l, cap, t);
                }
                else if (c == "2")
                    {
                        Console.Write("ID Събитие: "); int id = int.Parse(Console.ReadLine());
                        Console.Write("Ново име: "); string n = Console.ReadLine();
                        Console.Write("Нов капацитет: "); int cap = int.Parse(Console.ReadLine());
                        _s.EditEvent(id, n, cap);
                    }
                    else if (c == "3")
                    {
                        Console.Write("ID Събитие: "); int id = int.Parse(Console.ReadLine());
                        _s.DeleteEvent(id);
                    }
                    else if (c == "4")
                    {
                        Console.Write("Търси: "); string search = Console.ReadLine();
                      foreach (var e in _s.SearchEvents(search))
                      {
                        Console.WriteLine($"{e.Id}: {e.Name}");
                      }
                    }
                    else if (c == "5")
                    {
                       Console.Write("Дата (гггг-мм-дд): ");
                       string d = DateTime.Parse(Console.ReadLine()).ToString("yyyy-MM-dd");

                       foreach (var e in _s.FilterByDate(d))
                        Console.WriteLine($"{e.Name}");
                    }
                    else if (c == "6")
                    {
                        Console.Write("Тип: "); string t = Console.ReadLine();
                        foreach (var e in _s.FilterByType(t)) Console.WriteLine($"{e.Name}");
                    }
                    else if (c == "7")
                    {
                        Console.Write("ID Събитие: "); int id = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Свободни места: {_s.GetFreeSpots(id)}");
                    }
                    else if (c == "8")
                    {
                        Console.Write("ID Събитие: "); int id = int.Parse(Console.ReadLine());
                        Console.Write("Име на участник: "); int attendeeId = int.Parse(Console.ReadLine());
                        _s.RegisterAttendee(id, attendeeId);
                    }
                    else if (c == "9")
                    {
                        Console.Write("Код: "); string code = Console.ReadLine();
                        _s.CancelRegistration(code);
                    }
                    else if (c == "10")
                    {
                        Console.Write("Код: "); string code = Console.ReadLine();
                        _s.CheckTicket(code);
                    }
                    else if (c == "11")
                    {
                        Console.Write("Тип билет: "); string n = Console.ReadLine();
                        Console.Write("Цена (BGN): "); decimal p = decimal.Parse(Console.ReadLine());
                        _s.AddTicketType(n, p);
                    }
                    else if (c == "12")
                    {
                        Console.Write("Име на локация: "); string n = Console.ReadLine();
                        _s.AddLocation(n);
                    }
                    else if (c == "13")
                    {
                        Console.Write("Старо име: "); string o = Console.ReadLine();
                        Console.Write("Ново име: "); string n = Console.ReadLine();
                        _s.EditLocation(o, n);
                    }
                    else if (c == "14")
                    {
                        Console.Write("Локация: "); string n = Console.ReadLine();
                        _s.PrintLocationOccupancy(n);
                    }
                    else if (c == "15")
                    {
                        Console.Write("Организатор: "); string n = Console.ReadLine();
                        _s.AddOrganizer(n);
                    }
                    else if (c == "16")
                    {
                        Console.Write("ID Събитие: "); int id = int.Parse(Console.ReadLine());
                        _s.PrintAttendees(id);
                    }
                    else if (c == "17")
                    {
                        _s.PrintUpcoming();
                    }
                    else if (c == "18")
                    {
                        _s.PrintPopular();
                    }
                    else
                    {
                        Console.WriteLine("Невалиден избор!");
                    }
                }
            }
        }
}
