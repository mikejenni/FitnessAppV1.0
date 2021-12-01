using FitnessAppWPF.ViewModels;
using FitnessAppWPF.Views;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.Commands
{
    public class StartWorkoutPlaylistCommand : CommandBase
    {
        private readonly MainTitleWorkoutsViewModel _mainTitleWorkoutsViewModel;
        private readonly INavigationService _navigationService;
        public StartWorkoutPlaylistCommand(MainTitleWorkoutsViewModel mainTitleWorkoutViewModel, INavigationService navigationService)
        {
            _mainTitleWorkoutsViewModel = mainTitleWorkoutViewModel;
            _navigationService = navigationService;

        }
        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
