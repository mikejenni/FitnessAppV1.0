using FitnessApp.Business.Models;
using FitnessAppWPF.Commands;
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
using System.Windows.Input;
using static FitnessApp.Business.Models.Enums;

namespace FitnessAppWPF.ViewModels
{
    public class ExerciseMainViewModel : ViewModelBase
    {
        private readonly ExerciseStore _exerciseStore;
        private ObservableCollection<ExerciseViewModel> _exercises;

        public ObservableCollection<ExerciseViewModel> Exercises
        {
            get { return _exercises; }
            set { _exercises = value; OnPropertyChanged(nameof(Exercises)); }

        }
        private ObservableCollection<ExerciseViewModel> _allExercises;
        public ObservableCollection<ExerciseViewModel> AllExercises
        {
            get { return _allExercises; }
            set { _allExercises = value; OnPropertyChanged(nameof(AllExercises)); }

        }
        public bool HasExercise => _exercises.Count > 0;
        public ICommand NavigateExerciseBuilderCommand { get; }
        public ICommand CreateExersiceCommand { get; }
        public ICommand FilterExersiceCommand { get; }


        private TrainingTarget _selectedtrainingTarget;
        public TrainingTarget SelectedTrainingTarget
        {
            get { return _selectedtrainingTarget; }
            set
            {
                _selectedtrainingTarget = value;
                OnPropertyChanged(nameof(SelectedTrainingTarget));
                FilterExersiceCommand.Execute(null);
            }
        }

        private Bodypart _selectedbodypart;
        public Bodypart SelectedBodyPart
        {
            get { return _selectedbodypart; }
            set
            {
                _selectedbodypart = value;
                OnPropertyChanged(nameof(SelectedBodyPart));
                FilterExersiceCommand.Execute(null);
            }
        }

        public IEnumerable<Bodypart> Bodyparts
        {
            get
            {
                return Enum.GetValues(typeof(Bodypart)).Cast<Bodypart>();
            }
        }

        private bool? _countAsRound;
        public bool? CountAsRound
        {
            get { return _countAsRound; }
            set
            {
                _countAsRound = value;
                OnPropertyChanged(nameof(CountAsRound));
                FilterExersiceCommand.Execute(null);
            }
        }
        public IEnumerable<TrainingTarget> TrainingTargets
        {
            get
            {
                return Enum.GetValues(typeof(TrainingTarget)).Cast<TrainingTarget>();
            }
        }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterExersiceCommand.Execute(null);
            }
        }


        public ExerciseMainViewModel(INavigationService exercisebuilderNavigationService, ExerciseStore exerciseStore)
        {
            _exerciseStore = exerciseStore;
            NavigateExerciseBuilderCommand = new NavigateCommand(exercisebuilderNavigationService);
            FilterExersiceCommand = new FilterExcerciseCommand(this, exerciseStore);

            _exercises = new ObservableCollection<ExerciseViewModel>();
            _allExercises = new ObservableCollection<ExerciseViewModel>();
            LoadExercisesFromStore(exerciseStore);
            _exercises.CollectionChanged += Exercise_CollectionChanged;

            _exerciseStore.ExerciseCreated += ExerciseStore_ExerciseCreated;



        }

        private void ExerciseStore_ExerciseCreated(Exercise exercise)
        {
            AddExerciseToList(exercise);
        }

        private void LoadExercisesFromStore(ExerciseStore exerciseStore)
        {
            foreach (Exercise exercise in exerciseStore.Exercises)
            {
                AddExerciseToList(exercise);
            }
        }


        private void AddExerciseToList(Exercise exercise)
        {
            ExerciseViewModel exerciseViewModel = new ExerciseViewModel(exercise);
            _exercises.Insert(0, exerciseViewModel);
            _allExercises.Insert(0, exerciseViewModel);
        }

        private void Exercise_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasExercise));
        }
    }
}
