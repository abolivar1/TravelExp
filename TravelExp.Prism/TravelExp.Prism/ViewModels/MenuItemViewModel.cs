using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using TravelExp.Common.Helpers;
using TravelExp.Common.Models;

namespace TravelExp.Prism.ViewModels
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;

        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        private async void SelectMenuAsync()
        {
            if(PageName.Equals("LoginPage"))
            {
                await _navigationService.NavigateAsync($"NavigationPage/{PageName}");
                Settings.IsLogin = false;
                Settings.User = null;
                Settings.Token = null;
            }
            
            await _navigationService.NavigateAsync($"/TravelExpMasterDetailPage/NavigationPage/{PageName}");
        }
    }

}
