using ContestantRegister.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace ContestantRegister.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

#if DEBUG
        public static readonly ILoggerFactory loggerFactory = new LoggerFactory(new[] {
              new ConsoleLoggerProvider((_, __) => true, true)
        });
        

        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
            //.UseLoggerFactory(loggerFactory)  //tie-up DbContext with LoggerFactory object
            //.EnableSensitiveDataLogging()
            .UseNpgsql("Host=localhost;Database=ContestantRegister-1;Username=postgres;Password=postgres");
#endif
        public DbSet<Area> Areas { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Contest> Contests { get; set; }
        public DbSet<ContestArea> ContestAreas { get; set; }
        
        public DbSet<Region> Regions { get; set; }

        public DbSet<StudyPlace> StudyPlaces { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Institution> Institutions { get; set; }

        public DbSet<ContestRegistration> ContestRegistrations { get; set; }
        public DbSet<TeamContestRegistration> TeamContestRegistrations { get; set; }
        public DbSet<IndividualContestRegistration> IndividualContestRegistrations { get; set; }

        public DbSet<Email> Emails { get; set; }

        public DbSet<CompClass> CompClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


            builder.Entity<Institution>().HasBaseType<StudyPlace>();
            builder.Entity<School>().HasBaseType<StudyPlace>();

            builder.Entity<TeamContestRegistration>().HasBaseType<ContestRegistration>();
            builder.Entity<IndividualContestRegistration>().HasBaseType<ContestRegistration>();

           builder.Entity<ContestArea>()
                .HasAlternateKey(item => new { item.ContestId, item.AreaId });

            builder.Entity<StudyPlace>(entity =>
            {
                entity.HasOne(e => e.City)
                    .WithMany(p => p.StudyPlaces)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<City>(entity =>
            {
                entity.HasOne(e => e.Region)
                    .WithMany(p => p.Cities)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasOne(e => e.StudyPlace)
                    .WithMany(p => p.Users)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.RegistredBy)
                    .WithMany(p => p.RegistredByThisUser)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ContestRegistration>(entity =>
            {
                entity.HasOne(e => e.Contest)
                    .WithMany(p => p.ContestRegistrations)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.StudyPlace)
                    .WithMany(p => p.ContestRegistrations)
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasOne(e => e.RegistredBy)
                      .WithMany(p => p.ContestRegistrationsRegistredBy)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Participant1)
                    .WithMany(p => p.ContestRegistrationsParticipant1)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Trainer)
                    .WithMany(p => p.ContestRegistrationsTrainer)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Manager)
                    .WithMany(p => p.ContestRegistrationsManager)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ContestArea)
                    .WithMany(ca => ca.ContestRegistrations)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<TeamContestRegistration>(entity =>
            {
                entity.HasOne(e => e.Participant2)
                    .WithMany(p => p.ContestRegistrationsParticipant2)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Participant3)
                    .WithMany(p => p.ContestRegistrationsParticipant3)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ReserveParticipant)
                    .WithMany(p => p.ContestRegistrationsReserveParticipant)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Trainer2)
                    .WithMany(p => p.ContestRegistrationsTrainer2)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Trainer3)
                    .WithMany(p => p.ContestRegistrationsTrainer3)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}