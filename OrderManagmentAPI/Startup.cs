using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using OrderManagmentAPI.Model.Repository;
using OrderManagmentAPI.Repository;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
using OrderManagmentAPI.Service.Interfaces;
using OrderManagmentAPI.Service;
using Newtonsoft.Json.Serialization;
using OrderManagmentAPI.Service.Profiles;
using Newtonsoft.Json;

namespace OrderManagmentAPI
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
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;

            })

               .AddXmlDataContractSerializerFormatters().AddNewtonsoftJson(setupAction =>
               {
                   setupAction.SerializerSettings.ContractResolver =
                   new CamelCasePropertyNamesContractResolver();
               })

               .ConfigureApiBehaviorOptions(setupAction =>
                  {
                      setupAction.InvalidModelStateResponseFactory = context =>
                      {
                          var problemDetailsFactory = context.HttpContext.RequestServices
                          .GetRequiredService<ProblemDetailsFactory>();

                          var problemDetails = problemDetailsFactory.CreateValidationProblemDetails
                          (context.HttpContext, context.ModelState);

                          problemDetails.Detail = "See the errors field for details.";
                          problemDetails.Instance = context.HttpContext.Request.Path;

                          var actionExecutingContext = context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                          if (context.ModelState.ErrorCount > 0 &&
                            (context is ControllerContext ||
                             actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                          {
                              problemDetails.Type = "http://Inventory.com/modelvalidationproblem";
                              problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                              problemDetails.Detail = "one or more validation occurred.";


                              return new UnprocessableEntityObjectResult(problemDetails)
                              {
                                  ContentTypes = { "application/problem+json" }
                              };

                          };
                          problemDetails.Status = StatusCodes.Status400BadRequest;
                          problemDetails.Title = "One or more error on input occurred.";

                          return new BadRequestObjectResult(problemDetails)
                          {
                              ContentTypes = { "application/problem+json" }
                          };
                      };

                  });



            services.AddScoped<OrderContext, OrderContext>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrderProfile(provider.GetService<IClientRepository>()));
            }).CreateMapper());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened.Try again later");
                    });
                });
            }
            app.UseRouting();
         
            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
