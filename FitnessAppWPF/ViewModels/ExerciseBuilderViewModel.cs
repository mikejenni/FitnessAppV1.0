using FitnessAppWPF.Commands;
using FitnessAppWPF.Stores;
using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static FitnessApp.Business.Models.Enums;

namespace FitnessAppWPF.ViewModels
{
    public class ExerciseBuilderViewModel : ViewModelBase

    {
        public ExerciseBuilderViewModel(ExerciseStore exerciseStore)
        {
            CreateExerciseCommand = new CreateExerciseCommand(this, exerciseStore);
        }

        public ICommand CreateExerciseCommand { get; }

        public bool RepsSelected => SelectedTrainingTarget == TrainingTarget.Reps;

        public bool TimeSelected => SelectedTrainingTarget == TrainingTarget.Time;



        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private TrainingTarget _selectedtrainingTarget;
        public TrainingTarget SelectedTrainingTarget
        {
            get { return _selectedtrainingTarget; }
            set
            {
                _selectedtrainingTarget = value;
                OnPropertyChanged(nameof(SelectedTrainingTarget));
                OnPropertyChanged(nameof(RepsSelected));
                OnPropertyChanged(nameof(TimeSelected));
            }
        }

        public IEnumerable<TrainingTarget> TrainingTargets
        {
            get
            {
                return Enum.GetValues(typeof(TrainingTarget)).Cast<TrainingTarget>();
            }
        }

        private int _time;
        public int Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        private int _reps;
        public int Reps
        {
            get { return _reps; }
            set
            {
                _reps = value;
                OnPropertyChanged(nameof(Reps));
            }
        }



        private Bodypart _selectedbodypart;
        public Bodypart SelectedBodyPart
        {
            get { return _selectedbodypart; }
            set
            {
                _selectedbodypart = value;
                OnPropertyChanged(nameof(SelectedBodyPart));
            }
        }

        public IEnumerable<Bodypart> Bodyparts
        {
            get
            {
                return Enum.GetValues(typeof(Bodypart)).Cast<Bodypart>();
            }
        }

        private bool _countAsRound = true;
        public bool CountAsRound
        {
            get { return _countAsRound; }
            set
            {
                _countAsRound = value;
                OnPropertyChanged(nameof(CountAsRound));
            }
        }


    }
}
