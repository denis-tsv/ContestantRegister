﻿using FluentScheduler;

namespace ContestantRegister.BackgroundJobs
{
    public class FluentSchedulerRegistry : Registry
    {
        public FluentSchedulerRegistry()
        {
            //Schedule<EmailJob>().NonReentrant().ToRunEvery(15).Seconds();

            //Schedule<ContestStatusJob>().NonReentrant().ToRunEvery(5).Minutes();
        }
    }
}
