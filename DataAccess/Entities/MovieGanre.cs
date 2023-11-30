#nullable disable

using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [PrimaryKey(nameof(MovieId), nameof(GanreId))]
    public class MovieGanre
    {
        [Column(Order = 0)]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [Column(Order = 1)]
        public int GanreId { get; set; }
        public Ganre Ganre { get; set; }
    }
}