using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MC.Insurance.ApplicationServices;
using MC.Insurance.Domain;
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
			services.AddControllers();

			IInsuranceManagementService insuranceManagementService;
			IInsuranceDomain InsuranceDomain = new InsuranceDomain();
			ISerializer Serializer = new Serializer();
			IInsuranceFormatInputOutput InsuranceFormatInputOutput = new InsuranceFormatInputOutput(InsuranceDomain, Serializer);
			IInsuranceRepository InsuranceRepository = new InsuranceRepository();

			IInsuranceServiceResponse InsuranceServiceResponse = new InsuranceServiceResponse(InsuranceRepository);

			insuranceManagementService = new InsuranceManagementService(
				InsuranceDomain,
				InsuranceFormatInputOutput,
				InsuranceServiceResponse,
				Serializer
			);

			services.AddSingleton<IInsuranceManagementService>(insuranceManagementService);
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

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}