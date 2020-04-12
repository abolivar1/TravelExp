using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelExp.Prism.ViewModels
{
    public class ChangePasswordPageViewModel : ViewModelBase
    {
        public ChangePasswordPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Chage Password";
        }
    }
}
