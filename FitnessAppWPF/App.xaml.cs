using FitnessAppWPF.Services;
using FitnessAppWPF.Stores;
using FitnessAppWPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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
        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<NavigationStore>();
            services.AddSingleton<ModalNavigationStore>();

            services.AddSingleton<CloseModalNavigationService>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<WorkoutStore>();

            services.AddTransient<MainTitleWorkoutsViewModel>(CreateMainTitleWorkoutsViewModel);
            services.AddTransient<WorkoutBuilderViewModel>();
            services.AddTransient<HistoryViewModel>();
            services.AddTransient<ExerciseViewModel>();
            services.AddTransient<WorkoutListingViewModel>();

            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<INavigationService>(s => CreateMainTitleWorkoutNavigationService(s));
            services.AddTransient<NavigationBarViewModel>(CreateNavigationBarViewModel);
            _serviceProvider = services.BuildServiceProvider();

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }




        private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel(
                CreateWorkoutBuilderNavigationService(serviceProvider),
                CreateMainTitleWorkoutNavigationService(serviceProvider),
                CreateHistoryNavigationService(serviceProvider),
                CreateExerciseNavigationService(serviceProvider));
        }
        private MainTitleWorkoutsViewModel CreateMainTitleWorkoutsViewModel(IServiceProvider serviceProvider)
        {
            return new MainTitleWorkoutsViewModel(
                CreateWorkoutBuilderNavigationService(serviceProvider),
                serviceProvider.GetRequiredService<WorkoutStore>());

        }

        private INavigationService CreateWorkoutBuilderNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<WorkoutBuilderViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<WorkoutBuilderViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }

        private INavigationService CreateMainTitleWorkoutNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<MainTitleWorkoutsViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<MainTitleWorkoutsViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }
        private INavigationService CreateHistoryNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<HistoryViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<HistoryViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }
        private INavigationService CreateExerciseNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<ExerciseViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<ExerciseViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }
    }
}

