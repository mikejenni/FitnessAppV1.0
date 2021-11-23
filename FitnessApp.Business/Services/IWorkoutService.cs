using FitnessApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FitnessApp.Business.Services
{
    // Ist nur die Schnittstelle für die Übergabe. Alles was vorher <T> war ist jetzt <Workout>
    public interface IWorkoutService : IDataService<Workout>
    {

    }
}
