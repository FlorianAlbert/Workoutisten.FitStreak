using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Workoutisten.FitStreak.Server.Database;
using Workoutisten.FitStreak.Server.Database.Implementation;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Extensions;
using Workoutisten.FitStreak.Server.Service.Implementation.Converter;
using Workoutisten.FitStreak.Server.Service.Implementation.Converter.User;
using Workoutisten.FitStreak.Server.Service.Implementation.Training;
using Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using Workoutisten.FitStreak.Server.Service.Interface.Training;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
using User = Workoutisten.FitStreak.Server.Model.Account.User;
using UserDto = Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Workoutisten.FitStreak API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

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

// User Management
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IFriendshipService, FriendshipService>(); 
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHashingService, PasswordHashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAlphaNumericStringGenerator, AlphaNumericStringGenerator>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Training
builder.Services.AddScoped<IExerciseEntryService, ExerciseEntryService>();
builder.Services.AddScoped<IExerciseGroupService, ExerciseGroupService>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();

// Converter
builder.Services.AddTransient<IConverterWrapper, ConverterWrapper>();
builder.Services.AddTransient<IConverter<User, UserDto>, UserConverter>();

// Add authentication to the container
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtBearer:TokenSecret"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

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
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Workoutisten.FitStreak API");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
