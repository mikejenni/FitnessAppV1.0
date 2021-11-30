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
        public readonly Exercise Exercise;
        public string Name => Exercise.Name;
        public string Description => Exercise.Description;
        public TrainingTarget TrainingTarget => Exercise.TrainingTarget;
        public int TrainingTargetValue => Exercise.TrainingTarget == TrainingTarget.Reps ? Exercise.Reps : Exercise.Time;

        public Bodypart Bodypart => Exercise.Bodypart;
        public bool CountAsRound => Exercise.CountAsRound;


        // public string TrainingTarget => _exercise.TrainingTarget == Enums.TrainingTarget.Reps ? $"{Enums.TrainingTarget.Reps} {_exercise.Reps}" : $"{Enums.TrainingTarget.Time} {_exercise.Time:HH:mm:ss}";
        public ExerciseViewModel(Exercise exercise)
        {
            Exercise = exercise;
        }

    }
}
