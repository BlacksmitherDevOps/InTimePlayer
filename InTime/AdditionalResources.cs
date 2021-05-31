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

   public enum RepeatState
   {
       NoRepeat,
       RepeatPlaylist,
       RepeatSong
       
   }
   public class PlaylistItem
    {
        public int ID { get; set; }
        public string SongTitle { get; set; }
        public string SongArtist { get; set; }
        public string SongAlbum { get; set; }
        public DateTime SongDuration { get; set; }
    }
    public class AlbumItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public ICollection<PlaylistItem> Songs { get; set; }
    }
    public class SingerItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<AlbumItem> Albums { get; set; }
    }
}