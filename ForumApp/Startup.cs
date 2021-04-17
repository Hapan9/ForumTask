using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using BLL;
using BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using AutoMapper;

namespace PL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<Db>(o => o.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ForumDB;Trusted_Connection=True;"));

            services.AddTransient<IUserServise, UserServise>();

            services.AddTransient<ITopicService, TopicService>();

            services.AddTransient<IMessageService, MessageService>();

            services.AddTransient<IRepository<Message>, MessageRepository>();

            services.AddTransient<IRepository<Topic>, TopicRepository>();

            services.AddTransient<IRepository<User>, UserRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ForumAPI");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
