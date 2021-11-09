using System;

namespace Domain
{
    public class Log
    {
        public string Date { get; set; }
        public string Hour { get; set; }
        public string User { get; set; }
        public string Game { get; set; }
        public string Action { get; set; }

        public Log()
        { }
    }
}