
using System;
using System.Globalization;

namespace SessionsManager
{
    public static class DateTools
    {
        public static void ShowDateDetails()
        {
            Console.Write("Enter session id: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Not a number.");
                return;
            }

            Session s = SessionStore.FindById(id);
            if (s == null)
            {
                Console.WriteLine("No session with id " + id);
                return;
            }

            DateTime d = s.Date;

            Console.WriteLine("Year: " + d.Year);
            Console.WriteLine("Month: " + d.Month + " (" + d.ToString("MMMM") + ")");
            Console.WriteLine("Day: " + d.Day);
            Console.WriteLine("Time: " + d.ToString("HH:mm"));
            Console.WriteLine("Day of week: " + d.DayOfWeek);
            Console.WriteLine("Day of year: " + d.DayOfYear);
            Console.WriteLine("Is leap year: " + DateTime.IsLeapYear(d.Year));
            Console.WriteLine("Days in month: " + DateTime.DaysInMonth(d.Year, d.Month));
            Console.WriteLine("Duration: " + s.DurationMinutes + " min");
            Console.WriteLine("Ends: " + s.EndsAt.ToString("HH:mm"));
        }

        public static void DateDifference()
        {
            Console.Write("First session id: ");
            int id1;
            if (!int.TryParse(Console.ReadLine(), out id1))
            {
                Console.WriteLine("Not a number.");
                return;
            }

            Console.Write("Second session id: ");
            int id2;
            if (!int.TryParse(Console.ReadLine(), out id2))
            {
                Console.WriteLine("Not a number.");
                return;
            }

            Session a = SessionStore.FindById(id1);
            Session b = SessionStore.FindById(id2);

            if (a == null || b == null)
            {
                Console.WriteLine("One of those ids does not exist.");
                return;
            }

            TimeSpan gap = b.Date - a.Date;

            Console.WriteLine(a.Title + " -> " + b.Title);
            Console.WriteLine("Total days: " + gap.TotalDays.ToString("0.00"));
            Console.WriteLine("Whole days: " + gap.Days);
            Console.WriteLine("Total hours: " + gap.TotalHours.ToString("0.0"));
        }

        public static void PastAndUpcoming()
        {
            DateTime now = DateTime.Now;

            Console.WriteLine("PAST");
            for (int i = 0; i < SessionStore.Sessions.Length; i++)
            {
                Session s = SessionStore.Sessions[i];
                if (s.EndsAt < now)
                    Console.WriteLine(s.Title + " - " + s.Date.ToString("dd/MM/yyyy"));
            }

            Console.WriteLine();
            Console.WriteLine("UPCOMING");
            for (int i = 0; i < SessionStore.Sessions.Length; i++)
            {
                Session s = SessionStore.Sessions[i];
                if (s.EndsAt >= now)
                    Console.WriteLine(s.Title + " - " + s.Date.ToString("dd/MM/yyyy"));
            }
        }

        public static Session GetNextSession()
        {
            Session next = null;
            DateTime now = DateTime.Now;

            for (int i = 0; i < SessionStore.Sessions.Length; i++)
            {
                Session s = SessionStore.Sessions[i];
                if (s.Date >= now && (next == null || s.Date < next.Date))
                    next = s;
            }
            return next;
        }

        public static void ShowNextSession()
        {
            Session next = GetNextSession();

            if (next == null)
            {
                Console.WriteLine("No upcoming session.");
                return;
            }

            TimeSpan countdown = next.Date - DateTime.Now;

            Console.WriteLine("Title: " + next.Title);
            Console.WriteLine("Instructor: " + next.Instructor);
            Console.WriteLine("When: " + next.Date.ToString("dddd dd MMMM yyyy HH:mm"));
            Console.WriteLine("Countdown: " + countdown.Days + " days, " + countdown.Hours + " hours");
        }

        public static void FormattingDemo()
        {
            DateTime d = SessionStore.Sessions[0].Date;

            Console.WriteLine("d: " + d.ToString("d"));
            Console.WriteLine("D: " + d.ToString("D"));
            Console.WriteLine("t: " + d.ToString("t"));
            Console.WriteLine("dd/MM/yyyy: " + d.ToString("dd/MM/yyyy"));
            Console.WriteLine("yyyy-MM-dd: " + d.ToString("yyyy-MM-dd"));
            Console.WriteLine("dddd, dd MMMM yyyy: " + d.ToString("dddd, dd MMMM yyyy"));
            Console.WriteLine("hh:mm tt: " + d.ToString("hh:mm tt"));
        }

        public static void ReadAndValidateDemo()
        {
            Console.WriteLine("Type a date as dd/MM/yyyy. Type 'cancel' to go back.");

            DateTime date;
            if (!TryReadDate("Date: ", out date))
                return;

            Console.WriteLine("Accepted: " + date.ToString("dddd dd MMMM yyyy"));
            Console.WriteLine("In the future: " + (date > DateTime.Today));
        }

        public static bool TryReadDate(string prompt, out DateTime result)
        {
            result = default(DateTime);

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (input == null)
                    return false;

                input = input.Trim();

                if (input.Equals("cancel", StringComparison.OrdinalIgnoreCase))
                    return false;

                bool ok = DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out result);

                if (!ok)
                {
                    Console.WriteLine("Not a valid date. Use dd/MM/yyyy.");
                    continue;
                }

                return true;
            }
        }
    }
}
