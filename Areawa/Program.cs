using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Configuration;
using Core.Configuration;
using Core.Database;
using Core.Shared;
using Infrastructure;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Areawa;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
            
            
        // Add services to the container.
        //builder.Services.Configure<GeneralSettings>(builder.Configuration.GetSection("GeneralSettings"));
            
        builder.Services.RegisterCoreDependencies();
        builder.Services.RegisterInfrastructureDependencies();
        builder.Services.AddTransient<IApiKeyValidator, ApiKeyValidator>();

        builder.Services.AddDbContext<AreawaDbContext>(options => options.UseSqlServer(ConfigStore.GetDbConnectionString()));

        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Areawa", Version = "v1" });
            c.OperationFilter<ApiKeyHeaderSwaggerAttribute>();
        });

        var app = builder.Build();
            
            
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Areawa v1"));
        }
        else
        {
            //?? app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //?? app.UseHsts();
                
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Areawa v1"));
        }

        app.UseHttpsRedirection();
        //app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
            
        app.ConfigureExceptionHandler(/*logger*/);
            
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}