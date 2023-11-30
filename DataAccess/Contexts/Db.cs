using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class Db : DbContext
    {
        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Ganre> Ganres { get; set; }
        public DbSet<MovieGanre> MovieGanres { get; set; }

        public Db(DbContextOptions options) : base(options)
        {

        }
    }
}
