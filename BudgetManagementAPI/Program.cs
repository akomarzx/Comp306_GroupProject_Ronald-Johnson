﻿
using BudgetManagementAPI.Database;
using BudgetManagementAPI.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BudgetManagementAPI.Controllers;
using BudgetManagementAPI.Repository;
using Microsoft.OpenApi.Models;
using Humanizer;
using Swashbuckle.AspNetCore.Filters;
using BudgetManagementAPI.Mapper;

namespace BudgetManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddEntityFrameworkStores<BudgetDBContext>();

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddControllers();

            builder.Services.AddAuthorization();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            if (!builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddSecretsManager(configurator: config =>
                {
                    config.SecretFilter = record => record.Name.StartsWith($"ConnectionStrings__DefaultConnection");
                    config.KeyGenerator = (secret, name) => name
                                    .Replace("__", ":");
                    config.PollingInterval = TimeSpan.FromSeconds(5);
                });
            }

            builder.Services.AddDbContext<BudgetDBContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHealthChecks();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapHealthChecks("/");

            // Configure middleware
            app.UseCors("AllowAllOrigins");

            app.UseHttpsRedirection();

            app.MapIdentityApi<ApplicationUser>();

            app.MapControllers();

            app.UseAuthorization();

            app.Run();
        }
    }
}
