#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short? Year { get; set; }
        public double Revenue { get; set; }

        [DisplayName("Director")]
        [Required]
        public int? DirectorId { get; set; }

        [DisplayName("Director")]
        public string DirectorOutput { get; set; }

        [DisplayName("genras")]
        public List<int> GenrasIdsInput { get; set; }

        [DisplayName("genras")]
        public string GenrasNamesOutput { get; set; }
    }

}
