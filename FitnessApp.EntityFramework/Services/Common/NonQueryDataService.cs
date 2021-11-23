using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using FitnessApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FitnessApp.EntityFramework;

namespace SimpleTrader.EntityFramework.Services.Common
{
    // Hier sind Methoden die dann bei den anderen DataServices genommen werden können. So müssen die Methoden nicht immer copy-paste gemacht werden. Reduziert Code.
    public class NonQueryDataService<T> where T : DomainObject
    {
        private readonly FitnessAppDbContextFactory _contextFactory;

        public NonQueryDataService(FitnessAppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity) // Diese Schreibweise stellt sicher, dass die UI nicht einfriert. Task wird auf einem anderen Kern abgearbeitet.
        {
            using (FitnessAppDbContext context = _contextFactory.CreateDbContext()) // mach das und dann wird es gelöscht, bei Datenbanken sicherstellen, 
                                                                                    // dass die Abarbeitung durchgeführt wird und dann gelöscht wird und erst dann zur nächsten.

            {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity); // So werden Freezes zu unterbunden, wird parallel oder im Hintergrund ausgeführt.
                await context.SaveChangesAsync(); //  Die Schreibweise zu EntityFramework, Methoden das es keine SQL Schreibweise braucht

                return createdResult.Entity;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (FitnessAppDbContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;

                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (FitnessAppDbContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }
    }
}
