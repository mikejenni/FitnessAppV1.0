using FitnessApp.Business.Models;
using FitnessAppWPF.Stores;
using FitnessAppWPF.ViewModels;
using MVVMEssentials.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.Commands
{
    public class CreateExerciseCommand : CommandBase
    {

        private readonly ExerciseBuilderViewModel _exerciseBuilderViewModel;
        private readonly ExerciseStore _exerciseStore;
        private bool _canExecute;

        public CreateExerciseCommand(ExerciseBuilderViewModel exerciseBuilderViewModel, ExerciseStore exerciseStore)
        {
            _exerciseBuilderViewModel = exerciseBuilderViewModel;
            _exerciseBuilderViewModel.PropertyChanged += _exerciseBuilderViewModel_PropertyChanged;
            _exerciseStore = exerciseStore;
        }

        private void _exerciseBuilderViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            OnCanExecuteChanged();
        }

        public override bool CanExecute(object parameter)
        {
            _canExecute = string.IsNullOrWhiteSpace(_exerciseBuilderViewModel.Name) == false &&
                            string.IsNullOrWhiteSpace(_exerciseBuilderViewModel.Description) == false &&
                            ((_exerciseBuilderViewModel.SelectedTrainingTarget == Enums.TrainingTarget.Reps && _exerciseBuilderViewModel.Reps > 0) ||
                            (_exerciseBuilderViewModel.SelectedTrainingTarget == Enums.TrainingTarget.Time && _exerciseBuilderViewModel.Time > 0)) &&
                            _exerciseBuilderViewModel.SelectedBodyPart != Enums.Bodypart.Bodypart
                            ;
            return _canExecute == true && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            Exercise exercise = new Exercise()
            {
                Name = _exerciseBuilderViewModel.Name,
                Description = _exerciseBuilderViewModel.Description,
                Time = _exerciseBuilderViewModel.Time,
                Reps = _exerciseBuilderViewModel.Reps,
                TrainingTarget = _exerciseBuilderViewModel.SelectedTrainingTarget,
                Bodypart = _exerciseBuilderViewModel.SelectedBodyPart,
                CountAsRound = _exerciseBuilderViewModel.CountAsRound
            };
            _exerciseStore.CreateExercise(exercise);
        }

    }
}
