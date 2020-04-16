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

        private bool _isRunning;
        public AddTripPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Add Trip";
            IsRunning = false;
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
    }
}
