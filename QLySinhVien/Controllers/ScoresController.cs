using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLySinhVien.Models;
using QLySinhVien.ViewModel;

namespace QLySinhVien.Controllers
{
    public class ScoresController : Controller
    {
        private readonly QLSinhVien _context;

        public ScoresController(QLSinhVien context)
        {
            _context = context;
        }

        // GET: Scores
        public async Task<IActionResult> Index(int? CourseId = 0)
        {
            var courses = _context.Courses.ToList();           
            var test = _context.Tests.Include(t => t.Course).Where(s=>s.CourseId==CourseId|| CourseId==0).ToList();
            ViewBag.CourseList = new SelectList(courses, "CourseId", "Title");
            ViewBag.SelectedCourse = CourseId;
            return View(test);
        }
        public IActionResult StudentScores(int testId)
        {
            var score = _context.Scores
                 .Include(s => s.Student)
                .Include(s => s.Test)
               .ThenInclude(t => t.Course)
                .Where(s => s.TestId == testId)
                .Select(s => new ScoresVM
                {
                    ScoreID = s.ScoreId,
                    StudentID = s.StudentId ?? 0,
                    StudentName = s.Student.FirstName + " " + s.Student.LastName,
                    TestID = s.TestId ?? 0,
                    TestName = s.Test.TestName,
                    CourseTitle = s.Test.Course.Title,
                    Marks = (double)(s.Marks ?? 0)

                }).ToList();
            ViewBag.TestID = testId;
            return View(score);
        }

        // GET: Scores/Create
        [HttpGet]
        public IActionResult Create(int testId)
        {
            var test = _context.Tests.Include(t => t.Course).FirstOrDefault(t => t.TestId == testId);
            if (test == null)
            {
                return NotFound();
            }
                var existingStudentIds = _context.Scores
             .Where(s => s.TestId == testId)
             .Select(s => s.StudentId)
             .ToList();
                 var enrollment = _context.Enrollments
                .Include(e => e.Student)
                .Where(e => e.CourseId == test.CourseId && !existingStudentIds.Contains(e.StudentId ?? 0))
                .Select(e => new StudentScoreInputVM
                {
                    StudentId = e.StudentId ?? 0,
                    StudentName = e.Student.FirstName + " " + e.Student.LastName,
                    Mark = 0 
                })
                .ToList();
            var viewModel = new AddScoresVM
            {
                CourseID = test.CourseId,
                CourseTitle = test.Course.Title,
                TestID = test.TestId,
                TestName = test.TestName,
                StudentScores = enrollment
            };
            return View(viewModel);
        }

        // POST: Scores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]     
        public async Task<IActionResult> Create(AddScoresVM model)
        {
            foreach (var s in model.StudentScores)
            {
                var existing = _context.Scores
             .FirstOrDefault(x => x.StudentId == s.StudentId && x.TestId == model.TestID);

                if (existing == null)
                {
                    var score = new Score
                    {
                        StudentId = s.StudentId,
                        TestId = model.TestID,
                        Marks = s.Mark > 0 ? (decimal?)s.Mark : null 
                    };
                    _context.Scores.Add(score);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("StudentScores", new { testId =model.TestID});
        }

        // GET: Scores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var score =  _context.Scores
                .Include(s => s.Student)
                .Include(s => s.Test)
                .FirstOrDefaultAsync(m => m.ScoreId == id);
            if (score == null) return NotFound();
            var vm = new EditScoreVM
            {
                ScoreID = score.Result.ScoreId,
                StudentID = score.Result.StudentId ?? 0,
                StudentName = score.Result.Student.FirstName + " " + score.Result.Student.LastName,
                TestID = score.Result.TestId ?? 0,
                TestName = score.Result.Test.TestName,
                Marks = (double)(score.Result.Marks ?? 0)
            };
            return View(vm);
        }

        // POST: Scores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public async Task<IActionResult> Edit( EditScoreVM model)
        {
            
            var score =  _context.Scores.FirstOrDefault(s=>s.ScoreId==model.ScoreID);
            if(score ==null)return NotFound();
            score.Marks = (decimal?)model.Marks;
           
            _context.SaveChanges();
            return RedirectToAction("StudentScores", new { testId = model.TestID });
        }

        // GET: Scores/Delete/5
       

        // POST: Scores/Delete/5
        [HttpPost, ActionName("Delete")]     
        public async Task<IActionResult> Delete(int id)
        {
            var score = await _context.Scores.FindAsync(id);
            if (score != null)
            {
                _context.Scores.Remove(score);
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        private bool ScoreExists(int id)
        {
            return _context.Scores.Any(e => e.ScoreId == id);
        }
    }
}
