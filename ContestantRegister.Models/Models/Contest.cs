using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoFilter;
using ContestantRegister.Domain.Properties;

namespace ContestantRegister.Models
{
    [DisplayName("Контест")]
    public class Contest : DomainObject
    {
        public static readonly Spec<Contest> Archived = new Spec<Contest>(x => x.IsArchive);

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldErrorMessage")]
        [MaxLength(200)]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [MaxLength(5000)]
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Регистрация до")]
        public DateTime RegistrationEnd { get; set; }

        [Display(Name = "Старт")]
        public DateTime Start { get; set; }

        [Display(Name = "Часов")]
        [Range(1, int.MaxValue)]
        public int Duration { get; set; }

        [Display(Name = "Участники")]
        public ParticipantType ParticipantType { get; set; }

        [Display(Name = "Первенство")]
        public ContestType ContestType { get; set; }

        [Display(Name = "Архивный")]
        public bool IsArchive { get; set; }

        [Display(Name = "Статус")]
        public ContestStatus ContestStatus { get; set; }

        [Display(Name = "Участие")]
        public ContestParticipationType ContestParticipationType { get; set; }

        [Display(Name = "Английский язык")]
        public bool IsEnglishLanguage { get; set; }

        [Display(Name = "Указывать язык программирования при регистрации")]
        public bool IsProgrammingLanguageNeeded { get; set; }

        [Display(Name = "Допускать внеконкурсных участников")]
        public bool IsOutOfCompetitionAllowed { get; set; }

        [MaxLength(100)]
        [Display(Name = "Ссылка на контест на ЯКонтесте")]
        [DataType(DataType.Url, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "InvalidUrlErrorMessage")]
        public string YaContestLink { get; set; }

        [Display(Name = "Отправлять email с регистрационными данными после завершения регистрации")]
        public bool SendRegistrationEmail { get; set; }

        [Display(Name = "Показывать на сайте логин/пароль от ЯКонтест")]
        public bool ShowRegistrationInfo { get; set; }

        [Display(Name = "Показывать статус регистрации на бейлоре")]
        public bool ShowBaylorRegistrationStatus { get; set; }

        [Display(Name = "CSV с яконтест аккаунтами")]
        [DataType(DataType.MultilineText)]
        public string YaContestAccountsCSV { get; set; }

        [Display(Name = "Использовано яконтест аккаунтов")]
        [Range(0, int.MaxValue)]
        public int UsedAccountsCount { get; set; } = 0;

        [Display(Name = "Площадки")]
        public List<ContestArea> ContestAreas { get; set; } = new List<ContestArea>();

        [Display(Name = "Указывать ли площадку при регистрации")]
        public bool IsAreaRequired { get; set; }

        public ICollection<ContestRegistration> ContestRegistrations { get; set; }

        [Display(Name = "Зарегистрировано")]
        [Range(0, int.MaxValue)]
        public int RegistrationsCount { get; set; }

        [Display(Name = "Количество тренеров")]
        [Range(1, 3)]
        public int TrainerCount { get; set; }
        
        [Display(Name = "Показывать решетку перед номером команды")]
        public bool ShowSharpTeamNumber { get; set; }
    }
}
