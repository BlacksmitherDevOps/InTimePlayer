using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InTime
{
   public class AppState
    {
        public SoundState sound;

        public AppState()
        {
            sound = SoundState.HighSound;
        }
    }
}
