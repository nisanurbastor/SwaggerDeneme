using Microsoft.EntityFrameworkCore;
using MyApi.DbOperations;

var builder = WebApplication.CreateBuilder(args);

// DbContext'i ekle (Build'den önce!)
builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseInMemoryDatabase("BookStoreDB"));

// Controller ve Swagger desteği
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// (Gerekliyse) DbContext ve diğer servis kayıtları burada yapılmalı
// builder.Services.AddDbContext<BoardGamesDBContext>(...);

var app = builder.Build();

// Eğer development ortamındaysa swagger göster
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

//DataGenerator'ı çalıştır (uygulama başlarken)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DbGenerator.Initialize(services);
}
// Uygulamayı çalıştır
app.Run();
