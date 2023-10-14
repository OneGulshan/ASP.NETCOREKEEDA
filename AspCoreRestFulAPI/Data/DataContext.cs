using AspCoreRestFulAPI.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace AspCoreRestFulAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Employee> Employee { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Department>? Departments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().ToTable("User_Info");//Table name change here
            //modelBuilder.Entity<User>().HasOne(_=>_.Department).WithMany(_=>_.Users).HasForeignKey(_=>_.DepartmentId).IsRequired();//Here Performe Cascade table deletion Operation//Here Users also deleted after deleting all departmens
            //modelBuilder.Entity<Department>().HasMany(_ => _.Users).WithOne(_ => _.Department).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasOne(_ => _.Department).WithMany(_ => _.Users).HasForeignKey(_ => _.DepartmentId);//Set Null value after Deletion Cascade table record
            modelBuilder.Entity<Department>().HasMany(_ => _.Users).WithOne(_ => _.Department).OnDelete(DeleteBehavior.SetNull);
            base.OnModelCreating(modelBuilder);
        }
    }
}
