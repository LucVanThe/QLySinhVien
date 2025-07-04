namespace QLySinhVien.ViewModel
{
    public class SVdangkyVM
    {
        public int MaDangky { get; set; }
        public int MaSV { get; set; }
        public string TenMonHoc { get; set; }
        public string HoTen { get; set; }
        public DateOnly? NgayDangKy { get; set; }
    }
    public class ThemSVdangkyVM
    {
        public int MaDangky { get; set; }
        public int MaSV { get; set; }
        public string HoTen { get; set; }
        public int MaMonHoc { get; set; }
        public string TenMonHoc { get; set; }
        public DateOnly? NgayDangKy { get; set; }
    }
}
