using FitnessAppWPF.ViewModels;
using MVVMEssentials.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.Commands
{
    public class NextExerciseCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            _workoutPlayListViewModel.PreviousExercises.Add(_workoutPlayListViewModel.ActiveExercise);
            _workoutPlayListViewModel.ActiveExercise = _workoutPlayListViewModel.UpcomingExercises?.First();
            _workoutPlayListViewModel.UpcomingExercises.RemoveAt(0);
            IncreaseRoundNumber();
            _workoutPlayListViewModel.SetTrainingTarget();
            _workoutPlayListViewModel.CheckLists();
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter) && _workoutPlayListViewModel.UpcomingExercises.Count > 0;
        }

        private readonly WorkoutPlaylistViewModel _workoutPlayListViewModel;
        public NextExerciseCommand(WorkoutPlaylistViewModel workoutPlaylistViewModel)
        {
            _workoutPlayListViewModel = workoutPlaylistViewModel;
            _workoutPlayListViewModel.PropertyChanged += _workoutPlayListViewModel_PropertyChanged;
        }

        private void _workoutPlayListViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_workoutPlayListViewModel.UpcomingExercises))
            {
                OnCanExecuteChanged();
            }
                
        }

        private void IncreaseRoundNumber()
        {
            if(_workoutPlayListViewModel.ActiveExercise.CountAsRound == true)
            {
                _workoutPlayListViewModel.ActiveRound++;
            }
        }
    }

    
}
