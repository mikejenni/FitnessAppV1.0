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
    public class HistoryDataService : IHistoryService
    {
        private readonly FitnessAppDbContextFactory _contextFactory;
        private readonly NonQueryDataService<History> _nonQueryDataService;

        public HistoryDataService(FitnessAppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<History>(contextFactory);
        }
        public async Task<History> Create(History entity)
        {
            using (FitnessAppDbContext context = _contextFactory.CreateDbContext()) // mach das und dann wird es gelöscht, bei Datenbanken sicherstellen, 
            {
                var workout = context.Workouts.First(w => w.Id == entity.FinishedWorkout.Id);
                entity.FinishedWorkout = workout;

                EntityEntry<History> createdResult = await context.Set<History>().AddAsync(entity); // So werden Freezes zu unterbunden, wird parallel oder im Hintergrund ausgeführt.
                await context.SaveChangesAsync(); //  Die Schreibweise zu EntityFramework, Methoden das es keine SQL Schreibweise braucht

                return createdResult.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }
        public async Task<History> Get(int id)
        {
            using (FitnessAppDbContext context = _contextFactory.CreateDbContext())
            {
                History entity = await context.Set<History>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<History>> GetAll()
        {
            using (FitnessAppDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<History> entities = await context.Set<History>().ToListAsync();
                return entities;
            }
        }

        public async Task<History> Update(int id, History entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}
