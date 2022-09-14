using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Server.DataInitializers;
using Server.Infrastructure;
using Server.Interfaces.DataInitializerInterfaces;
using Server.Interfaces.RepositoryInterfaces;
using Server.Interfaces.ServiceInterfaces;
using Server.Interfaces.TokenMakerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Interfaces.ValidationInterfaces;
using Server.Mapping;
using Server.Models;
using Server.Repositories;
using Server.Services;
using Server.TokenMakers;
using Server.UnitOfWork;
using Server.Validations;
using System.Text;

string _cors = "cors";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RVA-Projekat", Version = "v1" });

    // Needed for sending token via Swagger for testing purposes.
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            { 
                Reference = new OpenApiReference
                { 
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }, 
            new string[]{}
        }
    });
});

// Adding a Policy for Claim validation.
builder.Services.AddAuthorization(options => 
{
    options.AddPolicy("SystemUser", policy => policy.RequireClaim("Sys_user")); // Can require that has authorization for policy in controller.
});

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"]));
    options.TokenValidationParameters = new TokenValidationParameters //Podesavamo parametre za validaciju pristiglih tokena
    {
        ValidateIssuer = true, //Validira izdavaoca tokena
        ValidateAudience = false, //Kazemo da ne validira primaoce tokena
        ValidateLifetime = true, //Validira trajanje tokena
        ValidateIssuerSigningKey = true, //validira potpis token, ovo je jako vazno!
        ValidIssuer = "http://localhost:44344", //odredjujemo koji server je validni izdavalac
        IssuerSigningKey = key //navodimo privatni kljuc kojim su potpisani nasi tokeni
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: _cors, builder => {
        builder.WithOrigins("http://localhost:3000") //Ovde navodimo koje sve aplikacije smeju kontaktirati nasu,u ovom slucaju nas Angular front
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

#region Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IStudentResultService, StudentResultService>();
#endregion

#region Factories
builder.Services.AddScoped<ITokenMakerFactory, TokenMakerFactory>();
#endregion

#region Validation
builder.Services.AddScoped<IValidation<User>, UserValidation>();
#endregion

#region DataInitializers
builder.Services.AddScoped<IDataInitializer, DataInitializer>();
builder.Services.AddScoped<IUserDataInitializer, UserDataInitializer>();
builder.Services.AddScoped<IStudentDataInitializer, StudentDataInitializer>();
builder.Services.AddScoped<ISubjectDataInitializer, SubjectDataInitializer>();
builder.Services.AddScoped<IExamDataInitializer, ExamDataInitializer>();
builder.Services.AddScoped<IStudentResultDataInitializer, StudentResultDataInitializer>();
#endregion

#region Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>(); 
builder.Services.AddScoped<IStudentResultRepository, StudentResultRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<FacultyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FacultyDbContext")));  // Make the DB.
#endregion

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new UserMappingProfile());
    mc.AddProfile(new ExamMappingProfile());
    mc.AddProfile(new StudentMappingProfile());
    mc.AddProfile(new StudentResultMappingProfile());
    mc.AddProfile(new SubjectMappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<IDataInitializer>().InitializeData();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "rva-project v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(_cors);

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => 
{
    endpoints.MapControllers();
});

app.Run();
