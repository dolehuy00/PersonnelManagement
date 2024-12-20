﻿using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieAppApi.Service;
using PersonnelManagement.Data;
using PersonnelManagement.Mappers;
using PersonnelManagement.Repositories;
using PersonnelManagement.Repositories.Impl;
using PersonnelManagement.Services;
using PersonnelManagement.Services.Impl;
using StackExchange.Redis;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<PersonnelDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/// Dependency Injection
// Token service
builder.Services.AddScoped<TokenService>();
// Acount
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
// Employee
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
// Assignment
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
// Salary History
builder.Services.AddScoped<ISalaryHistoryService, SalaryHistoryService>();
builder.Services.AddScoped<ISalaryHistoryRepository, SalaryHistoryRepository>();
// Generic repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
// Role
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<RoleMapper>();
// Department
builder.Services.AddScoped<IDepartmentService, DepartmentService>(); // Đăng ký dịch vụ
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // Đăng ký repository
// Project
builder.Services.AddScoped<IProjectService, ProjectService>(); // Đăng ký dịch vụ
builder.Services.AddScoped<IProjectRepository, ProjectRepository>(); // Đăng ký repository
// DeptAssignment
builder.Services.AddScoped<IDeptAssignmentService, DeptAssignmentService>();
builder.Services.AddScoped<IDeptAssignmentRepository, DeptAssignmentRepository>();
// Assignment
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<ISalaryHistoryRepository, SalaryHistoryRepository>();
// Static File
builder.Services.AddScoped<IStaticFileService, StaticFileService>();

// Đăng ký dịch vụ ánh xạ (mapping service)
builder.Services.AddScoped<DeptAssignmentMapper>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["AccessTokenJwt:Audience"],
        ValidIssuer = builder.Configuration["AccessTokenJwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AccessTokenJwt:Key"]!))
    };
});

// Authorization policy
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
    .AddPolicy("UserOnly", policy => policy.RequireRole("User"))
    .AddPolicy("AllRoles", policy => policy.RequireRole("User", "Admin"));

// Json
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Limit rate
builder.Services.AddOptions();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis")!);
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;  // Cho phép cross-origin
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  // Chỉ gửi cookie qua HTTPS
});

// allow specific host reactJs app run
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Cho phép yêu cầu từ localhost:3000
            .AllowAnyHeader()                       // Cho phép bất kỳ tiêu đề
            .AllowAnyMethod()                       // Cho phép bất kỳ phương thức HTTP (GET, POST, PUT, DELETE)
            .AllowCredentials();                    // Cho phép gửi cookie/credentials
    });
});

// add static file server client
builder.Services.AddHttpClient("WebStorageClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7084"); // Địa chỉ của Web Storage
});


/// App
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost3000");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
