using System;

namespace Domain
{
    public class Publish
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        
        public Game Game { get; set; }
        public User User { get; set; }
        
        public Publish() { }
    }
}