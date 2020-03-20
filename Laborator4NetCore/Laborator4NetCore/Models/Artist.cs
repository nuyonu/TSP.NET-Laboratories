using System;
using System.Collections.Generic;

namespace Laborator4NetCore.Models
{
    public class Artist
    {
        public Artist() 
        {
        }
        public int ArtistId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<AlbumArtist> AlbumArtists { get; set; }
    }
}
