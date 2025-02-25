using System.ComponentModel.DataAnnotations;

namespace TimeTableGenerator.Models
{
    public class TimeTableInputModel
    {
        [Required, Range(1, 7, ErrorMessage = "Enter a value between 1 and 7.")]
        public int WorkingDays { get; set; }

        [Required, Range(1, 8, ErrorMessage = "Enter a value between 1 and 8.")]
        public int SubjectsPerDay { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Enter a valid subject count.")]
        public int TotalSubjects { get; set; }

        public int TotalHours => WorkingDays * SubjectsPerDay;

    }
}
