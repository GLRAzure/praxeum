using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Praxeum.Data.Helpers;
using Praxeum.WebApi.Features.LeaderBoards;
using Praxeum.WebApi.Features.LeaderBoards.Learners;
using Praxeum.WebApi.Features.Learners;
using Praxeum.WebApi.Helpers;
using Swashbuckle.AspNetCore.Swagger;

namespace Praxeum.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // https://github.com/aspnet/AspNetCore/tree/bfec2c14be1e65f7dd361a43950d4c848ad0cd35/src/Azure/AzureAD
        // http://bitoftech.net/2016/08/31/integrate-azure-ad-b2c-asp-net-mvc-web-app/

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Praxeum API", Version = "v1" });

                c.EnableAnnotations();

                c.SchemaFilter<SwaggerExcludeSchemaFilter>();
                c.DocumentFilter<SwaggerTagDescriptionsDocumentFilter>(); // Enforces sort order and includes descriptions

                c.IgnoreObsoleteActions();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });

            services.Configure<AzureCosmosDbOptions>(
                Configuration.GetSection(nameof(AzureCosmosDbOptions)));

            services.Configure<AzureQueueStorageEventPublisherOptions>(
                Configuration.GetSection(nameof(AzureQueueStorageEventPublisherOptions)));

            services.Configure<LearnerOptions>(
                Configuration.GetSection(nameof(LearnerOptions)));

            services.AddScoped<IEventPublisher, AzureQueueStorageEventPublisher>();

            services.UseLeaderBoardServices();
            services.UseLeaderBoardLearnerServices();
            services.UseLearnerServices();

            Mapper.Initialize(
                cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                    cfg.AddProfile<LeaderBoardProfile>();
                    cfg.AddProfile<LeaderBoardLearnerProfile>();
                    cfg.AddProfile<LearnerProfile>();
                });

            Mapper.AssertConfigurationIsValid();
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

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Praxeum Api Explorer";

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Praxeum V1");

                c.InjectStylesheet("/swagger-ui/custom.css");

                c.EnableFilter();
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
