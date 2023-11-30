using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class MovieModel
    {
        public string Name { get; set; }
        public short? Year { get; set; }
        public double Revenue { get; set; }

        [DisplayName("Director")]
        public int? DirectorId { get; set; }

        [DisplayName("Ganres")]
        public List<int> GanresIdsInput { get; set; }

        [DisplayName("Ganres")]
        public string GanresNamesOutput { get; set; }
    }

}
