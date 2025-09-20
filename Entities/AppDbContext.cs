using Microsoft.EntityFrameworkCore;

namespace SzakdolgozatBackend.Entities
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonTime> LessonTimes { get; set; }
        public DbSet<Signature> Signatures { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;Database=RollcallDb;Trusted_Connection=True;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Type)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Lessons)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<Lesson>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.User)
                .WithMany(u => u.Lessons)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.LessonTimes)
                .WithOne(lt => lt.Lesson)
                .HasForeignKey(lt => lt.LessonId);

            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.Signatures)
                .WithOne(s => s.Lesson)
                .HasForeignKey(s => s.LessonId);

            modelBuilder.Entity<LessonTime>()
                .HasKey(lt => lt.Id);

            modelBuilder.Entity<LessonTime>()
                .HasOne(lt => lt.Lesson)
                .WithMany(l => l.LessonTimes);

            modelBuilder.Entity<Signature>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Signature>()
                .HasOne(s => s.Lesson)
                .WithMany(l => l.Signatures);

            modelBuilder.Entity<Signature>()
                .HasOne(s => s.Student)
                .WithMany(st => st.Signatures)
                .HasForeignKey(s => s.StudentNeptunCode);

            //might need to store the image in a varbinary or blob 
            //modelBuilder.Entity<Signature>()
            //    .Property(s => s.SignatureBase64)
            //    .HasColumnType("blob");

            modelBuilder.Entity<Student>()
                .HasKey(st => st.NeptunCode);

            modelBuilder.Entity<Student>()
                .HasMany(st => st.Signatures)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentNeptunCode);
        }
    }
}
