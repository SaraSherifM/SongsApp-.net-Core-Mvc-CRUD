using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SongsApp.Models
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }
        public string SongName { get; set; }
        public int ReleaseDate { get; set; }
        public string AlbumName { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey("Singer")]
        public int SingerId { get; set; }

        public virtual Singer Singers { get; set; }

    }
}
