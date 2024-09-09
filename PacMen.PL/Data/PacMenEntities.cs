using Microsoft.EntityFrameworkCore;
using PacMen.PL.Entities;

namespace PacMen.PL.Data;

public class PacMenEntities : DbContext
{

    Guid[] scoreId = new Guid[3];
    Guid[] userId = new Guid[3];

    public PacMenEntities()
    {
    }

    public PacMenEntities(DbContextOptions<PacMenEntities> options) : base(options)
    {

    }
    

    public virtual DbSet<tblScore> tblScores { get; set; }

    public virtual DbSet<tblUser> tblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=server-31591-300054925.database.windows.net;Database=bigprojectdb;User ID=300054925db;Password=Test123!");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        CreateScores(modelBuilder);
        CreateUsers(modelBuilder);
    }

    protected void CreateScores(ModelBuilder modelBuilder)
    {

        for (int i = 0; i < scoreId.Length; i++)
            scoreId[i] = Guid.NewGuid();

        modelBuilder.Entity<tblScore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tblScore_Id");

            entity.ToTable("tblScore");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("datetime");

            modelBuilder.Entity<spGetScores>().HasNoKey();

            //entity.HasOne(d => d.User)
            //     .WithMany(p => p.Scores)
            //     .HasForeignKey(d => d.UserId)
            //     .OnDelete(DeleteBehavior.ClientSetNull)
            //     .HasConstraintName("fk_tblScore_UserId");
        });

        List<tblScore> Scores = new List<tblScore>
            {
            //UserId = userId[0],
                new tblScore {Id = scoreId[0],  Score = 50, Date = new DateTime(2024, 5, 8),Level = 3},
                new tblScore {Id = scoreId[1], Score = 66, Date = new DateTime(2024, 1, 15),Level = 8},
                new tblScore {Id = scoreId[2],  Score = 100, Date = new DateTime(2024, 2, 20),Level = 100},
            };
        modelBuilder.Entity<tblScore>().HasData(Scores);

    }


    protected void CreateUsers(ModelBuilder modelBuilder)
    {
        for (int i = 0; i < userId.Length; i++)
            userId[i] = Guid.NewGuid();

        modelBuilder.Entity<tblUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tblUser_Id");

            entity.ToTable("tblUser");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            entity.Property(e => e.Image).IsUnicode(false)
            .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            entity.Property(e => e.LastName)
                .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            entity.Property(e => e.Password)
                .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            entity.Property(e => e.UserName)
                .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

            entity.HasOne(d => d.Score)
                 .WithMany(p => p.Users)
                 .HasForeignKey(d => d.ScoreId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("fk_tblUser_ScoreId");

            modelBuilder.Entity<spGetUsers>().HasNoKey();
        });

        List<tblUser> Users = new List<tblUser>
            {
                new tblUser {   Id = userId[0],
                                FirstName = "Alex",
                                LastName = "Rosas",
                                Email = "arosas@gmail.com",
                                UserName = "arosas",
                                Image = "arosas",
                                Password = GetHash("rosas123"),
                                ScoreId = scoreId[0],
                             },

                new tblUser {   Id = userId[1],
                                FirstName = "User1",
                                LastName = "us",
                                Email = "user1.com",
                                UserName = "user1",
                                Image = "user.img",
                                Password = GetHash("user123"),
                                ScoreId = scoreId[1],
                             },

                new tblUser {   Id = userId[2],
                                FirstName = "User2",
                                LastName = "us2",
                                Email = "user2.com",
                                UserName = "user2",
                                Image = "user2.img",
                                Password = GetHash("user12345"),
                                ScoreId = scoreId[2],
                             },
            };
        modelBuilder.Entity<tblUser>().HasData(Users);
    }

    private static string GetHash(string Password)
    {
        using (var hasher = new System.Security.Cryptography.SHA1Managed())
        {
            var hashbytes = System.Text.Encoding.UTF8.GetBytes(Password);
            return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
        }
    }

    // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
