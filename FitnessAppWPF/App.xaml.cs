using FitnessApp.Business.Services;
using FitnessApp.EntityFramework;
using FitnessApp.EntityFramework.Services;
using FitnessAppWPF.Services;
using FitnessAppWPF.Stores;
using FitnessAppWPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FitnessAppWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHost _host;
        public App()
        {
            _host = CreateHostBuilder().Build();

            // IServiceCollection services = new ServiceCollection();

            //_serviceProvider = services.BuildServiceProvider();

        }
        private static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args).ConfigureAppConfiguration(c =>
            {
                c.AddJsonFile("appsettings.json");
                c.AddEnvironmentVariables();
            }).ConfigureServices((context, services) => {

                string connectionString = context.Configuration.GetConnectionString("sqlite");
                Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlite(connectionString);

                services.AddDbContext<FitnessAppDbContext>(configureDbContext);
                services.AddSingleton<FitnessAppDbContextFactory>(new FitnessAppDbContextFactory(configureDbContext));

                services.AddSingleton<IExerciseService, ExerciseDataService>();
                services.AddSingleton<IWorkoutService, WorkoutDataService>();
                services.AddSingleton<IHistoryService, HistoryDataService>();

                services.AddSingleton<NavigationStore>();
                services.AddSingleton<ModalNavigationStore>();

                services.AddSingleton<CloseModalNavigationService>();
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<WorkoutStore>();
                services.AddSingleton<ExerciseStore>();
                services.AddSingleton<HistoryStore>();

                services.AddTransient<MainTitleWorkoutsViewModel>(CreateMainTitleWorkoutsViewModel);
                services.AddTransient<ExerciseMainViewModel>(CreateMainTitleExercisesViewModel);
                services.AddTransient<WorkoutBuilderViewModel>(CreateWorkoutBuilderViewModel);
                services.AddTransient<ExerciseBuilderViewModel>();
                services.AddTransient<HistoryViewModel>();
                services.AddTransient<WorkoutPlaylistViewModel>();
                //services.AddTransient<ExerciseViewModel>();
                services.AddTransient<WorkoutListingViewModel>();




                services.AddSingleton<MainWindow>(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainViewModel>()
                });
                services.AddSingleton<INavigationService>(s => CreateMainTitleWorkoutNavigationService(s));
                services.AddTransient<NavigationBarViewModel>(CreateNavigationBarViewModel);



            });
        }



        protected override void OnStartup(StartupEventArgs e)
        {

            _host.Start();

            FitnessAppDbContextFactory contextFactory = _host.Services.GetRequiredService<FitnessAppDbContextFactory>();
            using (FitnessAppDbContext context = contextFactory.CreateDbContext())
            {
                context.Database.Migrate();
            }

            INavigationService initialNavigationService = _host.Services.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }




        private static NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel(
                CreateWorkoutBuilderNavigationService(serviceProvider),
                CreateMainTitleWorkoutNavigationService(serviceProvider),
                CreateHistoryNavigationService(serviceProvider),
                CreateExerciseNavigationService(serviceProvider),
                CreateExercisetBuilderNavigationService(serviceProvider));
        }
        private static MainTitleWorkoutsViewModel CreateMainTitleWorkoutsViewModel(IServiceProvider serviceProvider)
        {
            return new MainTitleWorkoutsViewModel(
                CreateWorkoutPlaylistNavigationService(serviceProvider),
                CreateWorkoutBuilderNavigationService(serviceProvider),
                serviceProvider.GetRequiredService<WorkoutStore>());

        }
        private static WorkoutBuilderViewModel CreateWorkoutBuilderViewModel(IServiceProvider serviceProvider)
        {
            INavigationService exerciseManNavigation = CreateExerciseNavigationService(serviceProvider);

            return new WorkoutBuilderViewModel(serviceProvider.GetRequiredService<WorkoutStore>(), exerciseManNavigation);

        }
        private static ExerciseMainViewModel CreateMainTitleExercisesViewModel(IServiceProvider serviceProvider)
        {
            return new ExerciseMainViewModel(
                CreateExercisetBuilderNavigationService(serviceProvider),
                serviceProvider.GetRequiredService<ExerciseStore>(),
                serviceProvider.GetRequiredService<WorkoutStore>());
        }

        private static INavigationService CreateExercisetBuilderNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<ExerciseBuilderViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<ExerciseBuilderViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }
        private static INavigationService CreateWorkoutPlaylistNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<WorkoutPlaylistViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<WorkoutPlaylistViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }
        private static INavigationService CreateWorkoutBuilderNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<WorkoutBuilderViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<WorkoutBuilderViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }

        private static INavigationService CreateMainTitleWorkoutNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<MainTitleWorkoutsViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<MainTitleWorkoutsViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }
        private static INavigationService CreateHistoryNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<HistoryViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<HistoryViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }
        private static INavigationService CreateExerciseNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<ExerciseMainViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<ExerciseMainViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }



    }
}

