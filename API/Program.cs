using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
/*
* Zanim zmapujemy punkty końcowe kontrolera 
* należy dodać oprogramowanie pośredniczące CORS.
*/
builder.Services.AddCors();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(lambda => lambda.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("http://localhost:4200", "https://localhost:4200"));
app.MapControllers();

app.Run();
