using FitnessAppWPF.Stores;
using FitnessAppWPF.ViewModels;
using MVVMEssentials.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.Commands
{
    public class CreateUnsavedWorkoutCommand : CommandBase
    {
        private readonly WorkoutBuilderViewModel _workoutBuilderViewModel;
        private readonly WorkoutStore _workoutStore;
        public CreateUnsavedWorkoutCommand(WorkoutBuilderViewModel workoutBuilderViewModel, WorkoutStore workoutStore)
        {
            _workoutBuilderViewModel = workoutBuilderViewModel;
            _workoutStore = workoutStore;

        }
        public override void Execute(object parameter)
        {
            _workoutStore.CreateUnsavedWorkout(new FitnessApp.Business.Models.Workout { Name = _workoutBuilderViewModel.Name, Description = _workoutBuilderViewModel.Description });
        }
    }
}
