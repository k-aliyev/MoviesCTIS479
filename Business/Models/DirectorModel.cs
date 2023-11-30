#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class DirectorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }

        [DisplayName("BirthDate")]
        public string BirthDateOutput { get; set; }

        [DisplayName("Retired")]
        public bool IsRetired { get; set; }
    }

}
