
using System;

namespace SessionsManager
{
    public class Session : IComparable<Session>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Instructor { get; set; }
        public DateTime Date { get; set; }
        public int DurationMinutes { get; set; }

        public Session(int id, string title, string instructor, DateTime date, int durationMinutes)
        {
            Id = id;
            Title = title;
            Instructor = instructor;
            Date = date;
            DurationMinutes = durationMinutes;
        }

        public DateTime EndsAt
        {
            get { return Date.AddMinutes(DurationMinutes); }
        }

        public bool IsPast
        {
            get { return EndsAt < DateTime.Now; }
        }

        public bool IsUpcoming
        {
            get { return !IsPast; }
        }

        public int CompareTo(Session other)
        {
            if (other == null)
                return 1;
            return Date.CompareTo(other.Date);
        }

        public override string ToString()
        {
            return "ID: " + Id + ", Title: " + Title + ", Instructor: " + Instructor +
                ", Date: " + Date.ToString("dd/MM/yyyy HH:mm") + ", Duration: " + DurationMinutes + " min";
        }
    }
}
