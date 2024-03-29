﻿using ContosoUniversity.Data;
using ContosoUniversity.Repositories;
using ContosoUniversity.Repositories.Implements;
using ContosoUniversity.Services;
using ContosoUniversity.Services.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System;

namespace ContosoUniversity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // conexion a base de datos

            services.AddDbContext<SchoolContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("SchoolContext")));

            //Inyeccion de dependencias
            
            //Reposiories
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IEnrollmetnRepository, EnrollmentRepository>();
            services.AddScoped<IInstructorRepository, InstructorRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IOfficeAssignmentRepository, OfficeAssignmentRepository>();

            //Services

            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IInstructorService, InstructorService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IOfficeAssignmentService, OfficeAssignmentService>();

            //AutoMapper

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
