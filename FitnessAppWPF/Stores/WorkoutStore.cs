using FitnessAppWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.Stores
{
    public class WorkoutStore
    {
        private readonly List<Workout> _workouts;

        public IEnumerable<Workout> Workouts => _workouts;

        public event Action<Workout> WorkoutCreated;
        public WorkoutStore()
        {
            _workouts = new List<Workout>();


        }

        public void CreateWorkout(Workout workout)
        {
            _workouts?.Add(workout);
            WorkoutCreated?.Invoke(workout);
            // ist das gleiche wie:
            //if (WorkoutCreated != null)
            //{
            //    WorkoutCreated.Invoke(workout);
            //}

        }

    }
}
