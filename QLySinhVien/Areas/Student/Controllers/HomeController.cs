using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QLySinhVien.Areas.Student.Models;
using QLySinhVien.Models;
using QLySinhVien.ViewModel;


namespace QLySinhVien.Areas.Student.Controllers
{
    [Area("Student")]
    [StudenAuthentication] // Assuming this is a custom attribute for student authentication
    public class HomeController : Controller
    {
        private readonly QLSinhVien db;
        public HomeController(QLSinhVien context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            string email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "Student" });
            }
            var student = db.Students.Where(s => s.Email == email)
                .FirstOrDefault();
            return View(student);
        }
        public IActionResult XemDiem()
        {
            string email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "Student" });
            }
            var data = from s in db.Scores
                       join t in db.Tests on s.TestId equals t.TestId
                       join c in db.Courses on t.CourseId equals c.CourseId
                       where s.Student.Email == email
                       select new {
                           c.Title,
                           t.TestName,
                           t.Weight,
                           s.Marks
                       };
            var grouped = data.GroupBy(x => x.Title)
                .Select(g => new CourseScoreVM
                {
                    CourseTitle = g.Key,
                    TestScores = g.Select(ts => new TestScoreVM
                    {
                        TestName = ts.TestName,
                        Weight = (double)ts.Weight,
                        Score = (double)ts.Marks
                    }).ToList()
                }).ToList();
            return View(grouped);
        }
        [HttpGet]
        public IActionResult DangKyKhoaHoc()
        {
            var email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "Student" });
            }
            var allCourses = db.Courses.ToList();
            var registeredCourses = db.Enrollments
                .Where(e => e.Student.Email == email)
                .Select(e => e.CourseId)
                .ToList();
            var model = allCourses.Select(c => new RegisterCourse
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Credits = c.Credits,
                IsRegistered = registeredCourses.Contains(c.CourseId)
            }).ToList();
            return View(model);


        }
        [HttpPost]
        public IActionResult DangKyKhoaHoc(int courseId)
        {
            string email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "Student" });
            }
            bool isRegistered = db.Enrollments.Any(e => e.Student.Email == email && e.CourseId == courseId);
            if (!isRegistered)
            {
                var studentId = db.Students
                    .Where(s => s.Email == email).Select(s => s.StudentId)
                    .FirstOrDefault();
                var enrollment = new Enrollment
                {
                    CourseId = courseId,
                    StudentId = studentId,
                    EnrollmentDate = DateOnly.FromDateTime(DateTime.Now)
                };
                db.Add(enrollment);
                db.SaveChanges();


            }
            return RedirectToAction("DangKyKhoaHoc");
        }
        [HttpPost]
        public IActionResult HuyDangKy(int courseId)
        {
            string email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "Student" });
            }
            var enrollment = db.Enrollments
                .FirstOrDefault(e => e.Student.Email == email && e.CourseId == courseId);
            if (enrollment != null)
            {
                db.Enrollments.Remove(enrollment);
                db.SaveChanges();
            }
            return RedirectToAction("DangKyKhoaHoc");

        }
        [HttpGet]
        public IActionResult DanhSachLopHoc()
        {
            var classes = db.Classes
                .Include(c => c.Course)
                .Include(c => c.Teacher)
                .Select(c => new LopHocVM
                {
                    ClassId=c.ClassId,
                    NameClass=c.NameClass,
                    CourseTitle=c.Course.Title,
                    TeacherName=c.Teacher.FirstName + " "+c.Teacher.LastName,
                    ClassDate=c.ClassDate
                }).ToList();
            return View(classes);
        }
    }
}
