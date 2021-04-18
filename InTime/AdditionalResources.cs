using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InTime
{
   public enum SoundState
    {
        Muted,
        LowSound,
        MidSound,
        HighSound
    }

   public class PlaylistItem
    {
        public string SongTitle { get; set; }
        public string SongArtist { get; set; }
        public string SongAlbum { get; set; }
        public DateTime SongDuration { get; set; }
    }
}