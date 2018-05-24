using System.Linq;

namespace DataAccessLayer
{
    public class UserRepository 
    {
        private ContactDBContext contactModel;
        public UserRepository(ContactDBContext contactModel)
        {
            this.contactModel = contactModel;
        }
        
        public bool ValidateUser(string username, string password)
        {
            var user = contactModel.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
            return user != null;
        }
    }
}
