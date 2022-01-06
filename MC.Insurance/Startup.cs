using MC.Insurance.ApplicationServices;
using MC.Insurance.Domain;
using MC.Insurance.DTO;
using MC.Insurance.Infrastructure;
using MC.Insurance.Interfaces.Application;
using MC.Insurance.Interfaces.Domain;
using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System;

namespace insurance_back_mc
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
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = Configuration["JWT:Issuer"],
						ValidAudience = Configuration["JWT:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(Configuration["JWT:ClaveSecreta"])
						)
					};
				});

			services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
			services.Configure<SplunkConfig>(Configuration.GetSection("SplunkConfig"));
			services.Configure<LdapConfig>(Configuration.GetSection("Ldap"));
			services.Configure<JWTConfig>(Configuration.GetSection("JWT"));

			services.AddOptions();

			services.AddCors(c =>
			{
				c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
			});

			services.AddControllers();
			AddSwagger(services);

			services.AddSingleton<IInsuranceRepository, InsuranceRepository>();
			services.AddSingleton<IAuthenticationService, LdapAuthentication>();
			services.AddSingleton<IInsuranceDomain, InsuranceDomain>();
			services.AddSingleton<IInsuranceManagementService, InsuranceManagementService>();
			services.AddSingleton<ISplunkLogger, SplunkLogger>();
			services.AddDbContext<InsuranceContext>(item => item.UseSqlServer(Configuration.GetConnectionString("DefaultDatabase")));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{

			app.UseCors(options => options
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader()
			);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Foo API V1");
			});

			app.UseRouting();

			app.UseAuthorization();

			app.UseMiddleware<RequestResponseMiddleware>();
			app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseAuthentication();
		}

		private void AddSwagger(IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				var groupName = "v1";

				options.SwaggerDoc(groupName, new OpenApiInfo
				{
					Title = $"Insurance {groupName}",
					Version = groupName,
					Description = "Insurance API",
					Contact = new OpenApiContact
					{
						Name = "Insurance Company",
						Email = string.Empty,
						Url = new Uri("https://foo.com/"),
					}
				});
			});
		}
	}
}
