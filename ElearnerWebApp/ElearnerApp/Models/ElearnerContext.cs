namespace ElearnerApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ElearnerContext : DbContext
    {
        public ElearnerContext ()
            : base("name=ElearnerContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<BankAccount> BankAccounts { get; set; }
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
                .HasRequired(e => e.BankAccount)
                .WithRequiredPrincipal(e => e.Account);

            modelBuilder.Entity<Account>()
                .HasOptional(e => e.Student)
                .WithRequired(e => e.Account);

            modelBuilder.Entity<Account>()
                .HasOptional(e => e.Teacher)
                .WithRequired(e => e.Account);

            modelBuilder.Entity<BankAccount>()
                .Property(e => e.Deposit)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Content>()
                .Property(e => e.TextContent)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.VideoUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Course>()
                .HasOptional(e => e.Content)
                .WithRequired(e => e.Course);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Subscriptions)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.QuestionStr)
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
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Lastname)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Teacher)
                .HasForeignKey(e => e.TeacherId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subscription>()
                .Property(e => e.Comment)
                .IsUnicode(false);
        }
    }
}
