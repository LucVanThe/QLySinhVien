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
    public class EnrollmentsController : Controller
    {
        private readonly QLSinhVien _context;

        public EnrollmentsController(QLSinhVien context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index(int MaMonHoc)
        {
            var sv = _context.Enrollments.Where(s => s.CourseId == MaMonHoc).OrderBy(s => s.Student.LastName)
                .Select(s => new SVdangkyVM
                {
                    MaDangky = s.EnrollmentId,
                    MaSV = s.StudentId ?? 0,
                    TenMonHoc = s.Course.Title,
                    HoTen = s.Student.FirstName + " " + s.Student.LastName,
                    NgayDangKy = s.EnrollmentDate
                })
                .ToList();
            ViewBag.MaMonHoc = MaMonHoc;
        
            return View(sv);
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }
        public IActionResult DangKyMonHoc()
        {
            var enrollment =_context.Courses               
                .OrderBy(s=>s.Title)
                .Select(e=> new DangKyMonHocVm
                    {
                     MaMonHoc=e.CourseId,
                    TenMonHoc = e.Title,
                    SoTinChi = (int)e.Credits
                }).ToList();
           return View(enrollment);
        }
       

        // GET: Enrollments/Create
        public IActionResult Create(int MaMonHoc)
        {
            
                var existing = _context.Enrollments
             .Where(s => s. CourseId== MaMonHoc)
             .Select(s => s.StudentId)
             .ToList();
            var students = _context.Students
                .Where(s => !existing.Contains(s.StudentId))
                .Select(s => new ThemSVdangkyVM
                {

                    MaMonHoc = MaMonHoc,
                    TenMonHoc = _context.Courses.Where(c => c.CourseId == MaMonHoc).Select(c => c.Title).FirstOrDefault(),
                    MaSV = s.StudentId,
                    HoTen = s.FirstName + " " + s.LastName
                    
                })
                .ToList();
            ViewBag.CourseId = MaMonHoc;
           
            return View(students);
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int studentId, int courseId)
        {
            var exitsting = _context.Enrollments
                .Any(s => s.StudentId == studentId && s.CourseId == courseId);
            if (!exitsting)
            {
                var enrollment = new Enrollment
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    EnrollmentDate =DateOnly.FromDateTime( DateTime.Now)
                };
                _context.Enrollments.Add(enrollment);
                 _context.SaveChanges();
                
            }
            return RedirectToAction("Create", new { MaMonHoc = courseId });



        }

       
        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentId == id);
        }
    }
}
