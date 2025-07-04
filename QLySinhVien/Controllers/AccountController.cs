using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLySinhVien.Models;
using QLySinhVien.ViewModel;

namespace QLySinhVien.Controllers
{
   
    public class TeacherController : Controller
    {
        QLSinhVien db = new QLSinhVien();
        public IActionResult Index()
        {
          var teachers = db.TeacherAccs.ToList();
            return View(teachers);
        }
        public IActionResult Details(int id)
        {
            var teacher = db.TeacherAccs.FirstOrDefault(p => p.AccId == id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }
        public IActionResult Create()
        {
            ViewData["TeacherId"] = new SelectList(db.Teachers, "TeacherId", "LastName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(TeacherAcc teacherAcc)
        {
            if (ModelState.IsValid)
            {
                teacherAcc.RoleId = 1; // Assuming 1 is the role for teachers
                db.TeacherAccs.Add(teacherAcc);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["TeacherId"] = new SelectList(db.Teachers, "TeacherId", "LastName");
            return View(teacherAcc);
        }
        public IActionResult Edit(int id)
        {
            var teacher = db.TeacherAccs.FirstOrDefault(p => p.AccId == id);
            if (teacher == null)
            {
                return NotFound();
            }
            ViewData["TeacherId"] = new SelectList(db.Teachers, "TeacherId", "LastName");
            return View(teacher);
        }
        [HttpPost]
        public IActionResult Edit(TeacherAcc teacherAcc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacherAcc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["TeacherId"] = new SelectList(db.Teachers, "TeacherId", "LastName");
            return View(teacherAcc);
        }
        [HttpPost]
        public IActionResult Delete(int id) 
        {
            var teacher = db.TeacherAccs.FirstOrDefault(p => p.AccId == id);
            if (teacher == null)
            {
                return NotFound();
            }
            db.TeacherAccs.Remove(teacher);
            db.SaveChanges();
           
            return Json(new { success = true });
        }
    }

    public class StudentAccController : Controller
    {
        QLSinhVien db = new QLSinhVien();
        public IActionResult Index()
        {
            var sv=db.StudentAccs.ToList();
            return View(sv);
        }
        public IActionResult Detail(int id)
        {
            var teacher = db.StudentAccs.FirstOrDefault(p => p.AccId == id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(db.Students, "StudentId", "LastName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(StudentAcc sv)
        {
            if (ModelState.IsValid)
            {
                sv.RoleId = 2; // Assuming 2 is the role for students
                db.StudentAccs.Add(sv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["StudentId"] = new SelectList(db.Students, "StudentId", "LastName");
            return View(sv);
        }
        public IActionResult Edit(int id)
        {
            var sv = db.StudentAccs.FirstOrDefault(p => p.AccId == id);
            if (sv == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(db.Students, "StudentId", "LastName");
            return View(sv);
        }
        [HttpPost]
        public IActionResult Edit(StudentAcc sv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["StudentId"] = new SelectList(db.Students, "StudentId", "LastName");
            return View(sv);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var sv = db.StudentAccs.FirstOrDefault(p => p.AccId == id);
            if (sv == null)
            {
                return NotFound();
            }
            db.StudentAccs.Remove(sv);
            db.SaveChanges();

            return Json(new { success = true });
        }
    }
    public class AccountController : Controller
    {
        QLSinhVien db = new QLSinhVien();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
       public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if(ModelState.IsValid){
                var user=db.TeacherAccs.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                

                if (user != null && user.RoleId==1)
                {
                    // Logic to set user session or cookie
                    HttpContext.Session.SetString("Email", user.Email);
                    HttpContext.Session.SetString("Password", user.Password);
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.ErrorMessage = "Sai mật khẩu hoặc tài khoản";
            }
            return View(model);
        }
        public IActionResult Logout()
        {
            // Logic to log out the user
            HttpContext.Session.Clear(); // Clear session data
            return RedirectToAction("Login");
        }
    }
}