using Microsoft.EntityFrameworkCore;
using PacMen.PL.Data;
using PacMen.API.Hubs;


        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddSignalR();


builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRClientPolicy", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:7005/pacmenhub")
            .AllowCredentials();
    });

});



// Add session state
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

// TODO: Add the API functionality
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://bigprojectapi-300054925.azurewebsites.net/api/") });
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7081/api/") }); 



// Add connection string to the container
builder.Services.AddDbContext<PacMenEntities>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("PacMenConnection"));
            options.UseLazyLoadingProxies();
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCors("SignalRClientPolicy");
        app.UseRouting();

        app.UseSession(); // IMPORTANT : This must be before UseAuthorization

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<PacMenHub>("/pacmenHub");
        });
        app.Run();