namespace QLySinhVien.ViewModel
{
    public class CourseScoreVM
    {
        public string CourseTitle { get; set; }
        public List<TestScoreVM> TestScores { get; set; } = new List<TestScoreVM>();
    }
}
