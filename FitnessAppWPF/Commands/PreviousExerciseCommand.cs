using FitnessAppWPF.ViewModels;
using MVVMEssentials.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.Commands
{
    public class PreviousExerciseCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            _workoutPlayListViewModel.UpcomingExercises.Insert(0, _workoutPlayListViewModel.ActiveExercise);
            _workoutPlayListViewModel.ActiveExercise = _workoutPlayListViewModel.PreviousExercises.Last();
            _workoutPlayListViewModel.PreviousExercises.RemoveAt(_workoutPlayListViewModel.PreviousExercises.Count-1);
            DecreaseRoundNumber();
            _workoutPlayListViewModel.SetTrainingTarget();
            _workoutPlayListViewModel.CheckLists();
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter) && _workoutPlayListViewModel.PreviousExercises.Count > 0;
        }

        private readonly WorkoutPlaylistViewModel _workoutPlayListViewModel;
        public PreviousExerciseCommand(WorkoutPlaylistViewModel workoutPlaylistViewModel)
        {
            _workoutPlayListViewModel = workoutPlaylistViewModel;
            _workoutPlayListViewModel.PropertyChanged += _workoutPlayListViewModel_PropertyChanged;
        }

        private void _workoutPlayListViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_workoutPlayListViewModel.PreviousExercises))
            {
                OnCanExecuteChanged();
            }

        }
        private void DecreaseRoundNumber()
        {
            //Todo, Zähler zählt nicht richtig mit nicht countasround bei hin und zurück!
            if (_workoutPlayListViewModel.ActiveExercise.CountAsRound == true)
            {
                _workoutPlayListViewModel.ActiveRound--;
            }
        }
    }
}
