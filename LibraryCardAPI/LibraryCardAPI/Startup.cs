using LibraryCardAPI.Repository;
using LibraryCardAPI.Repository.Context;
using LibraryCardAPI.Repository.Generic;
using LibraryCardAPI.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using LibraryCardAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace LibraryCardAPI
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

            //Identity Core
            IdentityBuilder builder = services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            });

            // Identity Builder
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole<int>), builder.Services);
            builder.AddEntityFrameworkStores<LibraryCardContext>();
            builder.AddRoleValidator<RoleValidator<IdentityRole<int>>>();
            builder.AddRoleManager<RoleManager< IdentityRole<int>>> ();
            builder.AddSignInManager<SignInManager<User>>();

            // Bearer Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            // Allow requests

            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            })
            );

            services.AddDbContext<LibraryCardContext>(options => options.UseMySql(Configuration.GetConnectionString("MySQLConnection"),
                mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new Version(5, 7, 22), ServerType.MySql)
                    .EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                }));
            services.AddControllers();

         

            //Auto Mapper
            services.AddAutoMapper(typeof(Startup));

            //Injection Independency
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();

            services.AddScoped(typeof(IRepository), typeof(GenericRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IStudentRepository), typeof(StudentRepository));

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Library Card API",
                        Version = "v1",
                        Description = "Library Card API for generation students cards",
                        Contact = new OpenApiContact
                        {
                            Name = "Allison Sousa Bahls",
                            Url = new Uri("https://github.com/AllisonSBahls")
                        }
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            //Static files
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            // Swagger Json documentation
            app.UseSwagger();

            //Allow request front end

            // Generate Page Swagger HTML
            app.UseSwaggerUI(c=> {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Card API - v1");
                });

            //Swagger Page redirect
            var option = new RewriteOptions();
            option.AddRedirect("^&", "swagger");
            app.UseRewriter(option);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
