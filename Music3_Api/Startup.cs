using Autofac;
using Elasticsearch.Net;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Music3_Api.Applications.RedisCache;
using Music3_Api.AutoMapper.Config;
using Music3_Api.Models.Email;
using Music3_Api.Paging;
using Music3_Api.SignalR;
using Music3_Core.DomainModels;
using Music3_Core.EF;
using Music3_Core.Entities;
using Music3_Core.Extension.IRepositories;
using Music3_Core.Extension.Repositories;
using Music3_Kafka.Kafka;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Music3_Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public ILifetimeScope AutofacContainer { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddMapper();
            services.AddCache(Configuration);
            //khai bao db tu appsettings
            services.AddDbContext<OnlineMusicDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("OnlineMusicDbContext")));

            services.AddMediatR(Assembly.GetExecutingAssembly());
            //setup, su dung identity server 4
            services.AddIdentity<AppUser, AppRole>(options =>
            {
            })
                .AddEntityFrameworkStores<OnlineMusicDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(2);
            });

            services.AddControllers()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidatorDomainModel>());
            services.AddHttpContextAccessor();

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MusicVer3", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                      },
                        new List<string>()
                    }
                });
            });

            //Authen
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                // Adding Jwt Bearer  
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = Configuration["JWT:ValidAudience"],
                        ValidIssuer = Configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                    };
                });

            services.AddScoped<IElasticClient, ElasticClient>(sp =>
            {
                var connectionPool = new SingleNodeConnectionPool(new Uri(Configuration.GetValue<string>("Elastic:Url")));
                var settings = new ConnectionSettings(connectionPool, (builtInSerializer, connectionSettings) =>
                    new JsonNetSerializer(builtInSerializer, connectionSettings, () => new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    })).DefaultIndex(Configuration.GetValue<string>("Elastic:Index")).DisableDirectStreaming()
                    .PrettyJson()
                    .OnRequestCompleted(apiCallDetails =>
                    {
                        var list = new List<string>();
                        // log out the request and the request body, if one exists for the type of request
                        if (apiCallDetails.RequestBodyInBytes != null)
                        {
                            Log.Information(
                                $"{apiCallDetails.HttpMethod} {apiCallDetails.Uri} " +
                                $"{Encoding.UTF8.GetString(apiCallDetails.RequestBodyInBytes)}");
                        }
                        else
                        {
                            Log.Information($"{apiCallDetails.HttpMethod} {apiCallDetails.Uri}");
                        }

                        // log out the response and the response body, if one exists for the type of response
                        if (apiCallDetails.ResponseBodyInBytes != null)
                        {
                            Log.Information($"Status: {apiCallDetails.HttpStatusCode}" +
                                        $"{Encoding.UTF8.GetString(apiCallDetails.ResponseBodyInBytes)}");
                        }
                        else
                        {
                            Log.Information($"Status: {apiCallDetails.HttpStatusCode}");
                        }
                    });
                return new ElasticClient(settings);
            });

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddSingleton<IKafKaConnection, KafKaConnection>();
            services.AddScoped(typeof(IPaginatedList<>), typeof(PaginatedList<>));
            services.AddScoped(typeof(IRepositoryEF<>), typeof(RepositoryEF<>));
            services.AddScoped<IDapperExtension, DapperExtension>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return new DapperExtension(config);

            });
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger MusicVer3"));

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<SignalRHub>("/signalr");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
