using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DatabaseModels
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        
        public int Userid { get; set; }
        [Required(ErrorMessage = "Please enter your name.")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Please enter your email.")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Please enter your password.")]
        public string Password { get; set; }
        public int? Defaultstore { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
