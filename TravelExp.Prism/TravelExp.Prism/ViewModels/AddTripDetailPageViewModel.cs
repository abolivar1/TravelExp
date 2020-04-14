using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelExp.Prism.ViewModels
{
    public class AddTripDetailPageViewModel : ViewModelBase
    {
        public AddTripDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Add Trip Detail";
        }
    }
}
