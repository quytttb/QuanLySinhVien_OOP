using System.Text.Json;
using QuanLySinhVien_OOP.Models;

namespace QuanLySinhVien_OOP.Services;

/// <summary>
/// StudentService - Implementation của IStudentService
/// Thể hiện nguyên lý Encapsulation: đóng gói logic xử lý và dữ liệu
/// </summary>
public class StudentService : IStudentService
{
     // Private field - Encapsulation: danh sách sinh viên được ẩn khỏi bên ngoài
     private List<Student> _students = new();

     // Đường dẫn file JSON để lưu dữ liệu
     private readonly string _dataFilePath;

     // Đối tượng khóa để đảm bảo thread-safe khi đọc/ghi file
     private readonly SemaphoreSlim _fileLock = new(1, 1);

     // Cấu hình JSON serialization
     private readonly JsonSerializerOptions _jsonOptions = new()
     {
          WriteIndented = true, // Format đẹp, dễ đọc
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase
     };

     /// <summary>
     /// Constructor - khởi tạo service với đường dẫn file
     /// </summary>
     /// <param name="environment">IWebHostEnvironment để lấy đường dẫn gốc</param>
     public StudentService(IWebHostEnvironment environment)
     {
          // Tạo đường dẫn đến file data trong thư mục Data
          var dataFolder = Path.Combine(environment.ContentRootPath, "Data");

          // Tạo thư mục Data nếu chưa tồn tại
          if (!Directory.Exists(dataFolder))
          {
               Directory.CreateDirectory(dataFolder);
          }

          _dataFilePath = Path.Combine(dataFolder, "students.json");
     }

     /// <summary>
     /// Thêm sinh viên mới - kiểm tra trùng mã trước khi thêm
     /// </summary>
     public async Task AddStudentAsync(Student sv)
     {
          if (sv == null)
               throw new ArgumentNullException(nameof(sv));

          // Kiểm tra mã SV đã tồn tại chưa
          if (await IsMaSVExistsAsync(sv.MaSV))
          {
               throw new InvalidOperationException($"Mã sinh viên '{sv.MaSV}' đã tồn tại!");
          }

          _students.Add(sv);

          // Tự động lưu file sau khi thêm
          await SaveToFileAsync();
     }

     /// <summary>
     /// Cập nhật thông tin sinh viên theo mã
     /// </summary>
     public async Task UpdateStudentAsync(Student sv)
     {
          if (sv == null)
               throw new ArgumentNullException(nameof(sv));

          var existingStudent = _students.FirstOrDefault(s =>
              s.MaSV.Equals(sv.MaSV, StringComparison.OrdinalIgnoreCase));

          if (existingStudent == null)
          {
               throw new InvalidOperationException($"Không tìm thấy sinh viên với mã '{sv.MaSV}'!");
          }

          // Cập nhật thông tin
          existingStudent.HoTen = sv.HoTen;
          existingStudent.NgaySinh = sv.NgaySinh;
          existingStudent.DiemToan = sv.DiemToan;
          existingStudent.DiemVan = sv.DiemVan;
          existingStudent.DiemAnh = sv.DiemAnh;

          // Tự động lưu file sau khi cập nhật
          await SaveToFileAsync();
     }

     /// <summary>
     /// Xóa sinh viên theo mã
     /// </summary>
     public async Task DeleteStudentAsync(string maSV)
     {
          if (string.IsNullOrWhiteSpace(maSV))
               throw new ArgumentNullException(nameof(maSV));

          var student = _students.FirstOrDefault(s =>
              s.MaSV.Equals(maSV.Trim(), StringComparison.OrdinalIgnoreCase));

          if (student == null)
          {
               throw new InvalidOperationException($"Không tìm thấy sinh viên với mã '{maSV}'!");
          }

          _students.Remove(student);

          // Tự động lưu file sau khi xóa
          await SaveToFileAsync();
     }

     /// <summary>
     /// Tìm kiếm sinh viên theo từ khóa (tìm trong mã SV và họ tên)
     /// </summary>
     public Task<List<Student>> SearchStudentAsync(string keyword)
     {
          if (string.IsNullOrWhiteSpace(keyword))
          {
               return Task.FromResult(_students.ToList());
          }

          var searchTerm = keyword.Trim().ToLower();

          var result = _students
              .Where(s => s.MaSV.ToLower().Contains(searchTerm) ||
                          s.HoTen.ToLower().Contains(searchTerm))
              .ToList();

          return Task.FromResult(result);
     }

     /// <summary>
     /// Lấy tất cả sinh viên (trả về bản sao để bảo vệ dữ liệu gốc)
     /// </summary>
     public Task<List<Student>> GetAllStudentsAsync()
     {
          return Task.FromResult(_students.ToList());
     }

     /// <summary>
     /// Lấy sinh viên theo mã
     /// </summary>
     public Task<Student?> GetStudentByIdAsync(string maSV)
     {
          if (string.IsNullOrWhiteSpace(maSV))
               return Task.FromResult<Student?>(null);

          var student = _students.FirstOrDefault(s =>
              s.MaSV.Equals(maSV.Trim(), StringComparison.OrdinalIgnoreCase));

          return Task.FromResult(student);
     }

     /// <summary>
     /// Kiểm tra mã sinh viên đã tồn tại chưa
     /// </summary>
     public Task<bool> IsMaSVExistsAsync(string maSV)
     {
          if (string.IsNullOrWhiteSpace(maSV))
               return Task.FromResult(false);

          var exists = _students.Any(s =>
              s.MaSV.Equals(maSV.Trim(), StringComparison.OrdinalIgnoreCase));

          return Task.FromResult(exists);
     }

     /// <summary>
     /// Lưu danh sách sinh viên ra file JSON
     /// Sử dụng SemaphoreSlim để đảm bảo thread-safe
     /// </summary>
     public async Task SaveToFileAsync()
     {
          await _fileLock.WaitAsync();
          try
          {
               var json = JsonSerializer.Serialize(_students, _jsonOptions);
               await File.WriteAllTextAsync(_dataFilePath, json);
          }
          finally
          {
               _fileLock.Release();
          }
     }

     /// <summary>
     /// Đọc danh sách sinh viên từ file JSON
     /// Nếu file không tồn tại, khởi tạo danh sách rỗng
     /// </summary>
     public async Task LoadFromFileAsync()
     {
          await _fileLock.WaitAsync();
          try
          {
               if (File.Exists(_dataFilePath))
               {
                    var json = await File.ReadAllTextAsync(_dataFilePath);

                    if (!string.IsNullOrWhiteSpace(json))
                    {
                         var students = JsonSerializer.Deserialize<List<Student>>(json, _jsonOptions);
                         _students = students ?? new List<Student>();
                    }
               }
               else
               {
                    // Tạo file rỗng nếu chưa tồn tại
                    _students = new List<Student>();
                    await File.WriteAllTextAsync(_dataFilePath, "[]");
               }
          }
          catch (JsonException ex)
          {
               // Nếu file JSON bị lỗi, khởi tạo danh sách rỗng
               Console.WriteLine($"Lỗi đọc file JSON: {ex.Message}");
               _students = new List<Student>();
          }
          finally
          {
               _fileLock.Release();
          }
     }
}
