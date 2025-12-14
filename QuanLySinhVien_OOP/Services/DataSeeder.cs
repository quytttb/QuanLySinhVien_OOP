using System.Text.Json;
using QuanLySinhVien_OOP.Models;

namespace QuanLySinhVien_OOP.Services;

/// <summary>
/// Service để seed dữ liệu mẫu cho sinh viên
/// Dùng để test và demo ứng dụng
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Seed danh sách 10 sinh viên mẫu
    /// Xóa hết dữ liệu cũ và tạo lại từ đầu
    /// </summary>
    public static async Task SeedStudentsAsync(IWebHostEnvironment environment)
    {
        var dataFolder = Path.Combine(environment.ContentRootPath, "Data");

        // Tạo thư mục Data nếu chưa có
        if (!Directory.Exists(dataFolder))
        {
            Directory.CreateDirectory(dataFolder);
        }

        var filePath = Path.Combine(dataFolder, "students.json");

        // Danh sách 10 sinh viên mẫu với tên Việt Nam
        var students = new List<Student>
        {
            new Student("SV001", "Nguyễn Văn An", new DateTime(2003, 3, 15), 8.5, 7.0, 9.0),
            new Student("SV002", "Trần Thị Bình", new DateTime(2002, 7, 22), 9.0, 8.5, 8.0),
            new Student("SV003", "Lê Hoàng Cường", new DateTime(2003, 1, 10), 6.5, 7.5, 7.0),
            new Student("SV004", "Phạm Minh Đức", new DateTime(2002, 11, 5), 7.0, 6.0, 8.5),
            new Student("SV005", "Hoàng Thị Hoa", new DateTime(2003, 5, 18), 9.5, 9.0, 9.5),
            new Student("SV006", "Võ Quang Hùng", new DateTime(2002, 9, 30), 5.5, 6.5, 6.0),
            new Student("SV007", "Đặng Thị Lan", new DateTime(2003, 2, 14), 8.0, 8.5, 7.5),
            new Student("SV008", "Bùi Văn Minh", new DateTime(2002, 8, 8), 7.5, 7.0, 8.0),
            new Student("SV009", "Ngô Thị Ngọc", new DateTime(2003, 12, 25), 6.0, 5.5, 7.0),
            new Student("SV010", "Trịnh Quốc Phong", new DateTime(2002, 4, 1), 8.0, 9.0, 8.5)
        };

        // Serialize và lưu file
        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(students, jsonOptions);
        await File.WriteAllTextAsync(filePath, json);

        Console.WriteLine($"✅ Đã seed {students.Count} sinh viên vào {filePath}");
    }
}
