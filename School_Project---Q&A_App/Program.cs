using Microsoft.EntityFrameworkCore;
using School_Project___Q_A_App.Models;
using School_Project___Q_A_App.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositories
builder.Services.AddScoped(typeof(GenericRepository<>));
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<CategoryRepository>();

// Automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("sqlCon"));
});

builder.Services.AddCors(p => p.AddPolicy("corspolicy", opt =>
{
    opt.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corspolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
