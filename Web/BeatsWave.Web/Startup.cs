﻿namespace BeatsWave.Web
{
    using System;
    using System.Reflection;

    using BeatsWave.Data;
    using BeatsWave.Data.Common;
    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;
    using BeatsWave.Data.Repositories;
    using BeatsWave.Data.Seeding;
    using BeatsWave.Services.Data;
    using BeatsWave.Services.Data.CloudinaryWav;
    using BeatsWave.Services.Mapping;
    using BeatsWave.Services.Messaging;
    using BeatsWave.Web.Hubs;
    using BeatsWave.Web.Middlewares;
    using BeatsWave.Web.ViewModels;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSignalR(
                options =>
                    {
                        options.EnableDetailedErrors = true;
                    });

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddRazorRuntimeCompilation();

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSFR-TOKEN";
            });

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = this.configuration["Facebook:AppId"];
                facebookOptions.AppSecret = this.configuration["Facebook:AppSecret"];
            });

            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            services.AddApplicationInsightsTelemetry();
            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(x => new SendGridEmailSender(this.configuration.GetSection("SendGrid:SendGridKey").Value));
            services.AddTransient<ISettingsService, SettingsService>();

            //Image cloud
            services.AddTransient<ICloudImageService, CloudImageService>();
            services.AddTransient<IPictureInfoWriterService, PictureInfoWriterService>();
            services.AddTransient<IPictureService, PictureService>();

            services.AddTransient<IProducersService, ProducersService>();
            services.AddTransient<IBeatsService, BeatsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ILikeService, LikeService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<ICartsService, CartsService>();
            services.AddTransient<IFollowService, FollowService>();
            services.AddTransient<INotificationsService, NotificationsService>();

            //Beat cloud
            services.AddTransient<ICloudBeatService, CloudBeatService>();
            services.AddTransient<IBeatsUploadCloudService, BeatsUploadCloudService>();
            services.AddTransient<IBeatInfoWriterService, BeatInfoWriterService>();

            Account cloudinaryCredentials = new Account(
                this.configuration["Cloudinary:CloudName"],
                this.configuration["Cloudinary:ApiKey"],
                this.configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
            services.AddSingleton(cloudinaryUtility);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Home/Error");
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseResponseCompression();
            app.UseMiddleware<SetMaxRequestMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapHub<SortHub>("/sorthub");
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
