using System.ComponentModel.DataAnnotations;

namespace ContestantRegister.Models
{
    public enum ContestParticipationType : int
    {
        [Display(Name = "Очное")]
        Intramural = 1,

        [Display(Name = "Дистанционное")]
        Remote = 2,
    }
}
