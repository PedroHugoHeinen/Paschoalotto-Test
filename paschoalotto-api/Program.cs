using paschoalotto_api.Data;
using Microsoft.EntityFrameworkCore;
using paschoalotto_api.Services.Interfaces;
using paschoalotto_api.Services;
using paschoalotto_api.Repository.Interfaces;
using paschoalotto_api.Repository;
using AutoMapper;
using paschoalotto_api.Globals.DTOs;
using paschoalotto_api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserDTO>(); });
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddHttpClient<IUserRandomService, UserRandomService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Paschoalotto API V1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
