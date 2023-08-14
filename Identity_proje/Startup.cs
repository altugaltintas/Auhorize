using Identity_proje.Context;
using Identity_proje.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_proje
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
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));



            services.AddIdentity<AppUser, IdentityRole>
                (
                  
                x=>
                {

                    // burada giriþi iþlemleri / kuallnýcý defatlarý þifrelere ait detaylý istekerlimiz belireyebiyoruz. Eðer yazmsasak dateulltaki ayarlarý kabul edecektir

                    x.SignIn.RequireConfirmedPhoneNumber = false;  // giriþte doðrulanmýþ telefon gereklimi
                    x.SignIn.RequireConfirmedEmail= false; // giriþte doðrulanmý e-mail gereklimi
                    x.User.RequireUniqueEmail = true;  // eþsiz mail olsunmu kiþiye ait
                    x.Password.RequiredLength = 4;  // þifre kaç haneli olsun
                    x.Password.RequireLowercase = false; // þifre küçük harf zorunlumu
                    x.Password.RequireUppercase = false; // þifrede büyük harf zorunlumu
                    x.Password.RequireNonAlphanumeric = false; // þifrede noktalama iþaretleri zorunlumu
                    x.Password.RequireDigit = false; // þifrede rakam zorlunlumu
                    x.Password.RequiredUniqueChars = 0;  // þifrede eþsiz karakter gereklimi


                    // gibi gibi daha birçok ayarlama yapýlabilir. biz deðer atamazsak o defaulttaki deðerli kabul eder yer yer bu sebepten hatalar alýnabiliri dikatten kaçam yerler olursa


                }
               

                ).AddEntityFrameworkStores<ApplicationDBContext>()// sayesinde apUser Arka planda tutuluyor tablosuz ulaþýlmýþ oluyor
                .AddDefaultTokenProviders();
                ;
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
            }
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();  // kimlik doðrulama

            app.UseAuthorization(); // yetkilendirme 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
