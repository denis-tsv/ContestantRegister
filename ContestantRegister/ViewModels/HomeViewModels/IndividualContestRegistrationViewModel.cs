﻿using System.ComponentModel.DataAnnotations;
using ContestantRegister.Models;
using ContestantRegister.Properties;

namespace ContestantRegister.ViewModels.HomeViewModels
{
    public class IndividualContestRegistrationViewModel
    {
        [Display(Name = "Участник")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldErrorMessage")]
        public string Participant1Id { get; set; }

        [Display(Name = "Тренер")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldErrorMessage")]
        public string TrainerId { get; set; }

        [Display(Name = "Руководитель")]
        public string ManagerId { get; set; }

        [Display(Name = "Учебное заведение")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldErrorMessage")]
        public int StudyPlaceId { get; set; }

        public string ContestName { get; set; }

        [Display(Name = "Язык программирования")]
        [MaxLength(100)]
        public string ProgrammingLanguage { get; set; }

        [Display(Name = "Город")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldErrorMessage")]
        public int CityId { get; set; }

        [Display(Name = "Площадка")]
        public string Area { get; set; }

        public ParticipantType ParticipantType { get; set; }

        public int RegistrationId { get; set; }

        public int ContestId { get; set; }
    }
}