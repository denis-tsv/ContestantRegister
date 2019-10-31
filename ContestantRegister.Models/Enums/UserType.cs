using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ContestantRegister.Models
{
    public enum UserType : int
    {
        [Display(Name = "Школьник")]
        Pupil = 1,

        [Display(Name = "Студент")]
        Student = 2,

        [Display(Name = "Тренер")]
        Trainer = 3,
    }

    public static class UserTypeHelper
    {
        public static string GetDisplayName(this UserType userType)
        {
            var fieldInfo = userType.GetType().GetField(userType.ToString());
            var displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();
            return displayAttribute.Name;
        }
    }
}
