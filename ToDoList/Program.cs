using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "logs", "diagnostics.txt"),
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: 10 * 1024 * 1024,
                retainedFileCountLimit: 2,
                rollOnFileSizeLimit: true,
                shared: true,
                flushToDiskInterval: TimeSpan.FromSeconds(1))
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();
            // Add services to the container.
            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer("Server=.\\SQLExpress;Initial Catalog=TasksDataBase;Trusted_Connection=Yes;Integrated Security=true;TrustServerCertificate=True");
                //options.UseSqlite("Data Source=helloapp.db");
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                      new string[] { }
                    }
                 });
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    var secret = builder.Configuration.GetValue<string>("Auth:Secret")!;
                    var issuer = builder.Configuration.GetValue<string>("Auth:myIssuer")!;
                    var audience = builder.Configuration.GetValue<string>("Auth:myAudience")!;
                    var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = mySecurityKey
                    };
                });
            builder.Services.AddAuthorization();

            builder.Services.AddTransient<ITaskItem, TaskItem>();
            builder.Services.AddTransient<ITaskService, TaskService>();
            builder.Services.AddScoped<ITaskRepository, DBTaskRepository>();

            builder.Services.AddTransient<ILifeSphere, LifeSphere>();
            builder.Services.AddTransient<ILifeSphereService, LifeSphereService>();
            builder.Services.AddScoped<ILifeSphereRepository, DBLifeSphereRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                try
                {
                    Console.WriteLine("Request '{0}' began at {1}", context.Request.Path, DateTime.Now);
                    await next();
                    Console.WriteLine("Request '{0}' finished", context.Request.Path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });

            app.MapControllers();
            app.MapGet("v2/TaskItems/{id}",
                (HttpContext requestDelegate, int id) =>
                {
                    var service = requestDelegate.RequestServices.GetService<ITaskService>()!;
                    var taskItem = service.GetTaskById(id);
                    if (taskItem == null) return Results.NoContent();
                    return Results.Ok(taskItem);
                })
             .WithName("Test")
             .WithOpenApi();

            app.Run();
        }
    }
}