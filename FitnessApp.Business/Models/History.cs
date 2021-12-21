using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Business.Models
{
    public class History : DomainObject
    {
        public DateTime FinishedWorkoutTime { get; set; }

        public Workout FinishedWorkout { get; set; }


    }
}
