using System;

namespace Domain
{
    public class Log
    {
        public DateTime Date { get; set; }
        public User User { get; set; }
        public Game Game { get; set; }

        public Log()
        { }
    }
}