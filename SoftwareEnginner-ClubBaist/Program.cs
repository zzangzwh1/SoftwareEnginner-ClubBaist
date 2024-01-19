namespace SoftwareEnginner_ClubBaist
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container Rager Page
            builder.Services.AddRazorPages();

            builder.Services.AddSession();


            var app = builder.Build();

            //Configure the HTTP request pipeline

            if (!app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseStaticFiles(); //add for wwrooot
            app.UseRouting();

            app.UseSession();


            app.MapRazorPages();
            app.Run();
        }
    }
}
