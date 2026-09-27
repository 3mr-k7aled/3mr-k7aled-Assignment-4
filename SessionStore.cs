
using System;

namespace SessionsManager
{
    public static class SessionStore
    {
        private static Session[] _sessions = CreateStarterArray();

        public static Session[] Sessions
        {
            get { return _sessions; }
        }

        public static string[] Titles;
        public static DateTime[] Dates;
        public static int[] Durations;

        public static Session[] CreateStarterArray()
        {
            DateTime today = DateTime.Today;

            Session[] sessions = new Session[]
            {
                new Session(1, "C# Fundamentals",      "Mona Adel",   today.AddDays(-21).AddHours(10), 120),
                new Session(2, "OOP in C#",             "Karim Fouad", today.AddDays(-14).AddHours(13), 90),
                new Session(3, "Arrays & Collections",  "Mona Adel",   today.AddDays(-7).AddHours(10), 150),
                new Session(4, "LINQ Basics",           "Sara Nabil",  today.AddDays(-2).AddHours(16), 60),
                new Session(5, "Exception Handling",    "Karim Fouad", today.AddDays(3).AddHours(10), 105),
                new Session(6, "Working with Dates",    "Sara Nabil",  today.AddDays(6).AddHours(14), 75),
                new Session(7, "Strings & Text",        "Mona Adel",   today.AddDays(12).AddHours(11), 180),
                new Session(8, "ADO.NET Intro",         "Hany Samir",  today.AddDays(20).AddHours(9), 240)
            };

            Titles = new string[sessions.Length];
            Dates = new DateTime[sessions.Length];
            Durations = new int[sessions.Length];

            for (int i = 0; i < sessions.Length; i++)
            {
                Titles[i] = sessions[i].Title;
                Dates[i] = sessions[i].Date;
                Durations[i] = sessions[i].DurationMinutes;
            }

            return sessions;
        }

        public static void Reset()
        {
            private _sessions = CreateStarterArray();
            Console.WriteLine("Sessions array length: " + _sessions.Length);
            Console.WriteLine("Titles array length: " + Titles.Length);
            Console.WriteLine("Durations array length: " + Durations.Length);
        }

        public static void DisplayAll()
        {
            DisplayAll(_sessions, "ALL SESSIONS");
        }

        public static void DisplayAll(Session[] items, string heading = "SESSIONS")
        {
            Console.WriteLine(heading);
            Console.WriteLine(new string('-', 78));

            if (items == null || items.Length == 0)
            {
                Console.WriteLine("No sessions.");
                return;
            }

            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine("[" + i + "] " + items[i]);
            }

            Console.WriteLine(new string('-', 78));
            Console.WriteLine("Count: " + items.Length);
        }

        public static void SearchMenu()
        {
            Console.Write("Search by (1) Id or (2) Title? ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Enter id: ");
                int id;
                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Not a number.");
                    return;
                }

                Session found = FindById(id);
                if (found == null)
                    Console.WriteLine("No session with id " + id);
                else
                    Console.WriteLine(found.ToString());
            }
            else
            {
                Console.Write("Enter part of the title: ");
                string text = Console.ReadLine();
                Session[] matches = FindByTitle(text);

                if (matches.Length == 0)
                    Console.WriteLine("No matches for \"" + text + "\"");
                else
                    DisplayAll(matches, "MATCHES");
            }
        }

        public static Session FindById(int id)
        {
            for (int i = 0; i < _sessions.Length; i++)
            {
                if (_sessions[i].Id == id)
                    return _sessions[i];
            }
            return null;
        }

        public static Session[] FindByTitle(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new Session[0];

            Session[] buffer = new Session[_sessions.Length];
            int count = 0;

            for (int i = 0; i < _sessions.Length; i++)
            {
                if (_sessions[i].Title.ToLower().Contains(text.Trim().ToLower()))
                {
                    buffer[count] = _sessions[i];
                    count++;
                }
            }

            Session[] result = new Session[count];
            Array.Copy(buffer, result, count);
            return result;
        }

        public static void ArrayMethodsDemo()
        {
            Session[] work = new Session[_sessions.Length];
            Array.Copy(_sessions, work, _sessions.Length);
            Console.WriteLine("Copy length: " + work.Length);
            Console.WriteLine();

            Array.Sort(work);
            Console.WriteLine("Sort by date, first: " + work[0].Title + ", last: " + work[work.Length - 1].Title);
            Console.WriteLine();

            string[] titles = new string[Titles.Length];
            Array.Copy(Titles, titles, Titles.Length);
            Array.Sort(titles);
            Console.WriteLine("Sort titles A-Z: " + string.Join(" | ", titles));
            Console.WriteLine();

            Array.Reverse(titles);
            Console.WriteLine("Reverse: " + string.Join(" | ", titles));
            Console.WriteLine();

            Session third = _sessions[2];
            Console.WriteLine("IndexOf third session: " + Array.IndexOf(_sessions, third));
            Console.WriteLine("IndexOf \"LINQ Basics\": " + Array.IndexOf(Titles, "LINQ Basics"));
            Console.WriteLine("IndexOf missing title: " + Array.IndexOf(Titles, "Docker"));
            Console.WriteLine();

            bool anyLong = Array.Exists(_sessions, s => s.DurationMinutes > 180);
            Console.WriteLine("Exists duration > 180: " + anyLong);

            Session firstLong = Array.Find(_sessions, s => s.DurationMinutes >= 150);
            Console.WriteLine("Find first >= 150 min: " + (firstLong == null ? "none" : firstLong.Title));

            int idx = Array.FindIndex(_sessions, s => s.Title.StartsWith("OOP"));
            Console.WriteLine("FindIndex title starts OOP: " + idx);
        }

        public static void DurationAnalysis()
        {
            if (_sessions.Length == 0)
            {
                Console.WriteLine("No sessions.");
                return;
            }

            int total = 0;
            Session longest = _sessions[0];
            Session shortest = _sessions[0];

            for (int i = 0; i < _sessions.Length; i++)
            {
                total += _sessions[i].DurationMinutes;
                if (_sessions[i].DurationMinutes > longest.DurationMinutes)
                    longest = _sessions[i];
                if (_sessions[i].DurationMinutes < shortest.DurationMinutes)
                    shortest = _sessions[i];
            }

            double average = (double)total / _sessions.Length;

            Console.WriteLine("Total duration: " + total + " min");
            Console.WriteLine("Average: " + average.ToString("0.0") + " min");
            Console.WriteLine("Longest: " + longest.Title + " (" + longest.DurationMinutes + " min)");
            Console.WriteLine("Shortest: " + shortest.Title + " (" + shortest.DurationMinutes + " min)");
        }

        public static void FunctionsDemo()
        {
            Console.WriteLine("TotalMinutes: " + TotalMinutes(_sessions));
            Console.WriteLine("CountLongerThan default: " + CountLongerThan(_sessions));
            Console.WriteLine("CountLongerThan(150): " + CountLongerThan(_sessions, 150));
            Console.WriteLine("ToHours(150): " + ToHours(150));
            Console.WriteLine("ToHours(session 8): " + ToHours(_sessions[7]));
        }

        public static int TotalMinutes(Session[] items)
        {
            int sum = 0;
            for (int i = 0; i < items.Length; i++)
                sum += items[i].DurationMinutes;
            return sum;
        }

        public static int CountLongerThan(Session[] items, int minutes = 90)
        {
            int count = 0;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].DurationMinutes > minutes)
                    count++;
            }
            return count;
        }

        public static double ToHours(int minutes)
        {
            return Math.Round(minutes / 60.0, 2);
        }

        public static double ToHours(Session session)
        {
            return ToHours(session.DurationMinutes);
        }

        public static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Friday || date.DayOfWeek == DayOfWeek.Saturday;
        }

        public static void RefOutDemo()
        {
            int minutes = 60;
            AddThirty(minutes);
            Console.WriteLine("without ref: " + minutes);

            AddThirtyRef(ref minutes);
            Console.WriteLine("with ref: " + minutes);
            Console.WriteLine();

            Session a = new Session(99, "Temp Session", "Nobody", DateTime.Today, 45);
            Rename(a, "Renamed Inside Method");
            Console.WriteLine("mutate field: " + a.Title);

            Replace(a);
            Console.WriteLine("reassign param (no ref): " + a.Title);

            ReplaceRef(ref a);
            Console.WriteLine("reassign param (with ref): " + a.Title);
            Console.WriteLine();

            Session first = _sessions[0];
            Session second = _sessions[1];
            Console.WriteLine("before swap: " + first.Title + " / " + second.Title);
            Swap(ref first, ref second);
            Console.WriteLine("after swap: " + first.Title + " / " + second.Title);
            Console.WriteLine();

            int total;
            double average;
            Session longest;
            if (TryGetStats(_sessions, out total, out average, out longest))
            {
                Console.WriteLine("out total: " + total);
                Console.WriteLine("out average: " + average.ToString("0.0"));
                Console.WriteLine("out longest: " + longest.Title);
            }
        }

        private static void AddThirty(int m)
        {
            m += 30;
        }

        private static void AddThirtyRef(ref int m)
        {
            m += 30;
        }

        private static void Rename(Session s, string title)
        {
            s.Title = title;
        }

        private static void Replace(Session s)
        {
            s = new Session(100, "Brand New", "-", DateTime.Today, 30);
        }

        private static void ReplaceRef(ref Session s)
        {
            s = new Session(101, "Brand New With Ref", "-", DateTime.Today, 30);
        }

        public static void Swap(ref Session x, ref Session y)
        {
            Session temp = x;
            x = y;
            y = temp;
        }

        public static bool TryGetStats(Session[] items, out int total, out double average, out Session longest)
        {
            total = 0;
            average = 0;
            longest = null;

            if (items == null || items.Length == 0)
                return false;

            longest = items[0];
            for (int i = 0; i < items.Length; i++)
            {
                total += items[i].DurationMinutes;
                if (items[i].DurationMinutes > longest.DurationMinutes)
                    longest = items[i];
            }

            average = (double)total / items.Length;
            return true;
        }

        public static void ParamsDemo()
        {
            Console.WriteLine("SumMinutes(): " + SumMinutes());
            Console.WriteLine("SumMinutes(60): " + SumMinutes(60));
            Console.WriteLine("SumMinutes(60,90,120): " + SumMinutes(60, 90, 120));
            Console.WriteLine("SumMinutes(Durations): " + SumMinutes(Durations));
            Console.WriteLine();

            int before = _sessions.Length;
            AddSessions(
                new Session(9, "Unit Testing", "Sara Nabil", DateTime.Today.AddDays(25).AddHours(10), 90),
                new Session(10, "Git Workflow", "Hany Samir", DateTime.Today.AddDays(28).AddHours(13), 60));

            Console.WriteLine("Sessions before: " + before + " after: " + _sessions.Length);
            DisplayAll(_sessions, "AFTER ADD");
        }

        public static int SumMinutes(params int[] minutes)
        {
            int sum = 0;
            for (int i = 0; i < minutes.Length; i++)
                sum += minutes[i];
            return sum;
        }

        public static void AddSessions(params Session[] newSessions)
        {
            if (newSessions == null || newSessions.Length == 0)
                return;

            Session[] bigger = new Session[_sessions.Length + newSessions.Length];
            Array.Copy(_sessions, bigger, _sessions.Length);
            Array.Copy(newSessions, 0, bigger, _sessions.Length, newSessions.Length);
            _sessions = bigger;
        }
    }
}
