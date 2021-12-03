using FitnessApp.Business.Models;
using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.ViewModels
{
    public class WorkoutViewModel : ViewModelBase
    {
        public readonly Workout Workout;
        public string Name => Workout.Name;
        public string Description => Workout.Description;
        public List<ExerciseViewModel> Exercises { get; set; } = new List<ExerciseViewModel>();

        public WorkoutViewModel(Workout workout)
        {
            this.Workout = workout;

        }

    }
}
