using ASPCORE.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPCORE.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        [NotMapped]
        public DbSet<StudentCourse> StudentCourses{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "InMem");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<StudentCourse>().ToTable("StudentCource");
            base.OnModelCreating(modelBuilder);//Add this if error genereate 'IdentityUserLogin<string>' requires a primary key to be defined keyless

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Pankaj", Enrolled = DateTime.Parse("02/05/2023 5:50 PM") });
        }
        public DbSet<Test> Test { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
    }
}
