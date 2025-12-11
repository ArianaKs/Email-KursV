using KursV.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ДОБАВЬТЕ ЭТО ДЛЯ CORS:
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Разрешаем все источники
              .AllowAnyMethod()   // Разрешаем все методы
              .AllowAnyHeader();  // Разрешаем все заголовки
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ИСПОЛЬЗУЙТЕ CORS (добавьте эту строку):
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();