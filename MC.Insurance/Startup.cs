using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MC.Insurance.ApplicationServices;
using MC.Insurance.Domain;
using MC.Insurance.DTO;
using MC.Insurance.Infrastructure;
using MC.Insurance.Interfaces.Application;
using MC.Insurance.Interfaces.Domain;
using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
			services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
			services.Configure<SplunkConfig>(Configuration.GetSection("SplunkConfig"));

			services.AddOptions();

			services.AddCors(c =>
			{
				c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
			});

			services.AddControllers();

			services.AddSingleton<IInsuranceDomain, InsuranceDomain>();
			services.AddSingleton<ISerializer, Serializer>();
			services.AddSingleton<IInsuranceFormatInputOutput, InsuranceFormatInputOutput>();
			services.AddSingleton<IInsuranceRepository, InsuranceRepository>();
			services.AddSingleton<IInsuranceServiceResponse, InsuranceServiceResponse>();
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

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
