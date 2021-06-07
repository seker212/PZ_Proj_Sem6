using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp
{
    public class UserManager
    {
        User User { get; set; }

        public UserManager(User user)
        {
            User = user;
        }

        public bool VerifyUser()
        {
            //TODO: Write body
            return true;
        }

        public void GetUserType()
        {
            //TODO: Write body
        }
 
    }
}
