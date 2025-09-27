
using SzakdolgozatBackend.Entities;
using SzakdolgozatBackend.Profiles;
using SzakdolgozatBackend.Services;

namespace SzakdolgozatBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>();

            // Add services to the container.
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ILessonService, LessonService>();

            // AutoMapper Configuration
            builder.Services.AddAutoMapper(cfg => { }, typeof(UserProfile));
            builder.Services.AddAutoMapper(cfg => { }, typeof(LessonProfile));

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
