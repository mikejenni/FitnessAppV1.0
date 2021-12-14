using FitnessApp.Business.Models;
using FitnessAppWPF.Commands;
using FitnessAppWPF.Stores;
using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Exercise> _upcomingExercises;

        public ObservableCollection<Exercise> UpcomingExercises
        {
            get { return _upcomingExercises; }
            set 
            {
                _upcomingExercises = value;
                OnPropertyChanged(nameof(UpcomingExercises));
            }
        }
        private ObservableCollection<Exercise> _previousExercises;

        public ObservableCollection<Exercise> PreviousExercises
        {
            get { return _previousExercises; }
            set
            {
                _previousExercises = value;
                OnPropertyChanged(nameof(PreviousExercises));
            }
        }

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
        public ICommand NextExerciseCommand { get; }
        public ICommand PreviousExerciseCommand { get; }


        public Timer StopWatchTimer = new Timer { Interval = 1000, AutoReset = true };
        public Timer ActiveExerciseTimer;

        public WorkoutPlaylistViewModel(WorkoutStore workoutStore)
        {
            _workoutStore = workoutStore;
            Workout = workoutStore.WorkoutPlaylist;
            ActiveExercise = Workout.Exercises.FirstOrDefault();
            TotalRound = Workout.Exercises.Count(e => e.CountAsRound == true);
            TrainingTarget = ActiveExercise.TrainingTarget == Enums.TrainingTarget.Reps ? $"{ActiveExercise.Reps}" : TimeSpan.FromSeconds(ActiveExercise.Time).ToString(@"hh\:mm\:ss");
            StopWatchTimer.Elapsed += _stopWatchTimer_Elapsed;
            StartWorkoutCommand = new StartWorkoutCommand(this);
            ActiveExerciseTimer = ActiveExercise.TrainingTarget == Enums.TrainingTarget.Time ? new Timer { Interval = ActiveExercise.Time * 1000, AutoReset = false } : null;
            UpcomingExercises =  new ObservableCollection<Exercise>(Workout.Exercises.Skip(1));
            PreviousExercises = new ObservableCollection<Exercise>();
            NextExerciseCommand = new NextExerciseCommand(this);
            PreviousExerciseCommand = new PreviousExerciseCommand(this);
            ActiveRound = 1;

        }

        private void _stopWatchTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ElapsedTrainingTime = ElapsedTrainingTimer.Elapsed.ToString(@"hh\:mm\:ss");
            if (ActiveExercise.TrainingTarget == Enums.TrainingTarget.Time)
            {
                TrainingTarget = (TimeSpan.FromSeconds(ActiveExercise.Time) - ElapsedTrainingTimer.Elapsed).ToString(@"hh\:mm\:ss");
            }
        }
        public void CheckLists()
        {
            OnPropertyChanged(nameof(UpcomingExercises));
            OnPropertyChanged(nameof(PreviousExercises));
        }

    }
}
