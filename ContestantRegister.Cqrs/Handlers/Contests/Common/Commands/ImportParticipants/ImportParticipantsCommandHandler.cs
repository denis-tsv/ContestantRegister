using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContestantRegister.Cqrs.Features._Common.CommandHandlers;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Commands;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Utils;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Exceptions;
using ContestantRegister.Services.Extensions;
using CsvHelper;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.CommandHandlers
{
    internal class ImportParticipantsCommandHandler : RepositoryCommandBaseHandler<ImportParticipantsCommand>
    {
        private readonly IMapper _mapper;

        public ImportParticipantsCommandHandler(IRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public override async Task HandleAsync(ImportParticipantsCommand command)
        {
            var contest = await Repository.Set<Contest>()
                .Include($"{nameof(Contest.ContestAreas)}.{nameof(ContestArea.Area)}")
                .SingleOrDefaultAsync(c => c.Id == command.ContestId);
            if (contest == null) throw new EntityNotFoundException();
            
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
                var dto = csv.GetRecord<ContestRegistrationDto>();
                if (string.IsNullOrEmpty(dto.YaContestLogin)) continue;
                var registration = await Repository.Set<ContestRegistration>().SingleOrDefaultAsync
                    (r => r.ContestId == command.ContestId && r.YaContestLogin == dto.YaContestLogin);

                if (registration == null) continue;

                _mapper.Map(dto, registration);

                if (dto.Number.HasValue)
                {
                    registration.Number = dto.Number.Value;
                }

                if (!string.IsNullOrEmpty(dto.Status))
                {
                    if (Enum.TryParse<ContestRegistrationStatus>(dto.Status, out var status))
                    {
                        registration.Status = status;
                    }
                }

                if (!string.IsNullOrEmpty(dto.Area))
                {
                    var contestArea = contest.ContestAreas.FirstOrDefault(ca => ca.Area.Name == dto.Area);
                    if (contestArea != null)
                    {
                        registration.ContestAreaId = contestArea.Id;
                    }
                }
            }

            await Repository.SaveChangesAsync();
        }
    }
}
