using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace QLySinhVien.ViewModel
{

    public class ScoresVM
    {
        public int ScoreID { get; set; }


        public int StudentID { get; set; }
        public string StudentName { get; set; }


        public int TestID { get; set; }
        public string TestName { get; set; }


        public string CourseTitle { get; set; }


        public double Marks { get; set; }
    }
    public class StudentScoreInputVM 
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }  // chỉ để hiển thị
        public double Mark { get; set; }
    }
  
    public class AddScoresVM {
        public int? CourseID { get; set; }
        public string CourseTitle { get; set; }  // chỉ để hiển thị
        public int? TestID { get; set; }
        public string TestName { get; set; }
        public List<StudentScoreInputVM> StudentScores { get; set; } = new List<StudentScoreInputVM>();
    }
    public class EditScoreVM {
        public int ScoreID { get; set; }

        public int StudentID { get; set; }
        public string StudentName { get; set; }

        public int TestID { get; set; }
        public string TestName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập điểm.")]
        [Range(0, 10, ErrorMessage = "Điểm phải nằm trong khoảng từ 0 đến 10.")]
        public double Marks { get; set; }
    }




}
