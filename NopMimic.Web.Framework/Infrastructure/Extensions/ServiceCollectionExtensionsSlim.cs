using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NopMimic.Core.Infrastructure;

namespace NopMimic.Web.Framework.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensionsSlim
    {
        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        /// <returns>Configured service provider</returns>
        public static IServiceProvider ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            ////add NopConfig configuration parameters
            //services.ConfigureStartupConfig<NopConfig>(configuration.GetSection("Nop"));
            ////add hosting configuration parameters
            //services.ConfigureStartupConfig<HostingConfig>(configuration.GetSection("Hosting"));
            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            //create, initialize and configure the engine
            var engine = EngineContext.Create();
            engine.Initialize(services);
            var serviceProvider = engine.ConfigureServices(services, configuration);

            //if (!DataSettingsManager.DatabaseIsInstalled)
            //    return serviceProvider;

            ////implement schedule tasks
            ////database is already installed, so start scheduled tasks
            //TaskManager.Instance.Initialize();
            //TaskManager.Instance.Start();

            ////log application start
            //engine.Resolve<ILogger>().Information("Application started");

            ////install plugins
            //engine.Resolve<IPluginService>().InstallPlugins();

            return serviceProvider;
        }
        /// <summary>
        /// Register HttpContextAccessor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
        /// <summary>
        /// Add and configure MVC for the application
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <returns>A builder for configuring MVC services</returns>
        public static IMvcBuilder AddNopMvc(this IServiceCollection services)
        {
            //add basic MVC feature
            var mvcBuilder = services.AddMvc();

            //we use legacy (from previous versions) routing logic
            mvcBuilder.AddMvcOptions(options => options.EnableEndpointRouting = false);

            //sets the default value of settings on MvcOptions to match the behavior of asp.net core mvc 2.2
            mvcBuilder.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //var nopConfig = services.BuildServiceProvider().GetRequiredService<NopConfig>();
            //if (nopConfig.UseSessionStateTempDataProvider)
            //{
            //    //use session-based temp data provider
            //    mvcBuilder.AddSessionStateTempDataProvider();
            //}
            //else
            //{
            //    //use cookie-based temp data provider
            //    mvcBuilder.AddCookieTempDataProvider(options =>
            //    {
            //        options.Cookie.Name = $"{NopCookieDefaults.Prefix}{NopCookieDefaults.TempDataCookie}";

            //        //whether to allow the use of cookies from SSL protected page on the other store pages which are not
            //        options.Cookie.SecurePolicy = DataSettingsManager.DatabaseIsInstalled && EngineContext.Current.Resolve<SecuritySettings>().ForceSslForAllPages
            //            ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.None;
            //    });
            //}

            ////MVC now serializes JSON with camel case names by default, use this code to avoid it
            //mvcBuilder.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            ////add custom display metadata provider
            //mvcBuilder.AddMvcOptions(options => options.ModelMetadataDetailsProviders.Add(new NopMetadataProvider()));

            ////add custom model binder provider (to the top of the provider list)
            //mvcBuilder.AddMvcOptions(options => options.ModelBinderProviders.Insert(0, new NopModelBinderProvider()));

            ////add fluent validation
            //mvcBuilder.AddFluentValidation(configuration =>
            //{
            //    configuration.ValidatorFactoryType = typeof(NopValidatorFactory);
            //    //implicit/automatic validation of child properties
            //    configuration.ImplicitlyValidateChildProperties = true;
            //});

            return mvcBuilder;
        }
    }
}
