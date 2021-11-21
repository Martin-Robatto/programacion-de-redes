namespace ServerAdmin.Models
{
    public class PurchaseModelIn
    {
        
        public string User { get; set; }
        public string Title { get; set; }
        
        public string Parse()
        {
            return $"{User}&{Title}";
        }
    }
}