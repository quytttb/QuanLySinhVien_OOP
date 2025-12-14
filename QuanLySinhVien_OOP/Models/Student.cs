using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien_OOP.Models;

/// <summary>
/// Lớp Student kế thừa từ Person - thể hiện nguyên lý Inheritance
/// Mở rộng thêm các thuộc tính và phương thức đặc thù cho sinh viên
/// </summary>
public class Student : Person
{
     // Private fields - Encapsulation
     private string _maSV = string.Empty;
     private double _diemToan;
     private double _diemVan;
     private double _diemAnh;

     /// <summary>
     /// Mã sinh viên - định danh duy nhất
     /// </summary>
     [Required(ErrorMessage = "Mã sinh viên là bắt buộc")]
     [StringLength(20, MinimumLength = 3, ErrorMessage = "Mã SV phải từ 3-20 ký tự")]
     [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Mã SV chỉ chứa chữ và số")]
     public string MaSV
     {
          get => _maSV;
          set => _maSV = value?.Trim().ToUpper() ?? string.Empty;
     }

     /// <summary>
     /// Điểm môn Toán (0-10)
     /// </summary>
     [Required(ErrorMessage = "Điểm Toán là bắt buộc")]
     [Range(0, 10, ErrorMessage = "Điểm Toán phải từ 0 đến 10")]
     public double DiemToan
     {
          get => _diemToan;
          set => _diemToan = Math.Clamp(value, 0, 10);
     }

     /// <summary>
     /// Điểm môn Văn (0-10)
     /// </summary>
     [Required(ErrorMessage = "Điểm Văn là bắt buộc")]
     [Range(0, 10, ErrorMessage = "Điểm Văn phải từ 0 đến 10")]
     public double DiemVan
     {
          get => _diemVan;
          set => _diemVan = Math.Clamp(value, 0, 10);
     }

     /// <summary>
     /// Điểm môn Anh (0-10)
     /// </summary>
     [Required(ErrorMessage = "Điểm Anh là bắt buộc")]
     [Range(0, 10, ErrorMessage = "Điểm Anh phải từ 0 đến 10")]
     public double DiemAnh
     {
          get => _diemAnh;
          set => _diemAnh = Math.Clamp(value, 0, 10);
     }

     /// <summary>
     /// Constructor mặc định
     /// </summary>
     public Student() : base()
     {
          _maSV = string.Empty;
          _diemToan = 0;
          _diemVan = 0;
          _diemAnh = 0;
     }

     /// <summary>
     /// Constructor đầy đủ tham số
     /// </summary>
     /// <param name="maSV">Mã sinh viên</param>
     /// <param name="hoTen">Họ và tên</param>
     /// <param name="ngaySinh">Ngày sinh</param>
     /// <param name="diemToan">Điểm Toán</param>
     /// <param name="diemVan">Điểm Văn</param>
     /// <param name="diemAnh">Điểm Anh</param>
     public Student(string maSV, string hoTen, DateTime ngaySinh,
                    double diemToan, double diemVan, double diemAnh)
         : base(hoTen, ngaySinh)
     {
          MaSV = maSV;
          DiemToan = diemToan;
          DiemVan = diemVan;
          DiemAnh = diemAnh;
     }

     /// <summary>
     /// Tính điểm trung bình của sinh viên
     /// </summary>
     /// <returns>Điểm trung bình (làm tròn 2 chữ số)</returns>
     public double TinhDiemTB()
     {
          return Math.Round((DiemToan + DiemVan + DiemAnh) / 3, 2);
     }

     /// <summary>
     /// Override phương thức ToDisplayString - thể hiện Polymorphism
     /// Ghi đè để hiển thị thông tin đầy đủ của sinh viên
     /// </summary>
     /// <returns>Chuỗi mô tả đầy đủ thông tin sinh viên</returns>
     public override string ToDisplayString()
     {
          return $"Mã SV: {MaSV} | {base.ToDisplayString()} | " +
                 $"Toán: {DiemToan:F1} | Văn: {DiemVan:F1} | Anh: {DiemAnh:F1} | " +
                 $"ĐTB: {TinhDiemTB():F2}";
     }

     /// <summary>
     /// Override ToString cho serialization và debug
     /// </summary>
     public override string ToString()
     {
          return ToDisplayString();
     }
}
