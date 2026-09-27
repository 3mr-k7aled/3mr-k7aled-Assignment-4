
using System;
using System.Text;

namespace SessionsManager
{
    public static class Reports
    {
        public static void StringReport()
        {
            Session[] items = SessionStore.Sessions;
            string report = "";

            report = report + "========================================\n";
            report = report + "TRAINING SESSIONS REPORT\n";
            report = report + "========================================\n";

            int total = 0;
            for (int i = 0; i < items.Length; i++)
            {
                Session s = items[i];
                total = total + s.DurationMinutes;
                report = report + s.Id + " " + s.Title + " " + s.Date.ToString("dd/MM/yyyy") + " " + s.DurationMinutes + " min\n";
            }

            report = report + "----------------------------------------\n";
            report = report + "TOTAL: " + total + " min";

            Console.WriteLine(report);
            Console.WriteLine();

            string sample = "   C# Fundamentals , OOP in C# , LINQ Basics   ";

            Console.WriteLine("Trim: " + sample.Trim());
            Console.WriteLine("ToUpper: " + sample.Trim().ToUpper());
            Console.WriteLine("Replace: " + sample.Trim().Replace(',', ';'));
            Console.WriteLine("Contains OOP: " + sample.Contains("OOP"));
            Console.WriteLine("Substring(3,5): " + sample.Substring(3, 5));

            string[] parts = sample.Split(',');
            Console.WriteLine("Split count: " + parts.Length);

            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim();
                Console.WriteLine("Part " + i + ": " + parts[i]);
            }
        }

        public static void StringBuilderReport()
        {
            Session[] items = SessionStore.Sessions;
            StringBuilder sb = new StringBuilder();

            sb.Append("========================================\n");
            sb.Append("TRAINING SESSIONS REPORT\n");
            sb.Append("========================================\n");

            int total = 0;
            for (int i = 0; i < items.Length; i++)
            {
                Session s = items[i];
                total = total + s.DurationMinutes;
                sb.Append(s.Id + " " + s.Title + " " + s.Date.ToString("dd/MM/yyyy") + " " + s.DurationMinutes + " min\n");
            }

            sb.Append("----------------------------------------\n");
            sb.Append("TOTAL: " + total + " min");

            Console.WriteLine(sb.ToString());
        }
    }
}
