using FitnessApp.Business.Models;
using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FitnessApp.Business.Models.Enums;

namespace FitnessAppWPF.ViewModels
{
    public class ExerciseViewModel : ViewModelBase
    {
        private readonly Exercise _exercise;
        public string Name => _exercise.Name;
        public string Description => _exercise.Description;
        public TrainingTarget TrainingTarget => _exercise.TrainingTarget;
        public int TrainingTargetValue => _exercise.TrainingTarget == TrainingTarget.Reps ? _exercise.Reps : _exercise.Time;

        public Bodypart Bodypart => _exercise.Bodypart;
        public bool CountAsRound => _exercise.CountAsRound;


        // public string TrainingTarget => _exercise.TrainingTarget == Enums.TrainingTarget.Reps ? $"{Enums.TrainingTarget.Reps} {_exercise.Reps}" : $"{Enums.TrainingTarget.Time} {_exercise.Time:HH:mm:ss}";
        public ExerciseViewModel(Exercise exercise)
        {
            _exercise = exercise;
        }

    }
}
