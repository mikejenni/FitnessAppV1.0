using FitnessApp.Business.Models;
using FitnessAppWPF.Stores;
using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.ViewModels
{
    public class WorkoutPlaylistViewModel : ViewModelBase
    {
        private readonly WorkoutStore _workoutStore;

        private Workout _workout;

        public Workout Workout
        {
            get { return _workout; }
            set 
            {
                _workout = value;
                OnPropertyChanged(nameof(Workout));
            }
        }


        public WorkoutPlaylistViewModel(WorkoutStore workoutStore)
        {
            _workoutStore = workoutStore;

        }
    }
}
