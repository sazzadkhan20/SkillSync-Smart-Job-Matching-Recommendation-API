using BLL.DomainLogic.Calculators;
using BLL.DomainLogic.Converters;
using BLL.DomainLogic.Specifications;
using BLL.DTOs;
using BLL.Services;
using DAL;
using DAL.EF;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Dependency Injection
builder.Services.AddScoped<DataAccessFactory>();
builder.Services.AddScoped<CandidateRepository>();
builder.Services.AddScoped<JobPostRepository>();
builder.Services.AddScoped<JobApplicationRepository>();
//builder.Services.AddScoped<JobApplicationRepository>();
builder.Services.AddScoped<CandidateService>();
builder.Services.AddScoped<JobPostService>();
builder.Services.AddScoped<JobApplicationService>();
builder.Services.AddScoped<CandidateDTO>();
builder.Services.AddScoped<JobPostDTO>();
builder.Services.AddScoped<ISkillMatchCalculator, SkillMatchCalculator>();
builder.Services.AddScoped<JobPostSpecification>();
builder.Services.AddDbContext<SJMDbContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
