using MVVMEssentials.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppWPF.ViewModels
{
    public class LayoutViewModel : ViewModelBase
    {
        public NavigationBarViewModel NavigationBarViewModel { get; }
        public ViewModelBase ContentViewModel { get; }

        public LayoutViewModel(NavigationBarViewModel navigationBarViewModel, ViewModelBase contentViewModel)
        {
            NavigationBarViewModel = navigationBarViewModel;
            ContentViewModel = contentViewModel;
        }

        public override void Dispose()      // Beim navigieren wird die Dispose Methode aufgerufen, Memory leaks zu verhindern, speicher frei räumen
        {
            NavigationBarViewModel.Dispose();
            ContentViewModel.Dispose();

            base.Dispose();
        }
    }
}
