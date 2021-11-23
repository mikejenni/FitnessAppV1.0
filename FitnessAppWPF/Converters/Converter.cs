using FitnessApp.Business.Models;
using FitnessAppWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.Converters
{
    public static class Converter
    {
        public static List<ExerciseViewModel> ConvertToExerciseViewModels(this List<Exercise> exercises)
        {
            List<ExerciseViewModel> exerciseViewModels = new List<ExerciseViewModel>();
            foreach (var exercise in exercises)
            {
                exerciseViewModels.Add(new ExerciseViewModel(exercise));
            }
            return exerciseViewModels;
        }
    }
}
