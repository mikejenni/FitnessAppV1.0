using FitnessAppWPF.Model;
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
    public class CreateWorkoutCommand : CommandBase
    {

        private readonly MainTitleWorkoutsViewModel _mainTitleWorkoutsViewModel;
        private readonly WorkoutStore _workoutStore;


        public CreateWorkoutCommand(MainTitleWorkoutsViewModel mainTitleWorkoutsViewModel, WorkoutStore workoutStore)
        {
            _mainTitleWorkoutsViewModel = mainTitleWorkoutsViewModel;
            _workoutStore = workoutStore;
        }

        public override void Execute(object parameter)
        {
            Workout workout = new Workout()
            {
                Name = "Squats",
                Description = "Abhocken"
            };
            _workoutStore.CreateWorkout(workout);
        }
    }
}
