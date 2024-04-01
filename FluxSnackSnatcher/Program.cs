using FluxSnackSnatcher.Extensions;
using FluxSnackSnatcher.Settings;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json.Serialization;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingletons(builder.Configuration);

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Flux Snack Snatcher", Version = "v1" });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }

            var xmlModelsFile = "FluxSnackSnatcher.xml";
            var xmlModelsPath = Path.Combine(AppContext.BaseDirectory, xmlModelsFile);
            if (File.Exists(xmlModelsPath))
            {
                c.IncludeXmlComments(xmlModelsPath);
            }
        });

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        var appSettings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>();

        var port = 80;

        if (appSettings is not null && appSettings.Port > 0)
        {
            port = appSettings.Port;
        }

        builder.WebHost.UseUrls($"http://*:{port}");

        var app = builder.Build();

        //if (app.Environment.IsDevelopment())
        //{
        //    //app.UseAuthentication();
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseCors();

        Console.WriteLine($"----> Application listening on port {port}");

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        app.Run();

        stopwatch.Stop();
        var elapsedTime = stopwatch.Elapsed;

        Console.WriteLine($"Application runned for: {elapsedTime}");
    }
}


//using FluxSnackSnatcher.Extensions;
//using FluxSnackSnatcher.Settings;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyHeader()
//               .AllowAnyMethod();
//    });
//});

//builder.Services.AddSingletons(builder.Configuration);

//var appSettings = builder.Configuration.GetSection("Application").Get<ApiSettings>();

//var port = 80;

//if (appSettings is not null && appSettings.Port > 0)
//{
//    port = appSettings.Port;
//}

//builder.WebHost.UseUrls($"http://*:{port}");

//var app = builder.Build();

//app.UseSwagger();
//app.UseSwaggerUI();

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.UseCors();

//app.Run();
