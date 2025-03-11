using HandyControl.Tools;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Windows;
using YourTask.Application.Services;
using YourTask.Application.ViewModels;
using ApiException = YourTask.Application.Services.ApiException;

namespace YourTask.Application;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var culture = new CultureInfo("pt-BR");
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        ConfigHelper.Instance.SetLang("pt-BR");

        // Configure services
        var services = new ServiceCollection();
        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();
        ServiceLocator.Initialize(serviceProvider);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Registrar o client Refit HTTP
        services.AddRefitClient<ITarefaApi>(new RefitSettings()
        {
            ExceptionFactory = async httpResponse =>
            {
                // Try to read content even if it's null
                var content = httpResponse.Content != null
                              ? await httpResponse.Content.ReadAsStringAsync()
                              : "No content returned";

                if (!httpResponse.IsSuccessStatusCode)
                    return new ApiException(httpResponse.StatusCode, content);

                return null;
            },
            ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
            {
  
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            })
        })
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("http://localhost:8080"))
            .AddHttpMessageHandler<ApiExceptionHandler>();

        services.AddTransient<ApiExceptionHandler>();

        // Register views/viewmodels
        services
            .AddTransient<MainViewModel>()
            .AddTransient<TarefaViewModel>();
    }
}

