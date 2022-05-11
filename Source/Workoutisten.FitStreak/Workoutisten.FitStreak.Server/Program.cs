using Microsoft.EntityFrameworkCore;
using Workoutisten.FitStreak.Server.Database;
using Workoutisten.FitStreak.Server.Database.Implementation;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext to the container
builder.Services.AddDbContext<FitStreakDbContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(builder.Configuration.GetConnectionString("FitStreakDatabase"),
        optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(FitStreakDbContext).Assembly.FullName));
    options.UseTriggers();
});

// Add Triggers to the container
builder.Services.AddTriggers();

// Add own services to the container
builder.Services.AddScoped<IRepository, Repository>();

// Build the web application
var app = builder.Build();

// Ensure that the database exists
using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    using var dbContext = scope.ServiceProvider.GetRequiredService<FitStreakDbContext>();
    dbContext.Database.Migrate();
}

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
