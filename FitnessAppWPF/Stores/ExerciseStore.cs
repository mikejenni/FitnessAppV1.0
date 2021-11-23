using FitnessApp.Business.Models;
using FitnessApp.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.Stores
{
    public class ExerciseStore
    {
        private readonly List<Exercise> _exercises;
        private readonly IExerciseService _exerciseService;

        public IEnumerable<Exercise> Exercises => _exercises;

        public event Action<Exercise> ExerciseCreated;
        public event Action<List<Exercise>> ExerciseLoaded;

        public ExerciseStore(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
            _exercises = new List<Exercise>();
            _exercises = _exerciseService.GetAll().Result.ToList();

            ExerciseLoaded?.Invoke(_exercises);
        }

        public void CreateExercise(Exercise exercise)
        {
            _exercises.Add(exercise);
            ExerciseCreated?.Invoke(exercise);
            _exerciseService.Create(exercise);
        }
    }
}
