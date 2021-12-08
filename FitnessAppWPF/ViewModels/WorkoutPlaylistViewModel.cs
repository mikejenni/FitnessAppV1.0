using FitnessApp.Business.Models;
using FitnessAppWPF.Commands;
using FitnessAppWPF.Stores;
using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

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

        private string _trainingTarget;

        public string TrainingTarget
        {
            get { return _trainingTarget; }
            set 
            { 
                _trainingTarget = value;
                OnPropertyChanged(nameof(TrainingTarget));
            }
        }

        private string _elapsedTrainingTime = "00:00:00";

        public string ElapsedTrainingTime
        {
            get { return _elapsedTrainingTime; }
            set 
            { 
                _elapsedTrainingTime = value;
                OnPropertyChanged(nameof(ElapsedTrainingTime));
            }
        }

        public Stopwatch ElapsedTrainingTimer { get; set; }
        public ICommand StartWorkoutCommand { get; }
        public Timer StopWatchTimer = new Timer { Interval = 1000, AutoReset = true };
        public WorkoutPlaylistViewModel(WorkoutStore workoutStore)
        {
            _workoutStore = workoutStore;
            Workout = workoutStore.WorkoutPlaylist;
            ActiveExercise = Workout.Exercises.FirstOrDefault();
            TotalRound = Workout.Exercises.Count(e => e.CountAsRound == true);
            TrainingTarget = ActiveExercise.TrainingTarget == Enums.TrainingTarget.Reps ? $"{ActiveExercise.Reps}" : TimeSpan.FromSeconds(ActiveExercise.Time).ToString(@"hh\:mm\:ss");
            StopWatchTimer.Elapsed += _stopWatchTimer_Elapsed;
            StartWorkoutCommand = new StartWorkoutCommand(this);
        }

        private void _stopWatchTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ElapsedTrainingTime = ElapsedTrainingTimer.Elapsed.ToString(@"hh\:mm\:ss");
        }
    }
}
