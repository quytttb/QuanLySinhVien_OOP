using QuanLySinhVien_OOP.Components;
using QuanLySinhVien_OOP.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Đăng ký StudentService là Singleton để giữ dữ liệu trong bộ nhớ
// và đảm bảo chỉ có một instance duy nhất trong suốt vòng đời ứng dụng
builder.Services.AddSingleton<IStudentService, StudentService>();

var app = builder.Build();

// Kiểm tra argument --seed để seed dữ liệu mẫu
if (args.Contains("--seed"))
{
    Console.WriteLine("🌱 Đang seed dữ liệu sinh viên...");
    await DataSeeder.SeedStudentsAsync(app.Environment);
    Console.WriteLine("✅ Seed hoàn tất!");
}

// Load dữ liệu sinh viên từ file khi khởi động ứng dụng
var studentService = app.Services.GetRequiredService<IStudentService>();
await studentService.LoadFromFileAsync();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();