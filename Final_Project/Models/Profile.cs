
﻿using System;
using System.Collections.Generic; 
﻿

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Project.Models
{
    public partial class Profile
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Phone { get; set; }
        public string? ImageURL { get; set; }


        public string? Address { get; set; }
        public string? Gender { get; set; } 

        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "please choose Front image")]
        [Display(Name = "Avatar")]
        [NotMapped]
        public IFormFile? Avatar { get; set; }
    }
}
