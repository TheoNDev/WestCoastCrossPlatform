using System;
using Client.Factories;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using Client.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Client.Helpers;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<MainViewModel>();
        collection.AddSingleton<PageViewFactory>();

        collection.AddTransient<TeachersViewModel>();
        collection.AddTransient<StudentsViewModel>();
        collection.AddTransient<CoursesViewModel>();
        collection.AddTransient<ICourseService, CourseService>();
        collection.AddTransient<IStudentService, StudentService>();
        collection.AddTransient<ITeacherService, TeacherService>();

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var baseUrl = config["Settings:BaseUrl"]
            ?? throw new InvalidOperationException("BaseUrl is missing");

        var apiKey = config["Settings:ApiKey"]
            ?? throw new InvalidOperationException("ApiKey is missing");

        collection.AddHttpClient(Options.DefaultName, client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("x-apikey", apiKey);
        });

        collection.AddTransient(typeof(IHttpService<>), typeof(HttpService<>));
    }
}
