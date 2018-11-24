using Entities;
using System.Linq;

namespace Data
{
    public class UserSectionViewDataClass
    {
        private ChatDBEntities entities = null;
        public UserSectionViewDataClass()
        {
            entities = new ChatDBEntities();
        }

        public string GetUserNameByConectionId(string conectionId)
        {

            UserSectionView userView = entities.UserSectionViews.Where(p => p.ConnectionId == conectionId).FirstOrDefault();
            if (userView == null)
            {
                return "";
            }
            return userView.Name + " " + userView.LastName;
        }
    }
}
