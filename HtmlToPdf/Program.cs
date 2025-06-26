using DinkToPdf.Contracts;
using DinkToPdf;
using HtmlToPdf.Helpers;
using HtmlToPdf.Services;

namespace HtmlToPdf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            var context = new CustomAssemblyLoadContext();

            var dllpath = Directory.GetCurrentDirectory() + "//Dll//libwkhtmltox.dll";

            context.LoadUnmanagedLibrary(dllpath);

            builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            builder.Services.AddScoped<PdfConverter>();
            builder.Services.AddScoped<IViewRenderService, ViewRenderService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
