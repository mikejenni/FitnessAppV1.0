using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FitnessApp.Business.Models.Enums;

namespace FitnessApp.Business.Models
{
    public class Exercise : DomainObject
    {
        public string Name { get; set; }
        public TrainingTarget TrainingTarget { get; set; }
        public int Time { get; set; }
        public int Reps { get; set; }
        public Bodypart Bodypart { get; set; }
        public bool CountAsRound { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }
    }
}
