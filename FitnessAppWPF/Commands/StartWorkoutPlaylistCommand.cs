using FitnessAppWPF.Stores;
using FitnessAppWPF.ViewModels;
using FitnessAppWPF.Views;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.Commands
{
    public class StartWorkoutPlaylistCommand : CommandBase
    {
        private readonly MainTitleWorkoutsViewModel _mainTitleWorkoutsViewModel;
        private readonly INavigationService _navigationService;
        private readonly WorkoutStore _workoutStore;
        public StartWorkoutPlaylistCommand(MainTitleWorkoutsViewModel mainTitleWorkoutViewModel, INavigationService navigationService, WorkoutStore workoutStore)
        {
            _mainTitleWorkoutsViewModel = mainTitleWorkoutViewModel;
            _navigationService = navigationService;
            _workoutStore = workoutStore;


        }
        public override void Execute(object parameter)
        {
            if (_mainTitleWorkoutsViewModel.SelectedItem == null)
            {
                return;
            }
            _workoutStore.SetWorkoutPlaylist((_mainTitleWorkoutsViewModel.SelectedItem as WorkoutViewModel).Workout);
            _navigationService.Navigate();
            
        }
    }
}
