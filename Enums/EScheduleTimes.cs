using System.ComponentModel.DataAnnotations;

namespace TopMovie_SemesterarbeitSSE.Enums
{
    public enum EScheduleTimes
    {
        [Display(Name = "14:15")]
        Time1415,

        [Display(Name = "17:15")]
        Time1715,

        [Display(Name = "20:15")]
        Time2015,

        [Display(Name = "23:00")]
        Time2300
    }
}
