using Event_Management_System.Application;
using EventManagement.Application;
using EventManagement.ConsoleUI;
using EventManagement.Infrastructure;

namespace EventManagement
{
    class Program
    {
        static void Main()
        {
            // "Свързваме" трите слоя на архитектурата
            IEventRepository repo = new FileEventRepository();
            EventService service = new EventService(repo);
            EventUI ui = new EventUI(service);

            // Стартираме конзолното меню
            ui.Run();
        }
    }
}
