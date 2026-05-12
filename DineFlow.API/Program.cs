using DineFlow.Application.Interfaces;
using DineFlow.Application.Services;
using DineFlow.Infrastructure.Data;
using DineFlow.Application.Interfaces;
using DineFlow.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace DineFlow.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // =========================
            // DATABASE (DbContext)
            // =========================
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // =========================
            // SERVICES
            // =========================

            // Establishment Service
            builder.Services.AddScoped<IEstablishmentService, EstablishmentService>();
            builder.Services.AddScoped<IDiningZoneService, DiningZoneService>();
            builder.Services.AddScoped<ITableService, TableService>();
            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddScoped<IMenuCategoryService, MenuCategoryService>();
            builder.Services.AddScoped<IMenuItemService, MenuItemService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            // Controllers
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // =========================
            // PIPELINE
            // =========================

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}