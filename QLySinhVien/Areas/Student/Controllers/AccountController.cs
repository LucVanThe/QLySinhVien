using Microsoft.AspNetCore.Mvc;
using QLySinhVien.Models;
using QLySinhVien.ViewModel;

namespace QLySinhVien.Areas.Student.Controllers
{
    [Area("Student")]
    public class AccountController : Controller
    {
        QLSinhVien db= new QLSinhVien();
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.StudentAccs.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null && user.RoleId == 2)
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
            return RedirectToAction("Login", new {area="Student"});
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePassVM model)
        {
            string tk=HttpContext.Session.GetString("Email");
            if (tk== null)
            {
                return RedirectToAction("Login", "Account", new { area="Student"});
            }
            var user = db.StudentAccs.FirstOrDefault(u => u.Email == tk);
            if(user == null)
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại.");
                return View(model);
            }
            if (model.oldPassword != user.Password)
            {
                ModelState.AddModelError("", "Mật khẩu cũ không đúng.");
                return View(model);
            }
            if (model.newPassword != model.confirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu mới và xác nhận mật khẩu không khớp.");
                return View(model);
            }
            user.Password = model.newPassword;
            db.StudentAccs.Update(user);
            db.SaveChanges();
            ViewBag.SuccessMessage = "Đổi mật khẩu thành công.";
            return View();
        }
    }
}
