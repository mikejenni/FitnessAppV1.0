using FitnessApp.Business.Models;
using FitnessAppWPF.Stores;
using FitnessAppWPF.ViewModels;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.Commands
{
    public class CreateUnsavedWorkoutCommand : CommandBase
    {
        private readonly WorkoutBuilderViewModel _workoutBuilderViewModel;
        private readonly WorkoutStore _workoutStore;
        private readonly INavigationService _navigateToExerciseMain;
        public CreateUnsavedWorkoutCommand(WorkoutBuilderViewModel workoutBuilderViewModel, WorkoutStore workoutStore, INavigationService navigateToExerciseMain)
        {
            _workoutBuilderViewModel = workoutBuilderViewModel;
            _workoutBuilderViewModel.PropertyChanged += _workoutBuilderViewModel_PropertyChanged;
            _workoutStore = workoutStore;
            _navigateToExerciseMain = navigateToExerciseMain;



        }
        private void _workoutBuilderViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        private bool _canExecute;
        public override bool CanExecute(object parameter)
        {
            _canExecute = string.IsNullOrWhiteSpace(_workoutBuilderViewModel.Name) == false && string.IsNullOrWhiteSpace(_workoutBuilderViewModel.Description) == false;
            return _canExecute == true && base.CanExecute(parameter);
        }
        public override void Execute(object parameter)
        {
            if (_workoutStore.UnsavedWorkout == null)
            {
                _workoutStore.CreateUnsavedWorkout(new Workout { Name = _workoutBuilderViewModel.Name, Description = _workoutBuilderViewModel.Description });
            }
            
            _navigateToExerciseMain.Navigate();
        }
    }
}
