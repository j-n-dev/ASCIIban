using System;
using System.IO;

namespace ASCIIban
{
    internal class Playlist
    {
        private readonly string[] playlist;
        private int index = -1;
        public bool PlaylistWon { get; private set; } = false;

        public Playlist(string path)
        {
            playlist = File.ReadAllLines(path);
        }

        public string ReadNext()
        {
            if (index == playlist.Length - 2) PlaylistWon = true;
            index++;
            return playlist[index];
        }

        public string ReadCurrent()
        {
            if (index >= 0) return playlist[index];
            else return null;
        }
    }
}
