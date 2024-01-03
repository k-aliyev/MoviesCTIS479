#nullable disable 

using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [DisplayName("User Name")]
        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(10, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(8, MinimumLength = 5, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string Password { get; set; }

        public Status Status { get; set; }

        [DisplayName("Role")]
        [Required(ErrorMessage = "{0} is required!")]
        public int? RoleId { get; set; }

        [DisplayName("Role")]
        public string RoleNameOutput { get; set; }
    }
}
