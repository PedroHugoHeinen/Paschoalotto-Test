using paschoalotto_api.Data;
using Microsoft.EntityFrameworkCore;
using paschoalotto_api.Services.Interfaces;
using paschoalotto_api.Services;
using paschoalotto_api.Repository.Interfaces;
using paschoalotto_api.Repository;
using AutoMapper;
using paschoalotto_api.Globals.DTOs;
using System.Diagnostics;
using paschoalotto_api.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options => { options.AddPolicy("AllowAll", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }); });

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserDTO>().ReverseMap(); });
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddHttpClient<IUserRandomService, UserRandomService>();

builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Paschoalotto API", Version = "v1" }); });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(suio => suio.SwaggerEndpoint("/swagger/v1/swagger.json", "Paschoalotto API V1"));

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

var url = "https://localhost:7287/swagger/index.html";
Process.Start(new ProcessStartInfo
{
    FileName = url,
    UseShellExecute = true
});

app.Run();
