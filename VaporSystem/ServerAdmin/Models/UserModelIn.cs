namespace ServerAdmin.Models
{
    public class UserModelIn
    {
        public string Username { get; set; }
        public string Password { get; set; }
        
        public string Parse()
        {
            return $"{Username}#{Password}";
        }
        
        public string ParseToPutFormat(string oldUsername)
        {
            return $"{oldUsername}&{Username}#{Password}";
        }
    }
}