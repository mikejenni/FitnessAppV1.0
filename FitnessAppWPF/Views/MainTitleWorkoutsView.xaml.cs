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
    /// Interaction logic for MainTitleWorkoutsView.xaml
    /// </summary>
    public partial class MainTitleWorkoutsView : UserControl
    {
        public MainTitleWorkoutsView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty StartWorkoutPlaylistCommandProperty = DependencyProperty.Register(
       "StartWorkoutPlaylistCommand", typeof(ICommand),
       typeof(MainTitleWorkoutsView), new PropertyMetadata(null)
       );

        //TODO Selected Item mit Doppelklick selektieren
        public ICommand StartWorkoutPlaylistCommand
        {
            get => (ICommand)GetValue(StartWorkoutPlaylistCommandProperty);
            set => SetValue(StartWorkoutPlaylistCommandProperty, value);
        }

        private void WorkoutListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StartWorkoutPlaylistCommand?.Execute(null);
        }
    }
}
