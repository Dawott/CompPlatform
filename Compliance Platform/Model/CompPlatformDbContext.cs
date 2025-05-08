using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Compliance_Platform.Model
{
    public class CompPlatformDbContext : IdentityDbContext<CompPlatformUser, IdentityRole, string>
    {
        public CompPlatformDbContext(DbContextOptions options) : base(options)
        {
        }

       // public DbSet<CompPlatformUser> Users { get; set; }
        public DbSet<CompPlatformTool> Tools { get; set; }
        public DbSet<CompPlatformQuestions> Questions { get; set; }
        public DbSet<CompPlatformQuestionnaires> Questionnaires { get; set;}
        public DbSet<CompPlatformInstanceAnswer> InstanceAnswers { get; set; }
        public DbSet<CompPlatformInstance> InstancesTool { get; set; }
        public DbSet<CompPlatformAnswers> Answers { get; set; }
        public DbSet<CompPlatformVerificationHistory> VerificationHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompPlatformInstance>()
        .HasOne(tq => tq.Audytor)
        .WithMany()
        .HasForeignKey(tq => tq.AudytorId)
        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CompPlatformInstanceAnswer>()
        .HasOne(a => a.OpcjaOdpowiedzi)
        .WithMany()
        .HasForeignKey(a => a.OpcjaOdpowiedziId)
        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CompPlatformInstanceAnswer>()
        .HasOne(a => a.Wnioskujacy)
        .WithMany()
        .HasForeignKey(a => a.WnioskujacyId)
        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CompPlatformInstanceAnswer>()
        .HasOne(a => a.CompPlatformQuestions)
        .WithMany()
        .HasForeignKey(a => a.QuestionTemplateId)
        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CompPlatformVerificationHistory>()
               .HasOne(h => h.Instance)
               .WithMany()
               .HasForeignKey(h => h.InstanceId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CompPlatformVerificationHistory>()
                .HasOne(h => h.Auditor)
                .WithMany()
                .HasForeignKey(h => h.AuditorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
