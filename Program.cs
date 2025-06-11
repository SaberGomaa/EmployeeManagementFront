namespace AribMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add HttpContextAccessor for session access
            builder.Services.AddHttpContextAccessor();

            // Register AuthHeaderHandler
            builder.Services.AddTransient<AuthHeaderHandler>();

            // Configure HttpClient for backend API
            builder.Services.AddHttpClient("BackendApi", client =>
            {
                client.BaseAddress = new Uri("http://www.backend.somee.com"); // Replace with your API base URL
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            // Configure HttpClient with AuthHeaderHandler
            builder.Services.AddHttpClient("AuthorizedClient")
                .AddHttpMessageHandler<AuthHeaderHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
