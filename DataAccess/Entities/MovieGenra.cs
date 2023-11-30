#nullable disable

using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [PrimaryKey(nameof(MovieId), nameof(GenraId))]
    public class MovieGenra
    {
        [Column(Order = 0)]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [Column(Order = 1)]
        public int GenraId { get; set; }
        public Genra Genra { get; set; }
    }
}