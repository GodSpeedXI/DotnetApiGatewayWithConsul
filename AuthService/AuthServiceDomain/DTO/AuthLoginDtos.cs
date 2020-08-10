using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuthServiceDomain.DTO
{
    public class AuthLoginDtos
    {
        [Required]
        [MaxLength(80)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
    }
}
