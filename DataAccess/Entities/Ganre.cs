#nullable disable

using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Ganre : Record
    {
        [MaxLength(75)]
        public string Name { get; set; }
        List<MovieGanre> MovieGanres { get; set; }
    }
}