using System.ComponentModel.DataAnnotations;

namespace ContestantRegister.Models
{
    public enum ContestType : int
    {
        [Display(Name = "Личное")]
        Individual = 1,

        [Display(Name = "Командное")]
        Collegiate = 2
    }
}
