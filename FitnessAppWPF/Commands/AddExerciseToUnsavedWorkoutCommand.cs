using FitnessApp.Business.Models;
using FitnessAppWPF.ViewModels;
using MVVMEssentials.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitnessAppWPF.Commands
{
    public class AddExerciseToUnsavedWorkoutCommand : CommandBase
    {

        private readonly ExerciseMainViewModel _exerciseMainViewModel;
        public AddExerciseToUnsavedWorkoutCommand(ExerciseMainViewModel exerciseMainViewModel)
        {
            _exerciseMainViewModel = exerciseMainViewModel;
        }



        public override void Execute(object parameter)
        {
            if (_exerciseMainViewModel.UnsavedWorkout == null || _exerciseMainViewModel.SelectedItem == null)
            {
                return;
            }
            var exercise = (_exerciseMainViewModel.SelectedItem as ExerciseViewModel).Exercise;
            _exerciseMainViewModel.UnsavedWorkout.Exercises.Add(exercise);
            MessageBox.Show($"{exercise.Name} wurde hinzugefügt");
        }
    }
}
