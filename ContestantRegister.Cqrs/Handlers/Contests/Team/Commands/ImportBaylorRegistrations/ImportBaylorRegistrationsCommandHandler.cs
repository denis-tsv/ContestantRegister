using System.IO;
using System.Threading.Tasks;
using ContestantRegister.Cqrs.Features._Common.CommandHandlers;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Team.Commands;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using CsvHelper;
using Microsoft.AspNetCore.Identity;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Team.CommandHandlers
{
    internal class ImportBaylorRegistrationsCommandHandler : RepositoryCommandBaseHandler<ImportBaylorRegistrationsCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ImportBaylorRegistrationsCommandHandler(IRepository repository, UserManager<ApplicationUser> userManager) : base(repository)
        {
            _userManager = userManager;
        }

        public override async Task HandleAsync(ImportBaylorRegistrationsCommand command)
        {
            var sr = new StringReader(command.ViewModel.Data);
            var csv = new CsvReader(sr);
            csv.Configuration.MissingFieldFound = null;
            if (command.ViewModel.TabDelimeter)
            {
                csv.Configuration.Delimiter = "\t";
            }
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                var email = csv.GetField("Username");
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) continue;

                var completed = csv.GetField("Registration complete"); //yes or no
                user.IsBaylorRegistrationCompleted = (completed == "yes");
            }

            await Repository.SaveChangesAsync();
        }
    }
}
