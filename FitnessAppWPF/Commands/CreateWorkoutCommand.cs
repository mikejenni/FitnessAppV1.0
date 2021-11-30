using FitnessApp.Business.Models;
using FitnessAppWPF.Stores;
using FitnessAppWPF.ViewModels;
using MVVMEssentials.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitnessAppWPF.Commands
{
    public class CreateWorkoutCommand : CommandBase
    {

        private readonly WorkoutBuilderViewModel _workoutBuilderViewModel;
        private readonly WorkoutStore _workoutStore;
        private bool _canExecute;


        public CreateWorkoutCommand(WorkoutBuilderViewModel workoutBuilderViewModel, WorkoutStore workoutStore)
        {
            _workoutBuilderViewModel = workoutBuilderViewModel;
            _workoutBuilderViewModel.PropertyChanged += _workoutBuilderViewModel_PropertyChanged;
            _workoutStore = workoutStore;
        }

        private void _workoutBuilderViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object parameter)
        {
            _canExecute = string.IsNullOrWhiteSpace(_workoutStore.UnsavedWorkout?.Name) == false && string.IsNullOrWhiteSpace(_workoutStore.UnsavedWorkout?.Description) == false && _workoutStore.UnsavedWorkout?.Exercises?.Count >= 1;
            return _canExecute == true && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            Workout workout = new Workout()
            {
                Name = _workoutStore.UnsavedWorkout.Name,
                Description = _workoutStore.UnsavedWorkout.Description,
                Exercises = _workoutStore.UnsavedWorkout.Exercises,

            };
            _workoutStore.SaveNewWorkout(workout);
            MessageBox.Show($"{workout.Name} wurde hinzugefügt");
            _workoutBuilderViewModel.Name = string.Empty;
            _workoutBuilderViewModel.Description = string.Empty;
            _workoutBuilderViewModel.Exercises.Clear();

        }
    }
}
