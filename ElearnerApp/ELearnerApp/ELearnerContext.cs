namespace ELearnerApp
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
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
       // public virtual DbSet<BankAccount> BankAccounts { get; set; }

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
                .Property(a => a.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Teachers)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Course>()
                .Property(e => e.ContentsPath)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Students)
                .WithMany(e => e.Courses)
                .Map(m => m.ToTable("Subscriptions").MapLeftKey("CourseId").MapRightKey("StudentId"));

            modelBuilder.Entity<Student>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Student>()
                .Property(e => e.Name)
                .IsUnicode(false)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(e => e.Lastname)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Lastname)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Email)
                .IsUnicode(false);

            //modelBuilder.Entity<BankAccount>()
            //    .Property(b => b.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //modelBuilder.Entity<BankAccount>()
            //    .Property(b => b.Deposit)
            //    .HasPrecision(10, 2);

            //modelBuilder.Entity<Account>()
            //    .HasRequired(b => b.BankAccount)
            //    .WithRequiredPrincipal(b => b.Account);
        }
    }
}
