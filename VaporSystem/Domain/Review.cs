namespace Domain
{
    public class Review
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        
        public Game Game { get; set; }
        public User User { get; set; }
        
        public Review() { }
    }
}