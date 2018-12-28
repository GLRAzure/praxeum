using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Praxeum.WebApi.Features.LeaderBoards;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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

            services.AddTransient<ILeaderBoardHandler<LeaderBoardAdd, LeaderBoardAdded>, LeaderBoardAddHandler>();
            services.AddTransient<ILeaderBoardHandler<LeaderBoardFetchById, LeaderBoardFetchedById>, LeaderBoardFetchByIdHandler>();
            services.AddTransient<ILeaderBoardHandler<LeaderBoardFetchList, IEnumerable<LeaderBoardFetchedList>>, LeaderBoardFetchListHandler>();
            services.AddTransient<ILeaderBoardHandler<LeaderBoardDeleteById, LeaderBoardDeletedById>, LeaderBoardDeleteByIdHandler>();
            services.AddTransient<ILeaderBoardHandler<LeaderBoardUpdateById, LeaderBoardUpdatedById>, LeaderBoardUpdateByIdHandler>();
    
            services.AddTransient<ILeaderBoardRepository, LeaderBoardRepository>();

            Mapper.Initialize(
                cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;


                    cfg.AddProfile<LeaderBoardProfile>();
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
