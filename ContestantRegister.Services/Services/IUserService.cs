using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContestantRegister.Models;

namespace ContestantRegister.Services.DomainServices
{
    public interface IUserService
    {
        Task<List<KeyValuePair<string, string>>> ValidateUserAsync(IApplicationUser viewModel);
    }

    public interface IApplicationUser
    {
        string Email { get; set; }

        bool EmailConfirmed { get; set; }

        string BaylorEmail { get; set; }

        UserType UserType { get; set; }

        string Name { get; set; }

        string Surname { get; set; }

        string Patronymic { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        int CityId { get; set; }

        int StudyPlaceId { get; set; }

        DateTime? DateOfBirth { get; set; }

        DateTime? EducationStartDate { get; set; }

        //Student
        DateTime? EducationEndDate { get; set; }

        string VkProfile { get; set; }

        bool IsBaylorRegistrationCompleted { get; set; }

        // для студента обязательное поле
        string PhoneNumber { get; set; }

        //обязательное для студента 
        StudentType? StudentType { get; set; }

        //Trainer

        bool IsUserTypeDisabled { get; set; }

        bool CanSuggestStudyPlace { get; set; }
    }
    
}
