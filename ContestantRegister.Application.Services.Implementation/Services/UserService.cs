using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;
using ContestantRegister.Services.Extensions;

namespace ContestantRegister.Services.DomainServices
{

    internal class UserService : IUserService
    {
        private readonly IReadRepository _readRepository;

        public UserService(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        //TODO можно распилить этот метод на три для валидации каждого типа пользователя: студента, школьника и тренера
        public async Task<List<KeyValuePair<string, string>>> ValidateUserAsync(IApplicationUser viewModel)
        {
            var result = new List<KeyValuePair<string, string>>();

            var studyPlace = await _readRepository.Set<StudyPlace>().SingleAsync(s => s.Id == viewModel.StudyPlaceId);
            switch (viewModel.UserType)
            {
                case UserType.Pupil:
                    if (studyPlace is Institution)
                        result.Add(KeyValuePair.Create(nameof(viewModel.StudyPlaceId), "У школьника не может быть указан вуз в качестве учебного заведения"));
                    if (!viewModel.EducationStartDate.HasValue)
                        result.Add(KeyValuePair.Create(nameof(viewModel.EducationStartDate), viewModel.GetRequredFieldErrorMessage(nameof(viewModel.EducationStartDate))));
                    break;
                case UserType.Student:
                    if (studyPlace is School)
                        result.Add(KeyValuePair.Create(nameof(viewModel.StudyPlaceId), "У студента не может быть указана школа в качестве учебного заведения"));
                    if (!viewModel.EducationStartDate.HasValue)
                        result.Add(KeyValuePair.Create(nameof(viewModel.EducationStartDate), viewModel.GetRequredFieldErrorMessage(nameof(viewModel.EducationStartDate))));
                    if (!viewModel.EducationEndDate.HasValue)
                        result.Add(KeyValuePair.Create(nameof(viewModel.EducationEndDate), viewModel.GetRequredFieldErrorMessage(nameof(viewModel.EducationEndDate))));
                    if (viewModel.EducationStartDate.HasValue && viewModel.EducationEndDate.HasValue && viewModel.EducationStartDate > viewModel.EducationEndDate)
                        result.Add(KeyValuePair.Create(string.Empty, "Дата начала обучения должна быть позже даты конца обучения"));
                    if (viewModel.DateOfBirth.HasValue && DateTime.Now.Year - viewModel.DateOfBirth.Value.Year < 16)
                        result.Add(KeyValuePair.Create(nameof(viewModel.DateOfBirth), "Возраст слишком маленький, чтобы быть студентом"));
                    break;
            }

            if (viewModel.UserType == UserType.Student || viewModel.UserType == UserType.Trainer)
            {
                if (string.IsNullOrEmpty(viewModel.FirstName))
                    result.Add(KeyValuePair.Create(nameof(viewModel.FirstName), viewModel.GetRequredFieldErrorMessage(nameof(viewModel.FirstName))));
                if (string.IsNullOrEmpty(viewModel.LastName))
                    result.Add(KeyValuePair.Create(nameof(viewModel.LastName), viewModel.GetRequredFieldErrorMessage(nameof(viewModel.LastName))));
                if (string.IsNullOrEmpty(viewModel.PhoneNumber))
                    result.Add(KeyValuePair.Create(nameof(viewModel.PhoneNumber), viewModel.GetRequredFieldErrorMessage(nameof(viewModel.PhoneNumber))));
            }
            
            return result;
        }
    }
}
