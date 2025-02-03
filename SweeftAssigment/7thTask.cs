using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace SchoolApp
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string SubjectArea { get; set; }
        public ICollection<Student> Students { get; set; }
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Grade { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }

    public class SchoolDbContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Students)
                .WithMany(s => s.Teachers)
                .UsingEntity(j => j.ToTable("TeacherStudents"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-DSAR04C;Database=AdventureWorks2022;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        }
    }

    public class SchoolService
    {
        private readonly SchoolDbContext _context;

        public SchoolService(SchoolDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Teacher> FetchTeachersByStudent(string studentFirstName)
        {
            return _context.Teachers
                           .Where(t => t.Students.Any(s => s.FirstName == studentFirstName))
                           .ToList();
        }
    }

    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    using (var dbContext = new SchoolDbContext())
        //    {
        //        var service = new SchoolService(dbContext);
        //        var teachersForStudent = service.FetchTeachersByStudent("Giorgi");

        //        foreach (var teacher in teachersForStudent)
        //        {
        //            Console.WriteLine($"{teacher.FirstName} {teacher.LastName}");
        //        }
        //    }
        //}

        //              ↑↑↑↑ უშუალო გამოყენების მაგალითი ↑↑↑↑
        // !! დაკომენტარებულია რადგან არ შეიძლება არსებობდეს ორი ცალი Entry point. !! 
    }
}

