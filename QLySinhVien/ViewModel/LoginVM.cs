using System.ComponentModel.DataAnnotations;

namespace QLySinhVien.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
