# 🎓 Quản Lý Sinh Viên - OOP

Hệ thống quản lý sinh viên được xây dựng bằng **Blazor Web App (.NET 10.0)**, áp dụng đầy đủ 4 nguyên lý OOP.

## 📋 Mô tả

Ứng dụng web quản lý danh sách sinh viên với các chức năng:
- ✅ **Thêm** sinh viên mới
- ✅ **Sửa** thông tin sinh viên
- ✅ **Xóa** sinh viên (có xác nhận)
- ✅ **Tìm kiếm** realtime theo mã SV hoặc họ tên
- ✅ **Tính điểm trung bình** tự động
- ✅ **Lưu trữ** dữ liệu vào file JSON
- ✅ **Seed data** - tạo dữ liệu mẫu

## 🏗️ Công nghệ

- **.NET 10.0**
- **Blazor Web App** (Interactive Server)
- **Bootstrap 5** (UI Framework)
- **System.Text.Json** (Lưu trữ dữ liệu)

## 📁 Cấu trúc Project

```
QuanLySinhVien_OOP/
├── Models/                          # Các lớp Model
│   ├── Person.cs                    # Lớp cha (Abstraction, Encapsulation)
│   └── Student.cs                   # Lớp con (Inheritance, Polymorphism)
├── Services/                        # Business Logic Layer
│   ├── IStudentService.cs           # Interface (Abstraction)
│   ├── StudentService.cs            # Implementation (Encapsulation)
│   └── DataSeeder.cs                # Seed dữ liệu mẫu
├── Components/
│   ├── Pages/                       # Các trang Razor
│   │   ├── Home.razor               # Danh sách sinh viên
│   │   └── AddEditStudent.razor     # Form thêm/sửa
│   └── Layout/                      # Layout components
│       ├── MainLayout.razor
│       └── NavMenu.razor
├── Data/                            # Thư mục chứa dữ liệu
│   └── students.json                # File JSON lưu sinh viên
└── wwwroot/                         # Static files
    ├── app.css                      # Custom CSS
    └── favicon.svg                  # Icon sinh viên
```

## 🎯 Áp dụng 4 Nguyên lý OOP

| Nguyên lý | Áp dụng trong Project |
|-----------|----------------------|
| **Encapsulation** (Đóng gói) | - Private fields `_hoTen`, `_maSV`, `_students`<br>- Public properties với getter/setter<br>- Validation trong properties |
| **Inheritance** (Kế thừa) | - `Student` kế thừa từ `Person`<br>- Tái sử dụng HoTen, NgaySinh từ lớp cha<br>- Mở rộng với MaSV, điểm các môn |
| **Polymorphism** (Đa hình) | - `override ToDisplayString()` trong Student<br>- Ghi đè phương thức của lớp cha<br>- Hiển thị thông tin khác nhau tùy lớp |
| **Abstraction** (Trừu tượng) | - Interface `IStudentService`<br>- Tách biệt contract và implementation<br>- Dependency Injection với interface |

## 🚀 Hướng dẫn Cài đặt

### Yêu cầu
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) trở lên
- IDE: Visual Studio 2022, Rider, hoặc VS Code

### Các bước cài đặt

1. **Clone repository**
   ```bash
   git clone https://github.com/quytttb/QuanLySinhVien_OOP.git
   cd QuanLySinhVien_OOP
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Build project**
   ```bash
   dotnet build
   ```

4. **Chạy ứng dụng**
   
   - Chạy bình thường:
     ```bash
     dotnet run --project QuanLySinhVien_OOP/QuanLySinhVien_OOP.csproj
     ```
   
   - Chạy với seed data (tạo 10 sinh viên mẫu):
     ```bash
     dotnet run --project QuanLySinhVien_OOP/QuanLySinhVien_OOP.csproj -- --seed
     ```

5. **Truy cập ứng dụng**
   ```
   http://localhost:5241
   ```

## 💡 Sử dụng

### Trang chủ - Danh sách Sinh viên
- Xem danh sách toàn bộ sinh viên với điểm TB
- Tìm kiếm realtime theo mã SV hoặc họ tên
- Nút **Thêm Sinh viên** để tạo mới
- Nút **Sửa** để chỉnh sửa thông tin
- Nút **Xóa** để xóa sinh viên (có modal xác nhận)

### Form Thêm/Sửa Sinh viên
- Nhập đầy đủ thông tin: Mã SV, Họ tên, Ngày sinh
- Nhập điểm 3 môn: Toán, Văn, Anh (0-10)
- Xem preview điểm trung bình
- Validation tự động với DataAnnotations

### Dữ liệu
- Tự động lưu vào `Data/students.json` mỗi khi thay đổi
- Tự động load khi khởi động ứng dụng
- Có thể seed lại dữ liệu mẫu bằng flag `--seed`

## 📊 Dữ liệu mẫu (Seed)

Chạy với flag `--seed` sẽ tạo 10 sinh viên:
- Mã SV: SV001 - SV010
- Tên Việt Nam ngẫu nhiên
- Điểm các môn từ 5.0 - 9.5
- Ngày sinh từ năm 2002-2003

## 🎨 Giao diện

- **Bootstrap 5** responsive design
- **Bootstrap Icons** cho icon đẹp mắt
- **Badge màu** hiển thị điểm TB theo thang:
  - 🟢 Xanh lá (≥8.0): Giỏi
  - 🔵 Xanh dương (≥6.5): Khá
  - 🟡 Vàng (≥5.0): Trung bình
  - 🔴 Đỏ (<5.0): Yếu
- **Modal xác nhận** khi xóa
- **Form validation** với thông báo lỗi rõ ràng

## 📝 Tính năng nâng cao

- **Thread-safe**: Sử dụng `SemaphoreSlim` cho đọc/ghi file
- **Singleton Service**: Giữ dữ liệu trong bộ nhớ
- **Auto-save**: Tự động lưu sau mỗi thao tác CRUD
- **Error handling**: Xử lý lỗi đầy đủ với try-catch
- **Input validation**: Kiểm tra dữ liệu ở cả client và server

## 🐛 Troubleshooting

**Port đã được sử dụng?**
```bash
# Dừng process cũ
pkill -f dotnet
# Hoặc thay đổi port trong Properties/launchSettings.json
```

**File JSON bị lỗi?**
```bash
# Xóa file và chạy lại với --seed
rm QuanLySinhVien_OOP/Data/students.json
dotnet run -- --seed
```

## 👨‍💻 Tác giả

Phát triển bởi **Quý** cho môn Lập trình Hướng đối tượng

## 📄 License

MIT License - Tự do sử dụng cho mục đích học tập

---

⭐ **Star** repo này nếu bạn thấy hữu ích!