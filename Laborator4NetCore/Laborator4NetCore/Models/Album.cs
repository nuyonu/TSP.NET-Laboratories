using System;
using System.Collections.Generic;

namespace Laborator4NetCore.Models
{
    public class Album
    {
        protected Album() 
        {
        }
        public int AlbumId { get; set; }
        public ICollection<AlbumArtist> AlbumArtists { get; set; }
        public string AlbumName { get; set; }
    }
}