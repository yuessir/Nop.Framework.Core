using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NopMimic.Core.Startup;
using NopMimic.Web.Framework.Infrastructure.Extensions;

namespace NopMimic.Web.Framework.Infrastructure.Startup
{
    public class NopMvcStartup : INopStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddNopMvc();
        }

        public void Configure(IApplicationBuilder application)
        {
            //MVC routing
            application.UseNopMvc();
        }

        public int Order => 1000;
    }
}
