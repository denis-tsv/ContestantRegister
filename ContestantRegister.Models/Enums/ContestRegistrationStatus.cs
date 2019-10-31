using System.ComponentModel.DataAnnotations;

namespace ContestantRegister.Models
{
    public enum ContestRegistrationStatus : int
    {
        [Display(Name = "Завершена")]
        Completed = 1,

        [Display(Name = "Не завершена")]
        NotCompleted = 2,
    }
}
