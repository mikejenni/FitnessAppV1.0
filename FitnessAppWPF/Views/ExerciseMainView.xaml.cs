using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitnessAppWPF.Views
{
    /// <summary>
    /// Interaction logic for ExerciseMainView.xaml
    /// </summary>
    public partial class ExerciseMainView : UserControl
    {
        public ExerciseMainView()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty AddExerciseToUnsavedWorkoutCommandProperty = DependencyProperty.Register(
        "AddExerciseToUnsavedWorkoutCommand", typeof(ICommand),
        typeof(ExerciseMainView), new PropertyMetadata(null) 
        );

        //TODO Selected Item mit Doppelklick selektieren
        public ICommand AddExerciseToUnsavedWorkoutCommand
        {
            get => (ICommand)GetValue(AddExerciseToUnsavedWorkoutCommandProperty);
            set => SetValue(AddExerciseToUnsavedWorkoutCommandProperty, value);
        }

        private void ExerciseListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AddExerciseToUnsavedWorkoutCommand != null)
            {
                AddExerciseToUnsavedWorkoutCommand.Execute(null);
            }
        }
    }
}
