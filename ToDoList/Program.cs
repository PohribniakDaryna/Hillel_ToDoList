using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer("Server=.\\SQLExpress;Initial Catalog=TasksDataBase;Trusted_Connection=Yes;Integrated Security=true;TrustServerCertificate=True");
                //options.UseSqlite("Data Source=helloapp.db");
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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