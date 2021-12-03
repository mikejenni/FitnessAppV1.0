using FitnessAppWPF.Commands;
using FitnessApp.Business.Models;
using FitnessAppWPF.Stores;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitnessAppWPF.ViewModels
{
    public class MainTitleWorkoutsViewModel : ViewModelBase
    {
        private readonly WorkoutStore _workoutStore;
        private readonly ObservableCollection<WorkoutViewModel> _workouts;

        public IEnumerable<WorkoutViewModel> Workouts => _workouts; //{ get { return _workouts; } }
        public bool HasWorkout => _workouts.Count > 0;
        public ICommand NavigateWorkoutBuilderCommand { get; }
        public ICommand CreateWorkoutCommand { get; }
        public ICommand StartWorkoutPlaylistCommand { get; }

        // INavigationService is from Package MVVMEssentials for Navigation, W
        public MainTitleWorkoutsViewModel(INavigationService workoutplaylistNavigationService, INavigationService workoutbuilderNavigationService, WorkoutStore workoutStore)
        {
            _workoutStore = workoutStore;
            NavigateWorkoutBuilderCommand = new NavigateCommand(workoutbuilderNavigationService);
            StartWorkoutPlaylistCommand = new StartWorkoutPlaylistCommand(this, workoutplaylistNavigationService, _workoutStore);

            _workouts = new ObservableCollection<WorkoutViewModel>();
            LoadWorkoutsFromStore(workoutStore);
            _workouts.CollectionChanged += Workouts_CollectionChanged;

            _workoutStore.WorkoutSaved += WorkoutStore_WorkoutCreated;
        }


        private void WorkoutStore_WorkoutCreated(Workout workout)
        {
            AddWorkoutToList(workout);
        }

        private void LoadWorkoutsFromStore(WorkoutStore workoutStore)
        {
            foreach (Workout workout in workoutStore.Workouts)
            {
                AddWorkoutToList(workout);
            }
        }

        private void AddWorkoutToList(Workout workout)
        {
            WorkoutViewModel workoutViewModel = new WorkoutViewModel(workout); // konvertieren von workout zu workoutViewModel
            _workouts.Insert(0, workoutViewModel); // Insert an Index 0, ganz oben
        }

        private void Workouts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasWorkout));
        }

        private object _selectedItem;

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

    }
}
