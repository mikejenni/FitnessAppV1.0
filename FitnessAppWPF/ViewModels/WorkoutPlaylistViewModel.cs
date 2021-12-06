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

        private Exercise _activeExercise;

        public Exercise ActiveExercise
        {
            get { return _activeExercise; }
            set 
            { 
                _activeExercise = value;
                OnPropertyChanged(nameof(ActiveExercise));
            }
        }
        private int _activeRound;

        public int ActiveRound
        {
            get { return _activeRound; }
            set 
            { 
                _activeRound = value;
                OnPropertyChanged(nameof(ActiveRound));
            }
        }
        private int _totalRound;

        public int TotalRound
        {
            get { return _totalRound; }
            set 
            { 
                _totalRound = value;
                OnPropertyChanged(nameof(TotalRound));
            }
        }





        public WorkoutPlaylistViewModel(WorkoutStore workoutStore)
        {
            _workoutStore = workoutStore;
            Workout = workoutStore.WorkoutPlaylist;
            ActiveExercise = Workout.Exercises.FirstOrDefault();
            TotalRound = Workout.Exercises.Count(e => e.CountAsRound == true);
        }
    }
}
