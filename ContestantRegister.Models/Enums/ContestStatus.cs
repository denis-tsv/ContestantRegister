using System.ComponentModel.DataAnnotations;

namespace ContestantRegister.Models
{
    public enum ContestStatus : int
    {
        [Display(Name = "Регистрация закрыта")]
        RegistrationClosed = 1,

        [Display(Name = "Регистрация открыта")]
        RegistrationOpened = 2,

        [Display(Name = "Подтверждение участия")]
        ConfirmParticipation = 3,

        [Display(Name = "Регистрация и подтверждние участия")]
        ConfirmParticipationOrRegister = 4,

        [Display(Name = "Завершен")]
        Finished = 5
    }
}
