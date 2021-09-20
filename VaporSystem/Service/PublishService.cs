using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using Exceptions;

namespace Service
{
    public class PublishService
    {
        private static PublishService _instance;
        public static PublishService Instance
        {
            get { return GetInstance(); }
        }

        private static PublishService GetInstance()
        {
            if (_instance is null)
            {
                _instance = new PublishService();
            }
            return _instance;
        }

        private PublishService() { }

        public void Save(string publishLine)
        {
            string[] publishAttributes = publishLine.Split("&");
            User user = UserService.Instance.Get(publishAttributes[0]);
            Game game = GameService.Instance.Save(publishAttributes[1]);
            Publish inputPublish = new Publish()
            {
                Id = Guid.NewGuid(),
                User = user,
                Game = game,
                Date = DateTime.Now
            };
            PublishRepository.Get().Add(inputPublish);
        }

        public string Get(string userLine)
        {
            string publishes = string.Empty;
            IEnumerable<Publish> userPublishes = PublishRepository.Get().Where(publish => publish.User.Username.Equals(userLine));
            foreach (Publish publish in userPublishes)
            {
                publishes += "#" + publish.Game.Title;
            }
            if (userPublishes.Count() == 0)
            {
                throw new NotFoundException("Publishes");
            }
            return publishes;
        }
    }
}
