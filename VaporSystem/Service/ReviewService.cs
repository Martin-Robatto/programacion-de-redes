using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ReviewService
    {
        private static ReviewService _instance;
        public static ReviewService Instance
        {
            get { return GetInstance(); }
        }

        private static ReviewService GetInstance()
        {
            if (_instance is null)
            {
                _instance = new ReviewService();
            }
            return _instance;
        }

        private ReviewService() { }
        
        
    }
}
