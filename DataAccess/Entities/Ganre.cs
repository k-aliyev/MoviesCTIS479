#nullable disable

using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Genra : Record
    {
        [MaxLength(75)]
        public string Name { get; set; }
        public List<MovieGenra> MovieGenras { get; set; }
    }
}