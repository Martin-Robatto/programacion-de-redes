namespace ServerAdmin.Models
{
    public class GameModelIn
    {
        public string User { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Synopsis { get; set; }

        public string ParseToPostFormat()
        {
            return $"{User}&{Title}#{Genre}#{Synopsis}";
        }
        
        public string ParseToDeleteFormat()
        {
            return $"{User}&{Title}";
        }
        
        public string ParseToPutFormat(string oldTitle)
        {
            return $"{User}&{oldTitle}&{Title}#{Genre}#{Synopsis}";
        }
    }
}