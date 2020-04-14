using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelExp.Prism.ViewModels
{
    public class AddTripPageViewModel : ViewModelBase
    {
        public AddTripPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Add Trip";
        }
    }
}
