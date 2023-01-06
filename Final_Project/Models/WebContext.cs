using Microsoft.EntityFrameworkCore;
using Final_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models;
public partial class WebContext : DbContext
{
    public WebContext()
    { }
    public WebContext(DbContextOptions<WebContext> options) : base(options)
    { }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Profile> Profiles { get; set; }
    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = .; Database = Web; User id = admin; password = 123; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Profile>(entity =>
        {

            entity.Property(e => e.Id).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50); 
            entity.Property(e => e.Phone).HasMaxLength(50); 
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.ImageURL).HasMaxLength(50);
            entity.Property(e => e.DOB).HasColumnType("datetime");  
        });
        modelBuilder.Entity<User>(entity =>
        { 
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false); 
        });
        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.NameProduct)
                .HasMaxLength(50);
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Number)
                .HasMaxLength(50)
                .IsUnicode(false); 
            entity.Property(e => e.ImageProduct).HasMaxLength(50);
        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
} 
