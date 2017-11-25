using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model.Models;
using Microsoft.EntityFrameworkCore;

namespace WebCore01
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        void SetUPDataBase(IServiceCollection services)
        {
            //services.AddDbContext<GomoCCDBContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("GomoLocalDatabase")));
            // services.AddDbContext<BloggingContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("myHome")));
            string connstr = null;
            connstr = Configuration.GetConnectionString("BloggingDatabase");
            //log.Info("設定資料庫連線=" + connstr);
            services.AddDbContext<BloggingContext>(options =>
                      options.UseMySql(connstr));
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            SetUPDataBase(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
