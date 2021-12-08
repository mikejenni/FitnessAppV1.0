using FitnessApp.Business.Models;
using FitnessApp.Business.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleTrader.EntityFramework.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.EntityFramework.Services
{
    // Hier werden die Methoden die sich auf Workout beziehen konkretisiert.
    public class WorkoutDataService : IWorkoutService
    {
        private readonly FitnessAppDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Workout> _nonQueryDataService;

        public WorkoutDataService(FitnessAppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Workout>(contextFactory);
        }
        public async Task<Workout> Create(Workout entity)
        {
            using (FitnessAppDbContext context = _contextFactory.CreateDbContext()) // mach das und dann wird es gelöscht, bei Datenbanken sicherstellen, 
            {
                List<Exercise> exercises =new List<Exercise>() ;
                foreach (var exercise in entity.Exercises)
                {
                    var ex = context.Exercises.First(e => e.Id == exercise.Id);
                    exercises.Add(ex);
                }

                entity.Exercises = exercises;

                EntityEntry<Workout> createdResult = await context.Set<Workout>().AddAsync(entity); // So werden Freezes zu unterbunden, wird parallel oder im Hintergrund ausgeführt.
                await context.SaveChangesAsync(); //  Die Schreibweise zu EntityFramework, Methoden das es keine SQL Schreibweise braucht

                return createdResult.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }
        public async Task<Workout> Get(int id)
        {
            using (FitnessAppDbContext context = _contextFactory.CreateDbContext())
            {
                Workout entity = await context.Set<Workout>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Workout>> GetAll()
        {
            using (FitnessAppDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Workout> entities = await context.Set<Workout>().Include(e => e.Exercises).ToListAsync();
                return entities;
            }
        }

        public async Task<Workout> Update(int id, Workout entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}
