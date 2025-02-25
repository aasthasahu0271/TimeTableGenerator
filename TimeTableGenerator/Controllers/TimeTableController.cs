using Microsoft.AspNetCore.Mvc;
using TimeTableGenerator.Models;

namespace TimeTableGenerator.Controllers
{
    public class TimeTableController : Controller
    {
        private static List<SubjectAllocationModel> Subjects = new();
        private static TimeTableInputModel? timeTableInput;
        int totalAllocated;

        public IActionResult Index()
        {
            return View(new TimeTableInputModel());
        }

        /// <summary>
        /// Receives the input from the user and validates the model.
        /// If valid, it stores the input and redirects to the next action.
        /// </summary>
        /// <param name="model">The user input data for the timetable</param>
        /// <returns>Redirect to AllocateHours if valid, else returns the same page with validation errors.</returns>
        [HttpPost]
        public IActionResult SubmitInput(TimeTableInputModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            timeTableInput = model;
            return RedirectToAction("AllocateHours");
        }

        // Action to render the subject allocation page where the user can specify subject hours.
        public IActionResult AllocateHours()

        {
            if (timeTableInput == null)
                return RedirectToAction("Index"); // Prevent direct access without input data

            ViewBag.TotalHours = timeTableInput.TotalHours;
            ViewBag.TotalSubjects = timeTableInput.TotalSubjects;
            ViewBag.WorkingDays = timeTableInput.WorkingDays;
            ViewBag.SubjectsPerDay = timeTableInput.SubjectsPerDay;


            return View(new List<SubjectAllocationModel>());
        }

        // Action to process the subject allocation input from the user

        [HttpPost]
        public IActionResult SubmitAllocation(List<SubjectAllocationModel> subjectHours)
        {
            totalAllocated = subjectHours.Sum(s => s.Hours);

            if (totalAllocated != timeTableInput.TotalHours)
            {
                ModelState.AddModelError("", "Total subject hours must match total weekly hours.");
                return View("AllocateHours", subjectHours);
            }

            Subjects = subjectHours;
            return RedirectToAction("GenerateTimeTable");
        }

        // Action to generate the timetable schedule based on user input
        public IActionResult GenerateTimeTable()
        {
            var schedule = GenerateSchedule();
            return View(schedule);
        }


        /// <summary>
        /// Generates a timetable schedule based on the input and subject allocation data.
        /// </summary>
        /// <returns>A TimeTableModel containing the schedule</returns>

        private TimeTableModel GenerateSchedule()
        {
            int totalSlots = timeTableInput.WorkingDays * timeTableInput.SubjectsPerDay;
            List<string> subjectPool = new();

            foreach (var sub in Subjects)
                subjectPool.AddRange(Enumerable.Repeat(sub.SubjectName, sub.Hours));

            subjectPool = subjectPool.OrderBy(_ => new System.Random().Next()).ToList();

            List<List<string>> schedule = new();
            for (int i = 0; i < timeTableInput.SubjectsPerDay; i++)
                schedule.Add(subjectPool.Skip(i * timeTableInput.WorkingDays).Take(timeTableInput.WorkingDays).ToList());

            return new TimeTableModel
            {
                Days = Enumerable.Range(1, timeTableInput.WorkingDays).Select(d => $"Day {d}").ToList(),
                Schedule = schedule
            };
        }

    }
}
