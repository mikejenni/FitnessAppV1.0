using FitnessAppWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitnessAppWPF.Commands
{
    public class AddExerciseCmd : RelayCommandBase
    {

        public AddExerciseCmd()
        {

        }

        public override bool CanExecute(object parameter)
        {
            return false;
        }

        public override void Execute(object parameter)
        {
            AddExerciseCmdExecute();
        }

        private void AddExerciseCmdExecute()
        {
            MessageBox.Show("Neues Workout hunzufügen");
        }
    }
}
