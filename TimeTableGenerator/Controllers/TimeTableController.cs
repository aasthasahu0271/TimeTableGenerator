using Microsoft.AspNetCore.Mvc;
using TimeTableGenerator.Models;

namespace TimeTableGenerator.Controllers
{
    public class TimeTableController : Controller
    {
        private static List<SubjectAllocationModel> Subjects = new();
        private static TimeTableInputModel TimeTableInput;
        int totalAllocated;

        public IActionResult Index()
        {
            return View(new TimeTableInputModel());
        }

        [HttpPost]
        public IActionResult SubmitInput(TimeTableInputModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            TimeTableInput = model;
            return RedirectToAction("AllocateHours");
        }

        //public IActionResult AllocateHours()
        //{
        //    ViewBag.TotalHours = TimeTableInput.TotalHours;
        //    ViewBag.TotalSubjects = TimeTableInput.TotalSubjects;
        //    return View(new List<SubjectAllocationModel>());
        //}

        public IActionResult AllocateHours()
        
        {
            if (TimeTableInput == null)
                return RedirectToAction("Index"); // Prevent direct access without input data

            ViewBag.TotalHours = TimeTableInput.TotalHours;
            ViewBag.TotalSubjects = TimeTableInput.TotalSubjects;
            ViewBag.WorkingDays = TimeTableInput.WorkingDays;
            ViewBag.SubjectsPerDay = TimeTableInput.SubjectsPerDay;


            return View(new List<SubjectAllocationModel>());
        }

        [HttpPost]

        
        public IActionResult SubmitAllocation(List<SubjectAllocationModel> subjectHours)
        {
            //int totalAllocated = subjectHours.Sum(s => s.Hours);
             totalAllocated = subjectHours.Sum(s => s.Hours);

            if (totalAllocated != TimeTableInput.TotalHours)
            {
                ModelState.AddModelError("", "Total subject hours must match total weekly hours.");
                return View("AllocateHours", subjectHours);
            }

            Subjects = subjectHours;
            return RedirectToAction("GenerateTimeTable");
        }

        public IActionResult GenerateTimeTable()
        {
            var schedule = GenerateSchedule();
            return View(schedule);
        }

        private TimeTableModel GenerateSchedule()
        {
            int totalSlots = TimeTableInput.WorkingDays * TimeTableInput.SubjectsPerDay;
            List<string> subjectPool = new();

            foreach (var sub in Subjects)
                subjectPool.AddRange(Enumerable.Repeat(sub.SubjectName, sub.Hours));

            subjectPool = subjectPool.OrderBy(_ => new System.Random().Next()).ToList();

            List<List<string>> schedule = new();
            for (int i = 0; i < TimeTableInput.SubjectsPerDay; i++)
                schedule.Add(subjectPool.Skip(i * TimeTableInput.WorkingDays).Take(TimeTableInput.WorkingDays).ToList());

            return new TimeTableModel
            {
                Days = Enumerable.Range(1, TimeTableInput.WorkingDays).Select(d => $"Day {d}").ToList(),
                Schedule = schedule
            };
        }

    }
}
