using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Basics.FakeServices;
using Core.Basics.FakeServices.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Bogus;
using Core.Basics.IServices;
using Newtonsoft.Json.Converters;

namespace Core.Basics.WebAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(options => 
            {
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
                options.SerializerSettings.Converters.Add(new StringEnumConverter(camelCaseText: true));
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            });

            services.AddSingleton<CustomerFaker>()
            .AddSingleton<ICustomersService>(x => new FakeCustomersService(x.GetService<CustomerFaker>(), Configuration.GetValue<int>("FakerCount")))
            .AddSingleton<ProductFaker>()
            .AddSingleton<IProductsService>(x => new FakeProductsService(x.GetService<ProductFaker>(), Configuration.GetValue<int>("FakerCount")))
            .AddSingleton<OrderFaker>()
            .AddSingleton<IOrdersService>(x => new FakeOrdersService(x.GetService<OrderFaker>(), Configuration.GetValue<int>("FakerCount")));

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
