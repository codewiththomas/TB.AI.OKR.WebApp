using TB.OpenAI.ApiClient;
using TB.GPT4All.ApiClient;
using TB.AI.OKR.WebApp.Persistence.Contexts;
using Microsoft.Fast.Components.FluentUI;
using TB.AI.OKR.WebApp.Persistence.Repositories;
using Microsoft.OpenApi.Models;
using TB.Tools.Readability;

namespace TB.AI.OKR.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Add services to the container.

        #region API support 
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });
        #endregion

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        
        /* Include the self-written ApiClient */
        builder.Services.AddOpenAiApiClient(builder.Configuration);
        builder.Services.AddGPT4AllApiClient(builder.Configuration);
        builder.Services.AddReadabilityService();

        /* Add persistence layer */
        builder.Services.AddDbContext<ApplicationDbContext>();

        /* Add Fluent UI */
        builder.Services.AddHttpClient();
        builder.Services.AddFluentUIComponents(options =>
        {
            options.HostingModel = BlazorHostingModel.Server;
        });

        /* Add persistence layer */
        builder.Services.AddScoped<ILabelRepository, LabelRepository>();
        builder.Services.AddScoped<IOkrRuleRepository, OkrRuleRepository>();
        builder.Services.AddScoped<IOkrSetRepository, OkrSetRepository>();
        builder.Services.AddScoped<IReferenceSourceRepository, ReferenceSourceRepository>();        

        #endregion

        var app = builder.Build();

        #region Configure the HTTP request pipeline (Order matters!). 

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }



        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        #region API support
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
        app.MapControllers(); //needed for API-Support
        #endregion

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        #endregion

        app.Run();
    }
}