using Domain.Entity;
using LearningPlatform.Application.Contracts.Interfaces;
using LearningPlatform.Domain.Common;
using LearningPlatform.Domain.Entities;
using LearningPlatform.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Infrastructure
{
    public class LearningPlatformSystemContext : DbContext
    {
        public LearningPlatformSystemContext()
        {
        }

        private readonly ICurrentUserService currentUserService;

        public LearningPlatformSystemContext(
            DbContextOptions<LearningPlatformSystemContext> options, ICurrentUserService currentUserService) :
            base(options)
        {
            this.currentUserService = currentUserService;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }


        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });
            modelBuilder.Entity<Quiz>().ToTable("Quiz");

            modelBuilder.Entity<Question>()
            .ToTable("Question") // Ensure table is named "Question"
            .HasKey(q => q.Id);

        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Server=127.0.0.1;Port=5433;Database=LearningPlatformSystem;Username=postgres;Password=0806;");
        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            foreach (EntityEntry<AuditableEntity> entry in ChangeTracker
                         .Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = currentUserService.GetCurrentClaimsPrincipal()?.Claims
                        .FirstOrDefault(c => c.Type == "name")?.Value!;
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedBy = currentUserService.GetCurrentClaimsPrincipal()?.Claims
                        .FirstOrDefault(c => c.Type == "name")?.Value!;
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
