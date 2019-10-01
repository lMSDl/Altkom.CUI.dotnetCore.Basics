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
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Core.Basics.Models;
using Core.Basics.WebAPI.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Basics.WebAPI.Services;
using Microsoft.OpenApi.Models;

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
            })
            .AddXmlSerializerFormatters();

            services.AddSingleton<CustomerFaker>()
            .AddSingleton<ICustomersService>(x => new FakeCustomersService(x.GetService<CustomerFaker>(), Configuration.GetValue<int>("FakerCount")))
            .AddSingleton<ProductFaker>()
            .AddSingleton<IProductsService>(x => new FakeProductsService(x.GetService<ProductFaker>(), Configuration.GetValue<int>("FakerCount")))
            .AddSingleton<OrderFaker>()
            .AddSingleton<IOrdersService>(x => new FakeOrdersService(x.GetService<OrderFaker>(), Configuration.GetValue<int>("FakerCount")));

            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(AuthenticateService.Key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"}));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseMiddleware<ExceptionMiddleware>();
                /*/app.UseExceptionHandler(appError =>
                {
                    appError.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if(contextFeature != null)
                        {
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetails {
                                StatusCode = context.Response.StatusCode,
                                Message = contextFeature.Error.Message
                            }));
                        }
                    });
                });*/
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(x => {x.SwaggerEndpoint("../swagger/v1/swagger.json", "My Api v1");
            });
            app.UseMvc();
        }
    }
}
