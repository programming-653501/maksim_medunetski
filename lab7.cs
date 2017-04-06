using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    class Event
    {
        private static int IdCounter = 0;

        public int Id { get; private set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Person> Members { get; private set; }

        public Event()
        {
            Id = ++IdCounter;
            Members = new List<Person>();
        }

        public void AddMember(Person member)
        {
            Members.Add(member);
        }

        public void Show()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Duration: {Duration}");
            Console.WriteLine($"Note: {Note}");
            Console.WriteLine("Members: ");
            foreach (Person p in Members)
            {
                Console.WriteLine($"  {p.Id} - {p.FirstName} {p.LastName}");
            }
        }
    }

    class Person
    {
        private static int IdCounter = 0;

        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string Group { get; set; }

        public Person()
        {
            Id = IdCounter++;
        }

        public bool IsBirthdayToday()
        {
            return (Birthday.Day==DateTime.Now.Day)&&(Birthday.Month==DateTime.Now.Month);
        }

        public virtual void Show()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"FirstName: {FirstName}");
            Console.WriteLine($"MiddleName: {MiddleName}");
            Console.WriteLine($"LastName: {LastName}");
            Console.WriteLine($"Company: {Company}");
            Console.WriteLine($"Phone: {Phone}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Birthday: {Birthday}");
            Console.WriteLine($"Group: {Group}");
        }
    }

    class Owner:Person
    {
        public Owner()
        {
            
        }

        public override void Show()
        {
            Console.WriteLine($"FirstName: {FirstName}");
            Console.WriteLine($"MiddleName: {MiddleName}");
            Console.WriteLine($"LastName: {LastName}");
            Console.WriteLine($"Company: {Company}");
        }
    }

    class Program
    {
        static Owner Me;
        static List<Person> Contacts;
        static List<Event> Events;

        static void Help()
        {
            Console.WriteLine("(me) - show and edit information about me");
            Console.WriteLine("(contacts) - operations with your contacts");
            Console.WriteLine("(events) - operations with events");
            Console.WriteLine("(birthdays) - show who has birthday today");
        }

        static void MeCommand()
        {
            EditContact(Me);
        }

        static void PersonsCommand()
        {
            foreach (Person p in Contacts)
            {
                Console.WriteLine("-----");
                p.Show();
                Console.WriteLine("-----");
            }
            Console.WriteLine("Type Id to edit contact");
            Console.WriteLine("(add) - to add contact");
            Console.WriteLine("Something else to return in main menu");
            string command = Console.ReadLine();
            if (command == "add")
                AddContact();
            int id = 0;
            if (int.TryParse(command, out id))
            {
                foreach (Person p in Contacts)
                    if (p.Id == id)
                    {
                        EditContact(p);
                        break;
                    }
            }
        }

        static void EditContact(Person contact)
        {
            contact.Show();
            Console.WriteLine("Type capacity to edit it, or type something else to return in main menu");
            string capacity = Console.ReadLine();
            string value;
            switch (capacity)
            {
                case "FirstName":
                    Console.WriteLine("Type value:");
                    value = Console.ReadLine();
                    contact.FirstName = value;
                    break;
                case "MiddleName":
                    Console.WriteLine("Type value:");
                    value = Console.ReadLine();
                    contact.MiddleName = value;
                    break;
                case "LastName":
                    Console.WriteLine("Type value:");
                    value = Console.ReadLine();
                    contact.LastName = value;
                    break;
                case "Company":
                    Console.WriteLine("Type value:");
                    value = Console.ReadLine();
                    contact.Company = value;
                    break;
                case "Phone":
                    Console.WriteLine("Type value:");
                    value = Console.ReadLine();
                    contact.Phone = value;
                    break;
                case "Email":
                    Console.WriteLine("Type value:");
                    value = Console.ReadLine();
                    contact.Email = value;
                    break;
                case "Birthday":
                    while (true)
                    {
                        Console.WriteLine("Type year, month and day");
                        try
                        {
                            int year = int.Parse(Console.ReadLine());
                            int month = int.Parse(Console.ReadLine());
                            int day = int.Parse(Console.ReadLine());
                            DateTime birthday = new DateTime(year, month, day);
                            contact.Birthday = birthday;
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Something is wrong, please try again");
                        }
                    }
                    break;
                case "Group":
                    Console.WriteLine("Type value:");
                    value = Console.ReadLine();
                    contact.Group = value;
                    break;
            }
        }

        static void AddContact()
        {
            Console.WriteLine("Contact has been added");
            Contacts.Add(new Person());
        }

        static void EventsCommand()
        {
            foreach (Event p in Events)
            {
                Console.WriteLine("-----");
                p.Show();
                Console.WriteLine("-----");
            }
            Console.WriteLine("Type Id to edit event");
            Console.WriteLine("(add) - to add event");
            Console.WriteLine("Something else to return in main menu");
            string command = Console.ReadLine();
            if (command == "add")
                AddEvent();
            int id = 0;
            if (int.TryParse(command, out id))
            {
                foreach (Event p in Events)
                    if (p.Id == id)
                    {
                        EditEvent(p);
                        break;
                    }
            }
        }

        static void AddEvent()
        {
            Console.WriteLine("Event has been added");
            Events.Add(new Event());
        }

        static void EditEvent(Event ev)
        {

            ev.Show();
            Console.WriteLine("Type capacity to edit it, or type something else to return in main menu");
            Console.WriteLine("Type (Add) to add member to the event");
            string capacity = Console.ReadLine();
            string value;
            switch (capacity)
            {
                case "Name":
                    Console.WriteLine("Type value:");
                    value = Console.ReadLine();
                    ev.Name = value;
                    break;
                case "Note":
                    Console.WriteLine("Type value:");
                    value = Console.ReadLine();
                    ev.Note = value;
                    break;
                case "Date":
                    while (true)
                    {
                        Console.WriteLine("Type year, month and day");
                        try
                        {
                            int year = int.Parse(Console.ReadLine());
                            int month = int.Parse(Console.ReadLine());
                            int day = int.Parse(Console.ReadLine());
                            DateTime date = new DateTime(year, month, day);
                            ev.Date = date;
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Something is wrong, please try again");
                        }
                    }
                    break;
                case "Duration":
                    while (true)
                    {
                        Console.WriteLine("Type hours, minutes and seconds");
                        try
                        {
                            int hours = int.Parse(Console.ReadLine());
                            int minutes = int.Parse(Console.ReadLine());
                            int seconds = int.Parse(Console.ReadLine());
                            TimeSpan time = new TimeSpan(hours, minutes, seconds);
                            ev.Duration = time;
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Something is wrong, please try again");
                        }
                    }
                    break;
                case "Add":
                    Console.WriteLine("Type his Id");
                    int id;
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        foreach (Person p in Contacts)
                            if (p.Id == id)
                            {
                                ev.AddMember(p);
                                break;
                            }
                    }
                    break;
            }
        }

        static void BirthdaysCommand()
        {
            foreach (Person p in Contacts)
            {
                if (p.IsBirthdayToday())
                {
                    Console.WriteLine("-----");
                    p.Show();
                    Console.WriteLine("-----");
                }
            }
        }

        static void Main(string[] args)
        {
            Me = new Owner();
            Contacts = new List<Person>();
            Events = new List<Event>();
            while (true)
            {
                Console.WriteLine("Type your command (help):");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "help":
                        Help();
                        break;
                    case "me":
                        MeCommand();
                        break;
                    case "contacts":
                        PersonsCommand();
                        break;
                    case "birthdays":
                        BirthdaysCommand();
                        break;
                    case "events":
                        EventsCommand();
                        break;
                    default:
                        Console.WriteLine("Incorrect command! Please try again");
                        break;
                }
            }
        }
    }
}
