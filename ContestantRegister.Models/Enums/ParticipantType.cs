using System.ComponentModel.DataAnnotations;

namespace ContestantRegister.Models
{
    public enum ParticipantType : int
    {
        [Display(Name = "Школьники")]
        Pupil = 1,

        [Display(Name = "Студенты")]
        Student = 2
    }
}
