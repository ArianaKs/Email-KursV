using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы
builder.Services.AddControllers();
builder.Services.AddHttpClient();

// Добавляем поддержку CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Используем CORS
app.UseCors("AllowAll");

// Убираем HTTPS редирект для простоты
// app.UseHttpsRedirection();
app.UseAuthorization();

// ВАЖНО: Сначала статические файлы
app.UseStaticFiles();
app.UseDefaultFiles();

// Затем контроллеры
app.MapControllers();

// Главная страница
app.MapFallbackToFile("index.html");

// Явно указываем порт
var port = 7200;
app.Run($"http://localhost:{port}");