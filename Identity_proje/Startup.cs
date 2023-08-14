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

                    // burada giri�i i�lemleri / kualln�c� defatlar� �ifrelere ait detayl� istekerlimiz belireyebiyoruz. E�er yazmsasak dateulltaki ayarlar� kabul edecektir

                    x.SignIn.RequireConfirmedPhoneNumber = false;  // giri�te do�rulanm�� telefon gereklimi
                    x.SignIn.RequireConfirmedEmail= false; // giri�te do�rulanm� e-mail gereklimi
                    x.User.RequireUniqueEmail = true;  // e�siz mail olsunmu ki�iye ait
                    x.Password.RequiredLength = 4;  // �ifre ka� haneli olsun
                    x.Password.RequireLowercase = false; // �ifre k���k harf zorunlumu
                    x.Password.RequireUppercase = false; // �ifrede b�y�k harf zorunlumu
                    x.Password.RequireNonAlphanumeric = false; // �ifrede noktalama i�aretleri zorunlumu
                    x.Password.RequireDigit = false; // �ifrede rakam zorlunlumu
                    x.Password.RequiredUniqueChars = 0;  // �ifrede e�siz karakter gereklimi


                    // gibi gibi daha bir�ok ayarlama yap�labilir. biz de�er atamazsak o defaulttaki de�erli kabul eder yer yer bu sebepten hatalar al�nabiliri dikatten ka�am yerler olursa


                }
               

                ).AddEntityFrameworkStores<ApplicationDBContext>()// sayesinde apUser Arka planda tutuluyor tablosuz ula��lm�� oluyor
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


            app.UseAuthentication();  // kimlik do�rulama

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
