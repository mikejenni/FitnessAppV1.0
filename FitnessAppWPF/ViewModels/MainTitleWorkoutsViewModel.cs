using FitnessAppWPF.Commands;
using FitnessAppWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitnessAppWPF.ViewModels
{
    public class MainTitleWorkoutsViewModel
    {
        public RelayCommandBase AddExerciseCmd { get; set; }

        public MainTitleWorkoutsViewModel()
        {
            AddExerciseCmd = new AddExerciseCmd();
        }


    }
}
