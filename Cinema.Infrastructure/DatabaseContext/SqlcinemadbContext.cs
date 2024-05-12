using Cinema.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Core.Domain.Entities;

public partial class SqlcinemadbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public SqlcinemadbContext()
    {
    }

    public SqlcinemadbContext(DbContextOptions<SqlcinemadbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<CinemaHall> CinemaHalls { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieCategory> MovieCategories { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder); // Important!

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Director).HasMaxLength(50);
            entity.Property(e => e.ReleaseDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<MovieCategory>(entity =>
        {
            entity.HasKey(mc => mc.Id);

            entity.HasOne(d => d.Category).WithMany()
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovieCategories_Categories");

            entity.HasOne(d => d.Movie).WithMany()
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovieCategories_Movies");
        });

        modelBuilder.Entity<CinemaHall>(entity =>
        {
            entity.HasKey(ch => ch.Id);
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(s => s.CinemaHall)
                .WithMany(ch => ch.Sessions)
                .HasForeignKey(s => s.CinemaHallId)
                .OnDelete(DeleteBehavior.NoAction);
            
            entity.HasOne(s => s.Movie)
                .WithMany(m => m.Sessions)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.Id); 

            entity.Property(e => e.Row).IsRequired();
            entity.Property(e => e.Number).IsRequired();

            entity.HasOne(s => s.Session)
                .WithMany(ch => ch.Seats)
                .HasForeignKey(s => s.SessionId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
