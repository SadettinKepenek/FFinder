using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FFinder.BLL.Abstract;
using FFinder.BLL.Concrete;
using FFinder.Core.Authentication;
using FFinder.Core.DAL;
using FFinder.DAL.Abstract;
using FFinder.DAL.Concrete.EntityFramework;
using FFinder.Entity.Concrete;
using FFinder.MappingProfiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace FFinder
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }); ;

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BaseMappingProfile());
                mc.AddProfile(new CommentMappingProfile());
                mc.AddProfile(new CommentRateMappingProfile());
                mc.AddProfile(new FollowerMappingProfile());
                mc.AddProfile(new PostMappingProfile());
                mc.AddProfile(new PostRateMappingProfile());
                mc.AddProfile(new UserMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            #region Injections

            services.AddSingleton<ICommentDal, EfCommentRepository>();
            services.AddSingleton<ICommentRateDal, EfCommentRateRepository>();
            services.AddSingleton<IFollowerRepository, EfFollowerRepository>();
            services.AddSingleton<IPostRateRepository, EfPostRateRepository>();
            services.AddSingleton<IPostRepository, EfPostRepository>();

            services.AddSingleton<IAuthService, AuthManager>();
            services.AddSingleton<ICommentRateService, CommentRateManager>();
            services.AddSingleton<ICommentService,CommentManager >();
            services.AddSingleton<IFollowerService, FollowerManager>();
            services.AddSingleton<IPostRateService, PostRateManager>();
            services.AddSingleton<IPostService, PostManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            #endregion




            services.AddDbContext<SqlDbContext>(opt =>
            {
                opt.UseSqlServer(
                    Configuration.GetConnectionString("FFinder"), b => b.MigrationsAssembly("FFinder"));
            });

            services.AddIdentity<AuthIdentityUser, AuthIdentityRole>(options => { })
                .AddEntityFrameworkStores<SqlDbContext>().AddDefaultTokenProviders();

            var key = Encoding.ASCII.GetBytes(Core.Authentication.TokenBase.SecretKey);


            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.RefreshOnIssuerKeyNotFound = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,

                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "api/{controller=Users}/{action=Get}/{id?}");
            });
        }
    }
}
