﻿using System.ComponentModel.DataAnnotations;
using ContestantRegister.Domain.Properties;

namespace ContestantRegister.Cqrs.Features.Frontend.Contests.Common.ViewModels
{
    public class ImportParticipantsViewModel
    {
        public string ContestName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldErrorMessage")]
        [Display(Name = "Csv данные")]
        public string Data { get; set; }

        [Display(Name = "Разделитель - табуляция")]
        public bool TabDelimeter { get; set; } = true;
    }
}
