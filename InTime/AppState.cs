using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InTime.ServiceReference1;

namespace InTime
{
   public class AppState
    {
        public SoundState sound;
        public Client_User user;
        public AppState()
        {
           
        }

        public AppState(Client_User user)
        {
            this.user = user;
        }
    }
}
