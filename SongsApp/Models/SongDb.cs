using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsApp.Models
{
    public class SongDb:DbContext
    {
        public SongDb(DbContextOptions options):base(options)
        {
        }

        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<Singer> Singers { get; set; }
    }

}
