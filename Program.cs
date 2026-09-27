
using System;

namespace SessionsManager
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                ShowMenu();
                Console.Write("Choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    running = Dispatch(choice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                if (running)
                {
                    Console.WriteLine();
                    Console.Write("Press Enter to continue...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }

            Console.WriteLine("Bye.");
        }

        private static void ShowMenu()
        {
            Console.WriteLine("=========== SESSIONS MANAGER ===========");
            Console.WriteLine("1. Create project + starter arrays");
            Console.WriteLine("2. Display all sessions");
            Console.WriteLine("3. Search session");
            Console.WriteLine("4. Array methods");
            Console.WriteLine("5. Duration analysis");
            Console.WriteLine("6. Functions");
            Console.WriteLine("7. ref / out / reference types");
            Console.WriteLine("8. params");
            Console.WriteLine("9. Session date details");
            Console.WriteLine("10. Date difference");
            Console.WriteLine("11. Past / upcoming");
            Console.WriteLine("12. Next session");
            Console.WriteLine("13. Date formatting");
            Console.WriteLine("14. Read & validate date");
            Console.WriteLine("15. Exception handling");
            Console.WriteLine("16. Invalid array index");
            Console.WriteLine("17. throw");
            Console.WriteLine("18. finally");
            Console.WriteLine("19. String report");
            Console.WriteLine("20. StringBuilder report");
            Console.WriteLine("0. Exit");
            Console.WriteLine("=========================================");
        }

        private static bool Dispatch(string choice)
        {
            switch (choice)
            {
                case "1": SessionStore.Reset(); 
                    break;
                case "2": SessionStore.DisplayAll();
                    break;
                case "3": SessionStore.SearchMenu();
                    break;
                case "4": SessionStore.ArrayMethodsDemo(); 
                    break;
                case "5": SessionStore.DurationAnalysis(); 
                    break;
                case "6": SessionStore.FunctionsDemo();
                    break;
                case "7": SessionStore.RefOutDemo(); 
                    break;
                case "8": SessionStore.ParamsDemo();
                    break;
                case "9": DateTools.ShowDateDetails(); 
                    break;
                case "10": DateTools.DateDifference();
                    break;
                case "11": DateTools.PastAndUpcoming();
                    break;
                case "12": DateTools.ShowNextSession();
                    break;
                case "13": DateTools.FormattingDemo(); 
                    break;
                case "14": DateTools.ReadAndValidateDemo(); 
                    break;
                case "15": ErrorDemos.ExceptionHandlingDemo();
                    break;
                case "16": ErrorDemos.InvalidIndexDemo(); 
                    break;
                case "17": ErrorDemos.ThrowDemo(); 
                    break;

                case "18": ErrorDemos.FinallyDemo();
                    break;
                case "19": Reports.StringReport();
                    break;
                case "20": Reports.StringBuilderReport();
                    break;
                case "0": return false;
                default: Console.WriteLine("Pick a number from 0 to 20.");
                    break;
            }

            return true;
        }
    }
}
