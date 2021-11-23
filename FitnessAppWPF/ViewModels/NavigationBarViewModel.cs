using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitnessAppWPF.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateWorkoutBuilderCommand { get; }
        public ICommand NavigateMainTitleWorkoutsCommand { get; }
        public ICommand NavigateHistoryCommand { get; }
        public ICommand NavigateExerciseCommand { get; }
        public ICommand NavigateExerciseBuilderCommand { get; }
        public NavigationBarViewModel(
            INavigationService workoutbuilderNavigationService,
            INavigationService mainTitleWorkoutsNavigationService,
            INavigationService historyNavigationService,
            INavigationService exerciseNavigateCommand,
            INavigationService exercisebuilderNavigationService)
        {

            NavigateWorkoutBuilderCommand = new NavigateCommand(workoutbuilderNavigationService);
            NavigateMainTitleWorkoutsCommand = new NavigateCommand(mainTitleWorkoutsNavigationService);
            NavigateHistoryCommand = new NavigateCommand(historyNavigationService);
            NavigateExerciseCommand = new NavigateCommand(exerciseNavigateCommand);
            NavigateExerciseBuilderCommand = new NavigateCommand(exercisebuilderNavigationService);
        }

    }
}
