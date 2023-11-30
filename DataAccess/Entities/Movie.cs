#nullable disable
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Movie : Record
    {
        [MaxLength(150)]
        public string Name { get; set; }
        public short? Year { get; set; }
        public double Revenue { get; set; }
        public int? DirectorId { get; set; }
        public Director Director { get; set; }
        public List<MovieGenra> MovieGenras { get; set; }
    }
}