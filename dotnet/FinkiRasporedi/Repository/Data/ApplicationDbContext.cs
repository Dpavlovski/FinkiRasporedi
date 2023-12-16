﻿using FinkiRasporedi.Models.Base;
using FinkiRasporedi.Models.Domain;
using FinkiRasporedi.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinkiRasporedi.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<Student>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Course>? Courses { get; set; }

        public DbSet<Professor>? Professors { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Lecture> Lectures { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<CustomLecture> CustomLectures { get; set; } = default!;

        public DbSet<CourseProfessor> CourseProfessors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Professor>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Room>()
                .HasKey(r => r.Name);
            modelBuilder.Entity<Semester>()
                .HasKey(s => s.Code);
            modelBuilder.Entity<Subject>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Schedule>()
                .HasMany(s => s.Lectures)
                .WithMany()
                .UsingEntity(j => j.ToTable("ScheduleLectures"));
            modelBuilder.Entity<Student>()
               .HasMany(s => s.Schedules)
               .WithMany()
               .UsingEntity(j => j.ToTable("StudentSchedules"));
            modelBuilder.Entity<LectureDetails>()
               .HasOne(s => s.Professor)
               .WithMany();
            modelBuilder.Entity<LectureDetails>()
               .HasOne(s => s.Course)
               .WithMany();
            modelBuilder.Entity<LectureDetails>()
               .HasOne(s => s.Room)
               .WithMany();
            modelBuilder.Entity<Lecture>()
                .HasBaseType<LectureDetails>();
            modelBuilder.Entity<Lecture>()
                .ToTable("Lecture");
            modelBuilder.Entity<CustomLecture>()
                .HasBaseType<LectureDetails>();
            modelBuilder.Entity<CustomLecture>()
                .HasOne(s => s.Lecture);
            modelBuilder.Entity<CustomLecture>()
               .ToTable("CustomLecture");
            modelBuilder.Entity<Course>()
                .HasOne(s => s.Subject);
            modelBuilder.Entity<CourseProfessor>()
               .HasOne(cp => cp.Course)
               .WithMany();
            modelBuilder.Entity<CourseProfessor>()
               .HasOne(cp => cp.Professor)
               .WithMany();
            modelBuilder.Entity<CourseProfessor>()
               .HasKey("CourseId", "ProfessorId");
        }
    }
}