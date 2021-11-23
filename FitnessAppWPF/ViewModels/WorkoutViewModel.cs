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
        private readonly Workout _workout;
        public string Name => _workout.Name;
        public string Description => _workout.Description;
        public List<ExerciseViewModel> Exercises { get; set; } = new List<ExerciseViewModel>();

        public WorkoutViewModel(Workout workout)
        {
            _workout = workout;

        }

    }
}
