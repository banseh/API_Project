
using BugTicketSys.BL;
using BugTicketSys.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;

namespace BugTickectSysApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //var connectionString = builder.Configuration.GetConnectionString("default");

            //builder.Services.AddDbContext<BugticketContext>(options =>
            //    options.UseSqlServer(connectionString));



            //builder.Services.AddScoped<IprojectRepo, projectRepo>();


            //Console.WriteLine(connectionstring);


            builder.Services.AdddataAccessServices(builder.Configuration);
            builder.Services.Addbuissinessservices();  

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference("/scalar");
            }


            var imgfolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadImg");
            Directory.CreateDirectory(imgfolder);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(imgfolder),
                RequestPath = "/api/my-static-files"
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
