using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Business.Models
{
    public class Workout : DomainObject
    {

        public string Name { get; set; }


        public string Description { get; set; }

        public DateTime TimeStamp { get; set; }


        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
        public ICollection<History> Histories { get; set; }

        // public virtual ICollection<Exercise> Exercises { get; set; }

    }

}
