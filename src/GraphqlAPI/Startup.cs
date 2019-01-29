using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphqlDemo.Data;
using GraphqlDemo.GraphQL;
using GraphqlDemo.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphqlDemo
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});


            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Use SQL server when using windows

            // services.AddDbContext<GraphqlDbContext>(options =>
            //     options.UseSqlServer(_config.GetConnectionString("GraphqlDemo")));
            
            // Use Sql lite when using Mac
            services.AddDbContext<GraphqlDbContext>(options=>
                options.UseSqlite(_config.GetConnectionString("GraphqlDemoLite")));

            services.AddScoped<ProductRepository>();
            services.AddScoped<ProductReviewRepository>();

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<GraphqlDemoSchema>();

            services.AddGraphQL(o => { o.ExposeExceptions = true; })
                    .AddGraphTypes(ServiceLifetime.Scoped)
                    .AddDataLoader();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, GraphqlDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseCookiePolicy();

            //app.UseMvc();

            app.UseGraphQL<GraphqlDemoSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            db.Seed();
        }
    }
}
