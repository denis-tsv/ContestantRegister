using System.Linq;
using ContestantRegister.DataAccess;
using ContestantRegister.Models;
using ContestantRegister.Services.InfrastructureServices;
using FluentScheduler;

namespace ContestantRegister.BackgroundJobs
{
    public class ContestStatusJob : IJob
    {
        private readonly ApplicationDbContext _context;

        public ContestStatusJob(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            var contests = _context.Contests.Where(c => c.RegistrationEnd < DateTimeService.SfuServerNow &&
                                                        (c.ContestStatus == ContestStatus.RegistrationOpened ||
                                                         c.ContestStatus == ContestStatus.ConfirmParticipation ||
                                                         c.ContestStatus == ContestStatus.ConfirmParticipationOrRegister));
            foreach (var contest in contests)
            {
                contest.ContestStatus = ContestStatus.RegistrationClosed;
            }

            _context.SaveChanges();
        }
    }
}
