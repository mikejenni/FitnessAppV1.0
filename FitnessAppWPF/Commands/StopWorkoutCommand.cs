using FitnessAppWPF.ViewModels;
using MVVMEssentials.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.Commands
{
    public class StopWorkoutCommand : CommandBase
    {
        private WorkoutPlaylistViewModel _workoutPlaylistViewModel;

        public StopWorkoutCommand(WorkoutPlaylistViewModel workoutPlaylistViewModel)
        {
            _workoutPlaylistViewModel = workoutPlaylistViewModel;
        }

        public override void Execute(object parameter)
        {
            _workoutPlaylistViewModel.ElapsedTrainingTimer.Stop();
            _workoutPlaylistViewModel.StopWatchTimer.Stop();
        }
    }
}
