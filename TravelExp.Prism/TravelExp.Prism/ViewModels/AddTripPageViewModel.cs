﻿using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TravelExp.Common.Helpers;
using TravelExp.Common.Models;
using TravelExp.Common.Services;
using TravelExp.Prism.Helpers;

namespace TravelExp.Prism.ViewModels
{
    public class AddTripPageViewModel : ViewModelBase
    {
        public SelectionRange _selectedRange;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private CityResponse _city;
        private string _description;
        private ObservableCollection<CityResponse> _cities;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _AddTripCommand;

        public AddTripPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.AddTrip;
            IsEnabled = true;
            LoadCitiesAsync();
        }

        public DelegateCommand AddTripCommand => _AddTripCommand ?? (_AddTripCommand = new DelegateCommand(AddTripAsync));
        public CityResponse City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public SelectionRange SelectedRange
        {
            get => _selectedRange;
            set => SetProperty(ref _selectedRange, value);
        }

        public ObservableCollection<CityResponse> Cities
        {
            get => _cities;
            set => SetProperty(ref _cities, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
        public string Description 
        { 
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private async void AddTripAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            EmployeeResponse employee = JsonConvert.DeserializeObject<EmployeeResponse>(Settings.User);
            TripRequest request = new TripRequest
            {
                Description = Description,
                StartDate = SelectedRange.StartDate,
                EndDate = SelectedRange.EndDate,
                CityId = City.Id,
                EmployeeId = employee.Id,
                CultureInfo = Languages.Culture
            };
            Response response = await _apiService.AddTripAsync(url, "api", "/Trips", "bearer", token.Token, request);
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            IsRunning = false;
            TravelExpMasterDetailPageViewModel.GetInstance().ReloadUser();
            await App.Current.MainPage.DisplayAlert(Languages.Ok, response.Message, Languages.Accept);
            await _navigationService.NavigateAsync("/TravelExpMasterDetailPage/NavigationPage/TripsPage");
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (City == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoCity, Languages.Accept);
                return false;
            }

            if (SelectedRange == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoDates, Languages.Accept);
                return false;
            }

            if (Description == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoDescription, Languages.Accept);
                return false;
            }

            return true;
        }

        private async void LoadCitiesAsync()
        {
            IsRunning = true;
            IsEnabled = false;
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            Response response = await _apiService.GetListAsync<CityResponse>(url, "/api", "/Cities", "bearer", token.Token);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<CityResponse> list = (List<CityResponse>)response.Result;
            Cities = new ObservableCollection<CityResponse>(list.OrderBy(t => t.Name));
        }

    }
}
