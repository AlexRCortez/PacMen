using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMen.BL.Models
{
    public class User
    {

        [Required]
        public List<User> Users { get; set; }
        public Guid Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }

        public Guid ScoreId { get; set; }
        public Score Score { get; set; }
    }
}
