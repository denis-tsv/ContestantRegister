﻿using System;
using System.ComponentModel.DataAnnotations;
using ContestantRegister.Domain.Properties;
using ContestantRegister.Models;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels
{
    public abstract class ContestRegistrationViewModel
    {
        [Display(Name = "Участник")]
        public virtual string Participant1Id { get; set; }

        [Display(Name = "Тренер")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldErrorMessage")]
        public string TrainerId { get; set; }

        [Display(Name = "Руководитель")]
        public string ManagerId { get; set; }

        [Display(Name = "Учебное заведение")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldErrorMessage")]
        public int StudyPlaceId { get; set; }

        public bool IsProgrammingLanguageNeeded { get; set; }

        [Display(Name = "Язык программирования")]
        [MaxLength(100)]
        public string ProgrammingLanguage { get; set; }

        [Display(Name = "Город")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldErrorMessage")]
        public int CityId { get; set; }

        public bool IsAreaRequired { get; set; }

        [Display(Name = "Площадка")]
        public int? ContestAreaId { get; set; }

        [Display(Name = "Рабочее место")]
        [MaxLength(50)]
        public string ComputerName { get; set; }

        [Display(Name = "Логин в ЯКонтесте")]
        public string YaContestLogin { get; set; }

        [Display(Name = "Пароль в ЯКонтесте")]
        public string YaContestPassword { get; set; }

        [Display(Name = "Дата регистрации")]
        public DateTime? RegistrationDateTime { get; set; }

        [Display(Name = "Кем зарегистрирован")]
        public string RegistredByName { get; set; }

        [Display(Name = "№")]
        [Range(1, int.MaxValue)]
        public int? Number { get; set; }

        [Display(Name = "Статус регистрации")]
        public ContestRegistrationStatus Status { get; set; }        

        public ParticipantType ParticipantType { get; set; }

        [Display(Name = "Вне конкурса")]
        public bool IsOutOfCompetition { get; set; }

        public bool IsOutOfCompetitionAllowed { get; set; }

        [Display(Name = "Английский язык")]
        public bool IsEnglishLanguage { get; set; }

        public int RegistrationId { get; set; }

        public string ContestName { get; set; }

        public int ContestId { get; set; }

        public bool ShowRegistrationInfo { get; set; }

        public int ContestTrainerCont { get; set; }

        public virtual ContestRegistrationStatus CheckRegistrationStatus()
        {
            return string.IsNullOrEmpty(Participant1Id) ? 
                ContestRegistrationStatus.NotCompleted : 
                ContestRegistrationStatus.Completed;
        }
    }
}
