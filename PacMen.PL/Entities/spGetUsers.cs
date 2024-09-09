using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMen.PL.Entities
{
    public class spGetUsers
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; } = null!;

        public string? LastName { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? UserName { get; set; } = null!;

        public string? Image { get; set; } = null!;

        public string? Password { get; set; } = null!;

        public Guid ScoreId { get; set; }
    }
}
