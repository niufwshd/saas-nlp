using GovTown;
using Microsoft.EntityFrameworkCore;
using Model;

namespace WebApplication5.Data
{
    public class SchoolContext:DbContext
    {
        public SchoolContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<SqlServerInfo> sqlServerInfos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Course>().ToTable("Course");
            //modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            //modelBuilder.Entity<Student>().ToTable("Student");
            //modelBuilder.Entity<SqlServerInfo>().ToTable("SqlServerInfo");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=school;User Id=sa;Password=pearsink;");
        }
    }
}
