﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Consul;
using S3.Common;
using S3.Common.Consul;
using S3.Common.Dispatchers;
using S3.Common.Jaeger;
using S3.Common.Mongo;
using S3.Common.Authentication;
using S3.Common.Mvc;
using S3.Common.RabbitMq;
using S3.Common.Redis;
using S3.Common.RestEase;
using S3.Common.Swagger;
using OpenTracing;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using S3.Services.Record.Utility;
using S3.Services.Record.Rules.Commands;
using S3.Services.Record.ClassSubjectScores.Commands;
using S3.Services.Record.StudentScores.Commands;
using Microsoft.Extensions.Hosting;

namespace S3.Services.Record
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        //public IContainer Container { get; private set; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            // Add DbContext using SQL Server Provider
            services.AddDbContext<RecordDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("record-service-db")));
            services.AddCustomMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>()); // Enable fluent validation;
            services.AddSwaggerDocs();
            services.AddConsul();
            services.AddJwt();
            services.AddOpenTracing();
            services.AddRedis();
            services.AddScoped<IRecordDbInitialiser, RecordDbInitialiser>();

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //var builder = new ContainerBuilder();
            //builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
            //        .AsImplementedInterfaces();
            ////builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
            ////    .AsImplementedInterfaces();
            //builder.Populate(services);
            //builder.AddRabbitMq();
            //builder.AddDispatchers();

            //Container = builder.Build();

            //return new AutofacServiceProvider(Container);
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            //builder.RegisterModule(new MyApplicationModule());

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.AddRabbitMq();
            builder.AddDispatchers();
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IHostApplicationLifetime applicationLifetime, IStartupInitializer initializer,
            IConsulClient consulClient, IRecordDbInitialiser dbInitialiser)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
                // Initialise the database
                try
                {
                    if (Configuration.GetValue<string>("database:seed").ToLowerInvariant() == "true")
                        dbInitialiser.Initialise();
                }
                catch (Exception)
                {
                    //Log.Error("Error, {Name}", ex.ToString(), LogEventLevel.Error);
                }
            }

            app.UseMvc();
            app.UseAllForwardedHeaders();
            app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseAccessTokenValidator();
            app.UseServiceId();
            app.UseRabbitMq()
               .SubscribeCommand<CreateRuleCommand>()
               .SubscribeCommand<UpdateRuleCommand>()
               .SubscribeCommand<DeleteRuleCommand>()
               .SubscribeCommand<CreateStudentScoreCommand>()
               .SubscribeCommand<UpdateStudentScoreCommand>()
               .SubscribeCommand<DeleteStudentScoreCommand>()
               //.SubscribeCommand<CreateClassSubjectScoresCommand>()
               //.SubscribeCommand<UpdateClassSubjectScoresCommand>()
               //.SubscribeCommand<DeleteClassSubjectScoresCommand>()
               ;

            var serviceId = app.UseConsul();

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(serviceId);
                //Container.Dispose();
            });

            await initializer.InitializeAsync();
        }
    }
}
