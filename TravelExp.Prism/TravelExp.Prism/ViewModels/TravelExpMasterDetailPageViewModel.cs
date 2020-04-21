using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TravelExp.Common.Helpers;
using TravelExp.Common.Models;
using TravelExp.Common.Services;
using TravelExp.Prism.Helpers;

namespace TravelExp.Prism.ViewModels
{
    public class TravelExpMasterDetailPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private static TravelExpMasterDetailPageViewModel _instance;

        private readonly INavigationService _navigationService;
        private EmployeeResponse _user;

        public TravelExpMasterDetailPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _instance = this;
            _apiService = apiService;
            LoadUser();
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public EmployeeResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<EmployeeResponse>(Settings.User);
            }
        }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "trips",
                    PageName = "TripsPage",
                    Title = "Trips"
                },
                new Menu
                {
                    Icon = "changepass",
                    PageName = "ChangePasswordPage",
                    Title = "Change Password"
                },
                new Menu
                {
                    Icon = "user",
                    PageName = "ModifyUserPage",
                    Title = "Modify User"
                },
                new Menu
                {
                    Icon = "logout",
                    PageName = "LoginPage",
                    Title = "Logout"
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
        public static TravelExpMasterDetailPageViewModel GetInstance()
        {
            return _instance;
        }

        public async void ReloadUser()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                return;
            }

            EmployeeResponse user = JsonConvert.DeserializeObject<EmployeeResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            EmailRequest emailRequest = new EmailRequest
            {
                CultureInfo = Languages.Culture,
                Email = user.Email
            };

            Response response = await _apiService.GetUserByEmail(url, "api", "/Account/GetUserByEmail", "bearer", token.Token, emailRequest);
            EmployeeResponse userResponse = (EmployeeResponse)response.Result;
            Settings.User = JsonConvert.SerializeObject(userResponse);
            LoadUser();
        }

    }

}
