using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelExp.Common.Models;
using TravelExp.Common.Services;

namespace TravelExp.Prism.ViewModels
{
    public class TripsPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private List<TripResponse> _trips;

        public TripsPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = "Trips";
            LoadTournamentsAsync();
        }

        public List<TripResponse> Trips
        {
            get => _trips;
            set => SetProperty(ref _trips, value);
        }

        private async void LoadTournamentsAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<TripResponse>(
                url,
                "/api",
                "/Trips");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            Trips = (List<TripResponse>)response.Result;
        }
    }

}
