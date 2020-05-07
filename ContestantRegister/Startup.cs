using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoFilter;
using AutoMapper;
using ContestantRegister.BackgroundJobs;
using ContestantRegister.Cqrs.Features.Admin.Areas.Utils;
using ContestantRegister.Cqrs.Features.Admin.Cities.Utils;
using ContestantRegister.Cqrs.Features.Admin.CompClasses.Utils;
using ContestantRegister.Cqrs.Features.Admin.Contests.Utils;
using ContestantRegister.Cqrs.Features.Admin.Emails.Utils;
using ContestantRegister.Cqrs.Features.Admin.Institutions.Utils;
using ContestantRegister.Cqrs.Features.Admin.Regions.Utils;
using ContestantRegister.Cqrs.Features.Admin.Schools.Utils;
using ContestantRegister.Cqrs.Features.Admin.Users.Utils;
using ContestantRegister.Cqrs.Features.Frontend.Account.Utils;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Common.Utils;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Individual.Utils;
using ContestantRegister.Cqrs.Features.Frontend.Contests.Team.Utils;
using ContestantRegister.Cqrs.Features.Frontend.Home.Utils;
using ContestantRegister.Cqrs.Features.Frontend.Manage.Utils;
using ContestantRegister.DataAccess;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Framework.Cqrs;
using ContestantRegister.Infrastructure.Implementation;
using ContestantRegister.Models;
using ContestantRegister.Services.DomainServices;
using ContestantRegister.Services.DomainServices.ContestRegistration;
using ContestantRegister.Services.Extensions;
using ContestantRegister.Services.InfrastructureServices;
using ContestantRegister.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IMyUrlHelper = ContestantRegister.Services.InfrastructureServices.IUrlHelper;
using IUrlHelper = Microsoft.AspNetCore.Mvc.IUrlHelper;

namespace ContestantRegister
{
    //public class FirstTestCommandMiddleware : CommandHandlerMiddleware
    //{
    //    private readonly IEmailSender _emailSender;

    //    public FirstTestCommandMiddleware(object next, IEmailSender emailSender) : base(next)
    //    {
    //        _emailSender = emailSender;
    //    }

    //    public override Task HandleAsync(ICommand command)
    //    {
    //        return HandleNextAsync(command);
    //    }
    //}

    //public class SecondTestCommandMiddleware : CommandHandlerMiddleware
    //{
    //    private readonly IEmailSender _emailSender;

    //    public SecondTestCommandMiddleware(object next, IEmailSender emailSender) : base(next)
    //    {
    //        _emailSender = emailSender;
    //    }

    //    public override async Task HandleAsync(ICommand command)
    //    {
    //        await HandleNextAsync(command);
    //    }
    //}

    //public class FirstTestQueryMiddleware : QueryHandlerMiddleware
    //{
    //    private readonly IEmailSender _emailSender;

    //    public FirstTestQueryMiddleware(IEmailSender emailSender, object next) : base(next)
    //    {
    //        _emailSender = emailSender;
    //    }

    //    public override async Task<object> HandleAsync(IQuery<object> query)
    //    {
    //        return await HandleNextAsync(query);
    //    }
    //}

    //public class SecondTestQueryMiddleware : QueryHandlerMiddleware
    //{
    //    private readonly IEmailSender _emailSender;

    //    public SecondTestQueryMiddleware(IEmailSender emailSender, object next) : base(next)
    //    {
    //        _emailSender = emailSender;
    //    }

    //    public override Task<object> HandleAsync(IQuery<object> query)
    //    {
    //        return HandleNextAsync(query);
    //    }
    //}

    

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
            services.AddDataProtection()
                .PersistKeysToFileSystem(new System.IO.DirectoryInfo("data-protection-keys"))
                .SetApplicationName("olimp.ikit.sfu-kras.ru");

            services.AddDbContext<ApplicationDbContext>(options =>                
                options.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            ConfigureOptions(services);

            //AssemblyStatisticCalculator.Caculate();

            var assemblies = Assembly
                .GetEntryAssembly()
                .GetReferencedAssemblies()
                .Where(x => x.Name.StartsWith("ContestantRegister"))
                .Select(Assembly.Load);
            services.AddAutoMapper(assemblies);

            // Add application services.
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IContestRegistrationService, ContestRegistrationService>();

            //CQRS
            var metadata = new MiddlewareMetadata();
            //metadata.AddCommandMiddleware<FirstTestCommandMiddleware>();
            //metadata.AddCommandMiddleware<SecondTestCommandMiddleware>();
            //metadata.AddQueryMiddleware<FirstTestQueryMiddleware>();
            //metadata.AddQueryMiddleware<SecondTestQueryMiddleware>();
            services.AddSingleton(metadata);
            services.AddScoped<IHandlerDispatcher, HandlerDispatcher>();

            //Infrastructure
            services.AddScoped<IReadRepository, EfCoreReadRepository>();
            services.AddScoped<IRepository, EfCoreRepository>();
            services.AddScoped<IMyUrlHelper, MvcUrlHelper>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IEmailSender, EmailSender>();

            //TODO можно заменить на QuerableExecutor, который инжектируется во все хендлерыю но получаем еще одну зависимость и теряем привычный синтаксис экстеншн-методов
            QueryableExtensions.OrmExtensionsHider = new EfOrmExtensionsHider();

            //TODO можно изобрести какие-то модули или просто перейти на Autofac, там они уже есть
            //TODO еще можно регистрировать все каким-нибудь аццким рефлекшоном, но я не фанат таких решений :)
            //Admin services
            services.RegisterCitiesServices();
            services.RegisterAreasServices(); 
            services.RegisterCompClassesServices();
            services.RegisterInstitutionsServices();
            services.RegisterRegionsServices();
            services.RegisterSchoolsServices();
            services.RegisterEmailsServices();
            services.RegisterContestsServices();
            services.RegisterUsersServices();
            //Frontend services
            services.RegisterAccountServices();
            services.RegisterHomeServices();
            services.RegisterManageServices();
            services.RegisterCommonContestServices();
            services.RegisterTeamContestServices();
            services.RegisterIndividualContestServices();

            services.AddMvc(options => options.EnableEndpointRouting = false);

            //TODO перетащить вьюхи в папки с фичами
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new FeatureLocationExpander());
            });

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(serviceProvider =>
            {
                var actionContext = serviceProvider.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = serviceProvider.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });

            //Background jobs
            services.AddTransient<EmailJob>();
            services.AddTransient<ContestStatusJob>();

            /*
             services.AddControllersWithViews();
             */
        }

        public void ConfigureOptions(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<MailOptions>(Configuration.GetSection("SendEmail"));
            services.Configure<SuggestStudyPlaceOptions>(Configuration.GetSection("SuggestStudyPlace"));
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            var ruCultureInfo = new CultureInfo("ru-RU");
    
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(ruCultureInfo),
                SupportedCultures = new[] { ruCultureInfo },
                SupportedUICultures = new[] { ruCultureInfo }
            });

            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseBrowserLink();
                //app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            FilterPropertyAttribute.DefaultIgnoreCase = true;
            FilterPropertyAttribute.DefaultStringFilterCondition = StringFilterCondition.Contains;

            app.UseCustomExceptionHandlerMiddleware();

            context.Database.Migrate();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            /*
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
             */
        }
    }
}
