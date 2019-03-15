using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Praxeum.Data;
using Praxeum.Data.Helpers;
using Praxeum.Domain;
using Praxeum.Domain.Contests;
using Praxeum.Domain.Contests.Learners;
using Praxeum.Domain.Users;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Praxeum.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(
            IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add support services
            services.Configure<AzureCosmosDbOptions>(
                Configuration.GetSection(nameof(AzureCosmosDbOptions)));

            services.Configure<AzureTableStorageOptions>(
                Configuration.GetSection(nameof(AzureTableStorageOptions)));

            services.Configure<AzureQueueStorageEventPublisherOptions>(
                Configuration.GetSection(nameof(AzureQueueStorageEventPublisherOptions)));

            services.AddScoped<IEventPublisher, AzureQueueStorageEventPublisher>();
            services.AddScoped<IMicrosoftProfileRepository, MicrosoftProfileRepository>();
            services.AddScoped<IExperiencePointsCalculator, ExperiencePointsCalculator>();
            services.AddScoped<IContestLearnerCurrentValueUpdater, ContestLearnerCurrentValueUpdater>();
            services.AddScoped<IContestLearnerStartValueUpdater, ContestLearnerStartValueUpdater>();
            services.AddScoped<IContestLearnerTargetValueUpdater, ContestLearnerTargetValueUpdater>();

            // Add domain services
            services.AddTransient<IHandler<ContestAdd, ContestAdded>, ContestAdder>();
            services.AddTransient<IHandler<ContestCopy, ContestCopied>, ContestCopier>();
            services.AddTransient<IHandler<ContestDelete, ContestDeleted>, ContestDeleter>();
            services.AddTransient<IHandler<ContestFetch, ContestFetched>, ContestFetcher>();
            services.AddTransient<IHandler<ContestList, IEnumerable<ContestListed>>, ContestLister>();
            services.AddTransient<IHandler<ContestStart, ContestStarted>, ContestStarter>();
            services.AddTransient<IHandler<ContestUpdate, ContestUpdated>, ContestUpdater>();

            services.AddTransient<IHandler<ContestLearnerAdd, ContestLearnerAdded>, ContestLearnerAdder>();
            services.AddTransient<IHandler<ContestLearnerDelete, ContestLearnerDeleted>, ContestLearnerDeleter>();
            services.AddTransient<IHandler<ContestLearnerFetch, ContestLearnerFetched>, ContestLearnerFetcher>();
            services.AddTransient<IHandler<ContestLearnerListAdd, ContestLearnerListAdded>, ContestLearnerListAdder>();
            services.AddTransient<IHandler<ContestLearnerUpdate, ContestLearnerUpdated>, ContestLearnerUpdater>();

            services.AddTransient<IHandler<UserList, IEnumerable<UserListed>>, UserLister>();
            services.AddTransient<IHandler<UserFetchAdd, UserFetchedAdded>, UserFetcherAdder>();
            services.AddTransient<IHandler<UserFetch, UserFetched>, UserFetcher>();
            services.AddTransient<IHandler<UserUpdate, UserUpdated>, UserUpdater>();

            // Add data services
            services.AddTransient<IContestRepository, ContestRepository>();
            services.AddTransient<IContestLearnerRepository, ContestLearnerRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            // Add authentication services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect("AzureADB2C", options =>
            {
                options.Authority = Configuration.GetValue<string>("AzureADB2COptions:Authority");
                options.ClientId = Configuration.GetValue<string>("AzureADB2COptions:ClientId");
                options.ClientSecret = Configuration.GetValue<string>("AzureADB2COptions:ClientSecret");
                options.RequireHttpsMetadata = false;
                options.MetadataAddress = Configuration.GetValue<string>("AzureADB2COptions:MetadataAddress");

                // Set response type to code
                options.ResponseType = OpenIdConnectResponseType.IdToken;

                // Configure the scope
                options.Scope.Clear();
                options.Scope.Add("openid");

                //options.CallbackPath = new PathString(
                //    Configuration.GetValue<string>("AzureADB2COptions:CallbackPath"));

                // Configure the Claims Issuer to be AzureADB2C
                options.ClaimsIssuer = "AzureADB2C";

                // Set the correct name claim type
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        NameClaimType = "name"
                    };

                options.Events = new OpenIdConnectEvents
                {
                    OnAuthorizationCodeReceived = (context) =>
                    {
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = (context) =>
                    {
                        return Task.CompletedTask;
                    },
                    OnTokenResponseReceived = (context) =>
                    {
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async (context) =>
                    {
                        if (context.SecurityToken is JwtSecurityToken token)
                        {
                            if (context.Principal.Identity is ClaimsIdentity identity)
                            {
                                identity.AddClaim(new Claim("access_token", token.RawData));

                                var serviceProvider =
                                    services.BuildServiceProvider();

                                var userFetcherAdder =
                                     serviceProvider.GetService<IHandler<UserFetchAdd, UserFetchedAdded>>();

                                var userFetchAdd =
                                     new UserFetchAdd(
                                         context.Principal);

                                var userFetchAdded =
                                    await userFetcherAdder.ExecuteAsync(userFetchAdd);

                                foreach (var role in userFetchAdded.Roles.Split(','))
                                {
                                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                                }
                            }
                        }
                    }
                };
            });

            var mapperConfiguration =
                new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                    cfg.AddProfile<ContestProfile>();
                    cfg.AddProfile<ContestLearnerProfile>();
                    cfg.AddProfile<UserProfile>();
                });

            var mapper =
                 mapperConfiguration.CreateMapper();

            services.AddSingleton<IMapper>(mapper);

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseStaticFiles(
                    new StaticFileOptions()
                    {
                        FileProvider =
                            new PhysicalFileProvider(
                                Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")),
                        RequestPath = new PathString("/vendor")
                    });
            }

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
