using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Lab1.Data;
using Lab1.Data.Repositories;
using Lab1.Core.Interfaces;
using Lab1.Core.AppServices;
using Lab1.Core.Algorithms;
using Lab1.App.Forms; 

namespace Lab1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Настройка конфигурации
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            // 2. Сборка DI-контейнера
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("Postgres")));

            // Регистрация слоев (пути соответствуют папкам проекта)
            services.AddScoped<IExperimentRepository, ExperimentRepository>();
            services.AddScoped<ICachingService, CachingService>();
            services.AddScoped<IExperimentService, ExperimentService>();
            services.AddScoped<IApproximationService, ApproximationService>();

            // Регистрация алгоритмов
            services.AddTransient<IAlgorithm, BubbleSortAlgorithm>();
            services.AddTransient<IAlgorithm, QuickSortAlgorithm>();
            services.AddTransient<IAlgorithm, ConstantFunctionAlgorithm>();
            services.AddTransient<IAlgorithm, SumVectorAlgorithm>();
            services.AddTransient<IAlgorithm, ProductVectorAlgorithm>();
            services.AddTransient<IAlgorithm, PolynomialNaiveAlgorithm>();
            services.AddTransient<IAlgorithm, PolynomialHornerAlgorithm>();
            services.AddTransient<IAlgorithm, BuiltInSortAlgorithm>();
            services.AddTransient<IAlgorithm, MatrixMultiplicationAlgorithm>();
            services.AddTransient<IAlgorithm, PowerNaiveAlgorithm>();
            services.AddTransient<IAlgorithm, PowerRecursiveAlgorithm>();
            services.AddTransient<IAlgorithm, PowerBinaryAlgorithm>();
            services.AddTransient<IAlgorithm, RadixSortAlgorithm>();

            // Регистрация главной формы в DI
            services.AddTransient<MainForm>();
            services.AddTransient<PlotForm>();
            var serviceProvider = services.BuildServiceProvider();
            
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // Автоматически применяет все миграции и создает базу, если её нет
                dbContext.Database.Migrate(); 
            }

            // 3. Запуск приложения через DI
            var mainForm = serviceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm);
        }
    }
}