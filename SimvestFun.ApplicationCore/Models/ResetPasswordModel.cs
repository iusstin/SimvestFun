using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimvestFun.ApplicationCore.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public string NewPassword { get; set; } 
    }
}
