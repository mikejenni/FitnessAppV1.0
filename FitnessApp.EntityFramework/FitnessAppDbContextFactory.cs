using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.EntityFramework
{
    public class FitnessAppDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public FitnessAppDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public FitnessAppDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<FitnessAppDbContext> options = new DbContextOptionsBuilder<FitnessAppDbContext>();

            _configureDbContext(options);

            return new FitnessAppDbContext(options.Options);
        }
    }
}
