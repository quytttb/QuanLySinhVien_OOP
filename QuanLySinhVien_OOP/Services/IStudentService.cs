using QuanLySinhVien_OOP.Models;

namespace QuanLySinhVien_OOP.Services;

/// <summary>
/// Interface IStudentService - thể hiện nguyên lý Abstraction
/// Định nghĩa các phương thức cần thiết cho việc quản lý sinh viên
/// Tách biệt contract (giao diện) khỏi implementation (triển khai)
/// </summary>
public interface IStudentService
{
     /// <summary>
     /// Thêm sinh viên mới vào danh sách
     /// </summary>
     /// <param name="sv">Đối tượng sinh viên cần thêm</param>
     /// <returns>Task hoàn thành khi thêm xong</returns>
     Task AddStudentAsync(Student sv);

     /// <summary>
     /// Cập nhật thông tin sinh viên đã tồn tại
     /// </summary>
     /// <param name="sv">Đối tượng sinh viên với thông tin mới</param>
     /// <returns>Task hoàn thành khi cập nhật xong</returns>
     Task UpdateStudentAsync(Student sv);

     /// <summary>
     /// Xóa sinh viên theo mã
     /// </summary>
     /// <param name="maSV">Mã sinh viên cần xóa</param>
     /// <returns>Task hoàn thành khi xóa xong</returns>
     Task DeleteStudentAsync(string maSV);

     /// <summary>
     /// Tìm kiếm sinh viên theo từ khóa (mã SV hoặc họ tên)
     /// </summary>
     /// <param name="keyword">Từ khóa tìm kiếm</param>
     /// <returns>Danh sách sinh viên phù hợp</returns>
     Task<List<Student>> SearchStudentAsync(string keyword);

     /// <summary>
     /// Lấy tất cả sinh viên trong danh sách
     /// </summary>
     /// <returns>Danh sách tất cả sinh viên</returns>
     Task<List<Student>> GetAllStudentsAsync();

     /// <summary>
     /// Lấy sinh viên theo mã
     /// </summary>
     /// <param name="maSV">Mã sinh viên cần tìm</param>
     /// <returns>Sinh viên nếu tìm thấy, null nếu không</returns>
     Task<Student?> GetStudentByIdAsync(string maSV);

     /// <summary>
     /// Kiểm tra mã sinh viên đã tồn tại chưa
     /// </summary>
     /// <param name="maSV">Mã sinh viên cần kiểm tra</param>
     /// <returns>True nếu đã tồn tại, False nếu chưa</returns>
     Task<bool> IsMaSVExistsAsync(string maSV);

     /// <summary>
     /// Lưu dữ liệu ra file JSON
     /// </summary>
     /// <returns>Task hoàn thành khi lưu xong</returns>
     Task SaveToFileAsync();

     /// <summary>
     /// Đọc dữ liệu từ file JSON
     /// </summary>
     /// <returns>Task hoàn thành khi đọc xong</returns>
     Task LoadFromFileAsync();
}
