namespace Service
{
    public class PurchaseService
    {
        private static PurchaseService _instance;
        public static PurchaseService Instance
        {
            get { return GetInstance(); }
        }

        private static PurchaseService GetInstance()
        {
            if (_instance is null)
            {
                _instance = new PurchaseService();
            }
            return _instance;
        }

        private PurchaseService() { }
    }
}