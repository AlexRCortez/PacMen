using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using PacMen.API.Hubs;
using PacMen.PL.Data;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Ui.MsSqlServerProvider;
using Serilog.Ui.Web;
using System;

using System.Reflection;
using PacMen.Api.Helpers;
using PacMen.Api.Services;
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.


        builder.Services.AddSignalR()
            .AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            });


        builder.Services.AddCors(options =>
        {
            options.AddPolicy("SignalRClientPolicy", policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:7005", "https://localhost:7005")
                    .AllowCredentials();
            });
        });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "PacMen API",
                Version = "v1",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = "PacMen",
                    Email = "PacMen@fvtc.edu",
                    Url = new Uri("https://www.fvtc.edu")
                }

            });

            var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
            c.IncludeXmlComments(xmlpath);

        });


        //string connectionString = GetSecret("PacMenConnection").Result;

        // Add Connection information
        builder.Services.AddDbContextPool<PacMenEntities>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("PacMenConnection"));
            //options.UseSqlServer(connectionString);
            options.UseLazyLoadingProxies();
        });

        // configure strongly typed settings object
        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
        // configure DI for application services
        builder.Services.AddScoped<IUserService, UserService>();


        string connection = builder.Configuration.GetConnectionString("PacMenConnection");

        builder.Services.AddSerilogUi(options =>
        {
            options.UseSqlServer(connection, "Logs");
        });

        var configSettings = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();


        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configSettings)
            .CreateLogger();

        builder.Services
            .AddLogging(c => c.AddDebug())
            .AddLogging(c => c.AddSerilog())
            .AddLogging(c => c.AddEventLog())
            .AddLogging(c => c.AddConsole());

        var app = builder.Build();

        app.UseSerilogUi(options =>
        {
            options.RoutePrefix = "logs";
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || true)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseHttpsRedirection();
        app.UseCors("SignalRClientPolicy");
        app.UseRouting();
        app.UseAuthorization();

        //app.MapControllers();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<PacMenHub>("/pacmenHub");
        });

        app.Run();
    }

    public static async Task<string> GetSecret(string secretName)
    {
        try
        {
            //const string secretName = "DVDCentral-ConnectionString";
            var keyVaultName = "kv-31590";
            var kvUri = $"https://{keyVaultName}.vault.azure.net";

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
            //using var client = GetClient();
            var secret = await client.GetSecretAsync(secretName);
            Console.WriteLine(secret.Value.Value.ToString());
            return secret.Value.Value.ToString();
            //return (await client.GetSecretAsync(kvUri, secretName)).Value.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

}