﻿using FitnessAppWPF.Commands;
using FitnessAppWPF.Model;
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
        public MainTitleWorkoutsViewModel(INavigationService workoutbuilderNavigationService, WorkoutStore workoutStore)
        {
            _workoutStore = workoutStore;
            NavigateWorkoutBuilderCommand = new NavigateCommand(workoutbuilderNavigationService);
            CreateWorkoutCommand = new CreateWorkoutCommand(this, workoutStore);
            _workouts = new ObservableCollection<WorkoutViewModel>();

            _workouts.CollectionChanged += Workouts_CollectionChanged;

            _workoutStore.WorkoutCreated += WorkoutStore_WorkoutCreated;
        }


        private void WorkoutStore_WorkoutCreated(Workout workout)
        {
            WorkoutViewModel workoutViewModel = new WorkoutViewModel(workout);
            _workouts.Insert(0, workoutViewModel);

        }

        private void Workouts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasWorkout));
        }



    }
}
