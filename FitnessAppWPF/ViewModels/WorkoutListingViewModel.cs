using FitnessAppWPF.Model;
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
    public class WorkoutListingViewModel : ViewModelBase
    {
        private readonly WorkoutStore _workoutStore;
        private readonly ObservableCollection<WorkoutViewModel> _workouts;

        public IEnumerable<WorkoutViewModel> Workouts => _workouts; //{ get { return _workouts; } }
        public bool HasWorkout => _workouts.Count > 0;


        public WorkoutListingViewModel(WorkoutStore workoutStore)
        {
            _workoutStore = workoutStore;

            _workouts = new ObservableCollection<WorkoutViewModel>();
            _workouts.CollectionChanged += Workouts_CollectionChanged;

            _workoutStore.WorkoutCreated += WorkoutStore_WorkoutCreated;


        }

        public static WorkoutListingViewModel LoadViewModel(WorkoutStore workoutStore)
        {
            WorkoutListingViewModel viewModel = new WorkoutListingViewModel(workoutStore);

            return viewModel;
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
