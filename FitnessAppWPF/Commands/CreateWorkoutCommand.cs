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
            _canExecute = string.IsNullOrWhiteSpace(_workoutBuilderViewModel.Name) == false && string.IsNullOrWhiteSpace(_workoutBuilderViewModel.Description) == false;
            return _canExecute == true && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            Workout workout = new Workout()
            {
                Name = _workoutBuilderViewModel.Name,
                Description = _workoutBuilderViewModel.Description
            };
            _workoutStore.SaveNewWorkout(workout);
        }
    }
}
