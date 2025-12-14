using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien_OOP.Models;

/// <summary>
/// Lớp cha Person - thể hiện nguyên lý Abstraction và Encapsulation
/// Đóng gói các thuộc tính cơ bản của một người
/// </summary>
public class Person
{
     // Private fields - Encapsulation: ẩn dữ liệu bên trong
     private string _hoTen = string.Empty;
     private DateTime _ngaySinh;

     /// <summary>
     /// Họ và tên của người
     /// </summary>
     [Required(ErrorMessage = "Họ tên là bắt buộc")]
     [StringLength(100, MinimumLength = 2, ErrorMessage = "Họ tên phải từ 2-100 ký tự")]
     public string HoTen
     {
          get => _hoTen;
          set => _hoTen = value?.Trim() ?? string.Empty;
     }

     /// <summary>
     /// Ngày sinh của người
     /// </summary>
     [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
     [DataType(DataType.Date)]
     public DateTime NgaySinh
     {
          get => _ngaySinh;
          set => _ngaySinh = value;
     }

     /// <summary>
     /// Constructor mặc định
     /// </summary>
     public Person()
     {
          _hoTen = string.Empty;
          _ngaySinh = DateTime.Now;
     }

     /// <summary>
     /// Constructor có tham số
     /// </summary>
     /// <param name="hoTen">Họ và tên</param>
     /// <param name="ngaySinh">Ngày sinh</param>
     public Person(string hoTen, DateTime ngaySinh)
     {
          HoTen = hoTen;
          NgaySinh = ngaySinh;
     }

     /// <summary>
     /// Phương thức ảo ToString - cho phép lớp con override (Polymorphism)
     /// </summary>
     /// <returns>Chuỗi mô tả thông tin người</returns>
     public virtual string ToDisplayString()
     {
          return $"Họ tên: {HoTen}, Ngày sinh: {NgaySinh:dd/MM/yyyy}";
     }
}
