using System.ComponentModel.DataAnnotations;

namespace ContestantRegister.Models
{
    public enum StudentType
    {
        [Display (Name = "Студент")]
        Student = 1,

        [Display(Name = "Магистрант")]
        Magister = 2,

        [Display(Name = "Аспирант")]
        Aspirant = 3
    }
}
