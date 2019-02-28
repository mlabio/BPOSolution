using BPOSolution.ConfigServices;
using BPOSolution.Entity;
using BPOSolution.Helper;
using BPOSolution.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace BPOSolution
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            var key = System.Text.Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value);
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.RegisterMapper();

            services.AddTransient<SmtpHelper>();

            services.AddCors();
            services.AddMvc();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IBPOClientRegistration, BPOClientRegistration>();
            services.AddScoped<ISendEmail, SendEmail>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //NOTE: Don't install Swagger using Package Manager Console. Use the Nuget Package Manager instead.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

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
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {


                //Swagger for remote desktop
                c.SwaggerEndpoint("/call-center-services/api/swagger/v1/swagger.json", "Client Landing Page");

                //Swagger for local
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "Client Landing Page");
                c.RoutePrefix = "docs/swagger";
                //Note: this must not have any slash before the routeprefix.
            });
            
            app.UseCors(x => x.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowAnyOrigin()
               .AllowCredentials()

               );

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
