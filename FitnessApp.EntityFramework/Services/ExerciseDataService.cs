using FitnessApp.Business.Models;
using FitnessApp.Business.Services;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.EntityFramework.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.EntityFramework.Services
{
    // Hier werden die Methoden die sich auf Exercise beziehen konkretisiert.
    public class ExerciseDataService : IExerciseService
    {
        private readonly FitnessAppDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Exercise> _nonQueryDataService;

        public ExerciseDataService(FitnessAppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Exercise>(contextFactory);
        }
        public async Task<Exercise> Create(Exercise entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }
        public async Task<Exercise> Get(int id)
        {
            using (FitnessAppDbContext context = _contextFactory.CreateDbContext())
            {
                Exercise entity = await context.Set<Exercise>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Exercise>> GetAll()
        {
            using (FitnessAppDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Exercise> entities = await context.Set<Exercise>().ToListAsync();
                return entities;
            }
        }

        public async Task<Exercise> Update(int id, Exercise entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}
