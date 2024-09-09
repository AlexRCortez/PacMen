using System;
using System.Collections.Generic;

#nullable disable

namespace PacMen.PL.Entities;

public class tblScore : IEntity
{
    public Guid Id { get; set; }

   // public Guid UserId { get; set; }

    public int Score { get; set; }

    public DateTime Date { get; set; }

    public int Level { get; set; }


    //public virtual tblUser User { get; set; }

    public virtual ICollection<tblUser> Users { get; set; }
}
