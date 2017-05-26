namespace ELearnerAppDemo
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ElearnerContext : DbContext
    {
        public ElearnerContext ()
            : base("name=ELearnerDbContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating (DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Teachers)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.TextContent)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.VideoURL)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Content)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.Cours)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Subscriptions)
                .WithRequired(e => e.Cours)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Question1)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Lastname)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Subscriptions)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Lastname)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Teacher)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subscription>()
                .Property(e => e.Comment)
                .IsUnicode(false);
        }
    }
}
