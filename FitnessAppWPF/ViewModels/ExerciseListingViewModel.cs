using FitnessApp.Business.Models;
using FitnessAppWPF.Stores;
using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.ViewModels
{
    public class ExerciseListingViewModel : ViewModelBase
    {
        private readonly ExerciseStore _exerciseStore;
        private readonly ObservableCollection<ExerciseViewModel> _exercises;

        public IEnumerable<ExerciseViewModel> Exercises => _exercises;

        public bool HasExercise => _exercises.Count > 0;

        public ExerciseListingViewModel(ExerciseStore exerciseStore)
        {
            _exerciseStore = exerciseStore;

            _exercises = new ObservableCollection<ExerciseViewModel>();
            _exercises.CollectionChanged += Exercises_CollectionChanged;
            _exerciseStore.ExerciseCreated += ExerciseStore_ExerciseCreated;

        }

        public static ExerciseListingViewModel ListingViewModel(ExerciseStore exerciseStore)
        {
            ExerciseListingViewModel viewModel = new ExerciseListingViewModel(exerciseStore);

            return viewModel;
        }

        private void ExerciseStore_ExerciseCreated(Exercise exercise)
        {
            ExerciseViewModel exerciseViewModel = new ExerciseViewModel(exercise);
            _exercises.Insert(0, exerciseViewModel);
        }

        private void Exercises_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasExercise));
        }
    }
}
