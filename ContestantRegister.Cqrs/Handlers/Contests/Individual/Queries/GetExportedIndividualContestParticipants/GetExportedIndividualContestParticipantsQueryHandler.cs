using System.Linq;
using System.Threading.Tasks;
using ContestantRegister.Cqrs.Features._Common.QueryHandlers;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Individual.Queries;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Exceptions;
using ContestantRegister.Services.Extensions;
using OfficeOpenXml;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Individual.QueryHandlers
{
    internal class GetExportedIndividualContestParticipantsQueryHandler : ReadRepositoryQueryHandler<GetExportedIndividualContestParticipantsQuery, ExportIndividualContestParticipantsResult>
    {
        public GetExportedIndividualContestParticipantsQueryHandler(IReadRepository repository) : base(repository)
        {
        }

        public override async Task<ExportIndividualContestParticipantsResult> HandleAsync(GetExportedIndividualContestParticipantsQuery query)
        {
            var contestName = await ReadRepository.Set<Contest>()
                .Where(x => x.Id == query.ContestId)
                .Select(x => x.Name)
                .SingleOrDefaultAsync();

            if (contestName == null) throw new EntityNotFoundException();

            var registrations = await ReadRepository.Set<IndividualContestRegistration>()
                .Include(r => r.Contest)
                .Include(r => r.StudyPlace.City.Region)
                .Include(r => r.Participant1)
                .Include(r => r.Trainer)
                .Include(r => r.Manager)
                .Include(r => r.ContestArea.Area)
                .Where(r => r.ContestId == query.ContestId)
                .OrderBy(r => r.Number)
                .ToListAsync();

            var package = new ExcelPackage();

            var worksheet = package.Workbook.Worksheets.Add("Participants");
            worksheet.Cells["A1"].Value = "Email";
            worksheet.Cells["B1"].Value = "Surname";
            worksheet.Cells["C1"].Value = "Name";
            worksheet.Cells["D1"].Value = "Patronymic";
            worksheet.Cells["E1"].Value = "TrainerEmail";
            worksheet.Cells["F1"].Value = "TrainerSurname";
            worksheet.Cells["G1"].Value = "TrainerName";
            worksheet.Cells["H1"].Value = "TrainerPatronymic";
            worksheet.Cells["I1"].Value = "ManagerEmail";
            worksheet.Cells["J1"].Value = "ManagerSurname";
            worksheet.Cells["K1"].Value = "ManagerName";
            worksheet.Cells["L1"].Value = "ManagerPatronymic";
            worksheet.Cells["M1"].Value = "Region";
            worksheet.Cells["N1"].Value = "City";
            worksheet.Cells["O1"].Value = "StudyPlace";
            worksheet.Cells["P1"].Value = "Status";
            worksheet.Cells["Q1"].Value = "YaContestLogin";
            worksheet.Cells["R1"].Value = "YaContestPassword";
            worksheet.Cells["S1"].Value = "Area";
            worksheet.Cells["T1"].Value = "Number";
            worksheet.Cells["U1"].Value = "ComputerName";
            worksheet.Cells["V1"].Value = "ProgrammingLanguage";
            worksheet.Cells["W1"].Value = "DateOfBirth";
            worksheet.Cells["X1"].Value = "Class";
            worksheet.Cells["Y1"].Value = "Course";
            worksheet.Cells["Z1"].Value = "StudentType";
            worksheet.Cells["AA1"].Value = "StudyPlace_FullName";

            int row = 1;
            foreach (var registration in registrations)
            {
                row++;

                worksheet.Cells[row, 1].Value = registration.Participant1.Email;
                worksheet.Cells[row, 2].Value = registration.Participant1.Surname;
                worksheet.Cells[row, 3].Value = registration.Participant1.Name;
                worksheet.Cells[row, 4].Value = registration.Participant1.Patronymic;
                worksheet.Cells[row, 5].Value = registration.Trainer.Email;
                worksheet.Cells[row, 6].Value = registration.Trainer.Surname;
                worksheet.Cells[row, 7].Value = registration.Trainer.Name;
                worksheet.Cells[row, 8].Value = registration.Trainer.Patronymic;

                if (registration.Manager != null)
                {
                    worksheet.Cells[row, 9].Value = registration.Manager.Email;
                    worksheet.Cells[row, 10].Value = registration.Manager.Surname;
                    worksheet.Cells[row, 11].Value = registration.Manager.Name;
                    worksheet.Cells[row, 12].Value = registration.Manager.Patronymic;
                }

                worksheet.Cells[row, 13].Value = registration.StudyPlace.City.Region.Name;
                worksheet.Cells[row, 14].Value = registration.StudyPlace.City.Name;
                worksheet.Cells[row, 15].Value = registration.StudyPlace.ShortName;
                worksheet.Cells[row, 16].Value = registration.Status;
                worksheet.Cells[row, 17].Value = registration.YaContestLogin;
                worksheet.Cells[row, 18].Value = registration.YaContestPassword;
                worksheet.Cells[row, 19].Value = registration.ContestArea?.Area.Name;
                worksheet.Cells[row, 20].Value = registration.Number;
                worksheet.Cells[row, 21].Value = registration.ComputerName;
                worksheet.Cells[row, 22].Value = registration.ProgrammingLanguage;
                worksheet.Cells[row, 23].Value = registration.Participant1.DateOfBirth;
                worksheet.Cells[row, 24].Value = registration.Class;
                worksheet.Cells[row, 25].Value = registration.Course;
                worksheet.Cells[row, 26].Value = registration.StudentType;
                worksheet.Cells[row, 27].Value = registration.StudyPlace.FullName;

            }

            return new ExportIndividualContestParticipantsResult
            {
                ExcelPackage = package,
                ContestName = contestName
            };
        }
    }
}
