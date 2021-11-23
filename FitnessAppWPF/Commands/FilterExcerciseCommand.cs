using FitnessApp.Business.Models;
using FitnessAppWPF.Stores;
using FitnessAppWPF.ViewModels;
using MVVMEssentials.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitnessAppWPF.Commands
{
    public class FilterExcerciseCommand : CommandBase
    {

        public override void Execute(object parameter)
        {
            var exercise = new ObservableCollection<ExerciseViewModel>(_exerciseMainViewModel.AllExercises);

            if (_exerciseMainViewModel.SelectedTrainingTarget == Enums.TrainingTarget.RepsOrTime && _exerciseMainViewModel.SelectedBodyPart == Enums.Bodypart.Bodypart && _exerciseMainViewModel.CountAsRound == null && string.IsNullOrWhiteSpace(_exerciseMainViewModel.SearchText))
            {
                _exerciseMainViewModel.Exercises = new ObservableCollection<ExerciseViewModel>(exercise);
                return;
            }

            if (_exerciseMainViewModel.SelectedTrainingTarget != Enums.TrainingTarget.RepsOrTime)
            {
                exercise = new ObservableCollection<ExerciseViewModel>(exercise.Where(e => e.TrainingTarget == _exerciseMainViewModel.SelectedTrainingTarget));
            }

            if (_exerciseMainViewModel.SelectedBodyPart != Enums.Bodypart.Bodypart)
            {
                exercise = new ObservableCollection<ExerciseViewModel>(exercise.Where(e => e.Bodypart == _exerciseMainViewModel.SelectedBodyPart));
            }

            if (_exerciseMainViewModel.CountAsRound != null)
            {
                exercise = new ObservableCollection<ExerciseViewModel>(exercise.Where(e => e.CountAsRound == _exerciseMainViewModel.CountAsRound));
            }

            if (string.IsNullOrWhiteSpace(_exerciseMainViewModel.SearchText) == false)
            {
                exercise = new ObservableCollection<ExerciseViewModel>(exercise.Where(e => e.Name.Contains(_exerciseMainViewModel.SearchText) || e.Description.Contains(_exerciseMainViewModel.SearchText)));
            }

            _exerciseMainViewModel.Exercises = new ObservableCollection<ExerciseViewModel>(exercise);


        }

        public FilterExcerciseCommand(ExerciseMainViewModel exerciseMainViewModel, ExerciseStore exerciseStore)
        {
            _exerciseMainViewModel = exerciseMainViewModel;


        }

        private readonly ExerciseMainViewModel _exerciseMainViewModel;






    }
}
