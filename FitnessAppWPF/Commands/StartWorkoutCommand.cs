using FitnessAppWPF.ViewModels;
using MVVMEssentials.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.Commands
{
    public class StartWorkoutCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            if (_workoutPlaylistViewModel.ElapsedTrainingTimer != null && _workoutPlaylistViewModel.ElapsedTrainingTimer.IsRunning)
            {
                _workoutPlaylistViewModel.ElapsedTrainingTimer.Stop();
                return;
            }
            if(_workoutPlaylistViewModel.ElapsedTrainingTimer != null && _workoutPlaylistViewModel.ElapsedTrainingTimer.ElapsedMilliseconds > 0)
            {
                _workoutPlaylistViewModel.ElapsedTrainingTimer.Start();
                return;
            }

            _workoutPlaylistViewModel.ElapsedTrainingTimer = Stopwatch.StartNew();
            _workoutPlaylistViewModel.StopWatchTimer.Enabled = true;
            _workoutPlaylistViewModel.StopWatchTimer.Start();
        }

        private readonly WorkoutPlaylistViewModel _workoutPlaylistViewModel;
        public StartWorkoutCommand(WorkoutPlaylistViewModel workoutPlaylistViewModel)
        {
            _workoutPlaylistViewModel = workoutPlaylistViewModel;
        }
    }
}
