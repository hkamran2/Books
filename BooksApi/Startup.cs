using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.Context;
using BooksApi.Filters.ResultFilters;
using BooksApi.Profiles;
using BooksApi.Repository;
using BooksApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BooksApi
{
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            /** ---------------- ED DbContext Core Config -------------------- */
            var connectionString = Configuration["ConnectionStrings:BooksDBConnectionString"];
            services.AddDbContext<BooksContext>(db => db.UseSqlServer(connectionString));
            
            /** ---------------- AutoMapper -------------------- */
            services.AddAutoMapper(typeof(Startup));
        
            /** ---------------- Services ---------------------- */
            //Unit of work that contains the db context 
            services.AddTransient<IUnitofWork, UnitofWork>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IAuthorService, AuthorService>();

            /** ---------------- Action Result Filters ---------------------- */
            services.AddSingleton<BooksCollectionResult>();
            services.AddSingleton<BooksResult>();
            services.AddSingleton<AuthorResult>();
            services.AddSingleton<AuthorsCollectionResult>();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
