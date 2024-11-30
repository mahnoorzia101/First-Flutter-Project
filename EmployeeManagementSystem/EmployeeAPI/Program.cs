using BLL.Services;
using DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Register Employee-related services
builder.Services.AddScoped<EmployeeRepository>(sp =>
    new EmployeeRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<EmployeeService>();

// Register Department-related services
builder.Services.AddScoped<DepartmentRepository>(sp =>
    new DepartmentRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<DepartmentService>();

// Add Controllers
builder.Services.AddControllers();

// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFlutterApp", policy =>
    {
        policy.WithOrigins("http://localhost:58442") // Replace with your Flutter app's URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Allows all origins
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:8000", "https://localhost:8000") // Replace with your Flutter app's URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use the CORS policy
// Switch to "AllowAll" if needed for broader testing

app.UseCors("AllowFlutterApp");


app.UseCors("AllowLocalhost");

app.UseCors("AllowAll");


app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
        context.Response.StatusCode = 204; // No Content
        return;
    }
    await next();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
