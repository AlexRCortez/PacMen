using System;
using System.Collections.Generic;

#nullable disable

namespace PacMen.PL.Entities;

public class tblUser : IEntity
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Guid ScoreId { get; set; }

    public virtual ICollection<tblScore> Scores { get; set; }
    public virtual tblScore Score { get; set; }

}
