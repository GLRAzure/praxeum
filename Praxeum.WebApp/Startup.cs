using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
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
using Praxeum.WebApp.Helpers;
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

            // services.AddAuthentication(AzureADB2CDefaults.AuthenticationScheme)
            //    .AddAzureADB2C(options => Configuration.Bind("AzureAdB2COptions", options));

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
                options.Authority = $"https://glrpraxeum.b2clogin.com/glrpraxeum.onmicrosoft.com/v2.0";
                options.ClientId = "f6e6ecf1-ce4e-481d-b6f4-6195a94bfc95";
                options.ClientSecret = "+9yJXfD[EB,V2IJ?Z.[bOu9l"; // OpenIdConnectProtocolException: Message contains error: 'invalid_request', error_description: 'AADB2C90079: Clients must send a client_secret when redeeming a confidential grant.
                options.RequireHttpsMetadata = false;
                options.MetadataAddress = "https://glrpraxeum.b2clogin.com/glrpraxeum.onmicrosoft.com/v2.0/.well-known/openid-configuration?p=B2C_1_Default_SignIn_SignUp";
                // options.GetClaimsFromUserInfoEndpoint = true;

                // Set response type to code
                options.ResponseType = OpenIdConnectResponseType.IdToken;

                // Configure the scope
                options.Scope.Clear();
                options.Scope.Add("openid");

                options.CallbackPath = new PathString("/signin-oidc");

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

                                // TODO: This is an ideal use case for caching

                                var profileService =
                                    new ProfileService(Configuration);

                                var profile =
                                    await profileService.GetAsync(context.Principal);

                                if (profile == null)
                                {
                                    profile =
                                        await profileService.AddAsync(context.Principal);
                                } else
                                {
                                    profile =
                                        await profileService.UpdateAsync(context.Principal, profile);
                                }

                                foreach(var role in profile.Roles.Split(','))
                                {
                                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                                }
                            }
                        }
                    }
                };
            });

            services.Configure<AzureAdB2COptions>(
                Configuration.GetSection(
                    nameof(AzureAdB2COptions)));

            services
                .AddMvc();
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

            // app.UseMvc();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
