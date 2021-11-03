using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.Model
{
    public class Workout
    {

        public int Id { get; set; }
        public string Name { get; set; }


        public string Description { get; set; }


        public List<Exercise> Exercises { get; set; } = new List<Exercise>();



    }

}
