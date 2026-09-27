
using System;

namespace SessionsManager
{
    public class InvalidSessionException : Exception
    {
        public int SessionId { get; set; }

        public InvalidSessionException() { }

        public InvalidSessionException(string message) : base(message) { }

        public InvalidSessionException(string message, int sessionId) : base(message)
        {
            SessionId = sessionId;
        }
    }

    public static class ErrorDemos
    {
        public static void ExceptionHandlingDemo()
        {
            Console.Write("Enter a duration in minutes: ");
            string input = Console.ReadLine();

            try
            {
                int minutes = int.Parse(input);
                int sessionsPerDay = 480 / minutes;

                Console.WriteLine("Parsed value: " + minutes);
                Console.WriteLine("Fits in an 8h day: " + sessionsPerDay);
            }
            catch (FormatException)
            {
                Console.WriteLine("That is not a number.");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("A session cannot be 0 minutes long.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong: " + ex.Message);
            }
        }

        public static void InvalidIndexDemo()
        {
            Session[] items = SessionStore.Sessions;

            Console.WriteLine("Array length = " + items.Length);

            try
            {
                Console.WriteLine("Reading items[" + items.Length + "]");
                Console.WriteLine(items[items.Length]);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("IndexOutOfRangeException caught.");
            }

            Console.Write("Enter an index to read: ");
            int index;

            if (!int.TryParse(Console.ReadLine(), out index))
            {
                Console.WriteLine("Not a number.");
            }
            else if (index < 0 || index >= items.Length)
            {
                Console.WriteLine("Out of range.");
            }
            else
            {
                Console.WriteLine(items[index]);
            }
        }

        public static void ThrowDemo()
        {
            try
            {
                Validate(new Session(0, "", "Nobody", DateTime.Today, -30));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("caught: " + ex.Message);
            }

            try
            {
                GetSessionOrThrow(999);
            }
            catch (InvalidSessionException ex)
            {
                Console.WriteLine("caught InvalidSessionException: " + ex.Message);
                Console.WriteLine("id: " + ex.SessionId);
            }
        }

        public static void Validate(Session s)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (string.IsNullOrWhiteSpace(s.Title))
                throw new ArgumentException("Session title is required.");

            if (s.DurationMinutes <= 0)
                throw new ArgumentException("Duration must be greater than zero.");
        }

        public static Session GetSessionOrThrow(int id)
        {
            Session found = SessionStore.FindById(id);

            if (found == null)
                throw new InvalidSessionException("No session exists with id " + id, id);

            return found;
        }

        public static void FinallyDemo()
        {
            Console.WriteLine("CASE A - no exception");
            RunStep(10);

            Console.WriteLine();
            Console.WriteLine("CASE B - exception, caught here");
            RunStep(0);
        }

        private static void RunStep(int divisor)
        {
            try
            {
                int result = 100 / divisor;
                Console.WriteLine("result = " + result);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("caught: divided by zero");
            }
            finally
            {
                Console.WriteLine("finally: done");
            }
        }
    }
}
