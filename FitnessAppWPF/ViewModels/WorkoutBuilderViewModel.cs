using FitnessAppWPF.Commands;
using FitnessAppWPF.Converters;
using FitnessAppWPF.Stores;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitnessAppWPF.ViewModels
{
    public class WorkoutBuilderViewModel : ViewModelBase
    {

        public WorkoutBuilderViewModel(WorkoutStore workoutstore, INavigationService navigateToExerciseMain )
        {
            CreateUnsavedWorkoutCommand = new CreateUnsavedWorkoutCommand(this, workoutstore, navigateToExerciseMain);
            CreateWorkoutCommand = new CreateWorkoutCommand(this, workoutstore);
            Name = workoutstore.UnsavedWorkout?.Name;
            Description = workoutstore.UnsavedWorkout?.Description;
            Exercises = workoutstore.UnsavedWorkout?.Exercises == null ? new ObservableCollection<ExerciseViewModel>() : new ObservableCollection<ExerciseViewModel>(workoutstore.UnsavedWorkout?.Exercises?.ConvertToExerciseViewModels());
        }

        public ICommand CreateWorkoutCommand { get; }
        public ICommand CreateUnsavedWorkoutCommand { get; }

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

        private ObservableCollection<ExerciseViewModel> _exercises;
        public ObservableCollection<ExerciseViewModel> Exercises
        {
            get { return _exercises; }
            set { _exercises = value; OnPropertyChanged(nameof(Exercises)); }

        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

    }
}
