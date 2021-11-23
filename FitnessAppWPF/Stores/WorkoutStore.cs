using FitnessApp.Business.Models;
using FitnessApp.Business.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.Stores
{
    // Alle Zustände rundum Workouts, wird informieren über die Liste von IEnumerable, was enthalten ist und geladen wird. Das ist die eine Quelle für Datenbank, Userinterface und Events.
    // Ist auch bei der App.xaml.cs als Singleton hinterlegt
    public class WorkoutStore
    {
        private readonly List<Workout> _workouts;
        private readonly IWorkoutService _workoutService;
        public Workout UnsavedWorkout { get; private set; }
        // Die eine Liste mit Workouts 
        public IEnumerable<Workout> Workouts => _workouts;

        public event Action<Workout> WorkoutSaved;
        public event Action<List<Workout>> WorkoutsLoaded;
        public event Action<Workout> WorkoutCreated;

        // Konstruktor braucht IWoorkoutService, ist die Anbindung zur Datenbank. So kann es mit Datenbank sprechen.
        public WorkoutStore(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
            _workouts = _workoutService.GetAll().Result.ToList(); // TODO: umbauen


            WorkoutsLoaded?.Invoke(_workouts);




        }
        // Garantie, dass Datenbank und Liste auf gleichem stand sind
        public void SaveNewWorkout(Workout workout)
        {
            _workouts?.Add(workout);
            WorkoutSaved?.Invoke(workout);
            _workoutService.Create(workout);
            UnsavedWorkout = null;
        }
        public void SaveWorkout(Workout workout)
        {
            _workouts?.RemoveAll(w => w.Id == workout.Id);
            _workouts?.Add(workout);
            WorkoutSaved?.Invoke(workout);
            _workoutService.Update(workout.Id, workout);

        }
        public void CreateUnsavedWorkout(Workout workout)
        {
            UnsavedWorkout = workout;
            WorkoutCreated?.Invoke(workout);
        }

    }
}
