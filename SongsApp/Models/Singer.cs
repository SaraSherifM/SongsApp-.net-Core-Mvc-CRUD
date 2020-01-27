using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SongsApp.Models
{
    public class Singer
    {
        [Key]
        public int SingerId { get; set; }
        public string SingerName { get; set; }

        public virtual ICollection<Song> Songs { get; }

        public Singer()
        {
            Songs = new HashSet<Song>();
        }
    }
}
