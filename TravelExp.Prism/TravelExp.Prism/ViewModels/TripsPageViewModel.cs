using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using TravelExp.Common.Models;
using TravelExp.Common.Services;

namespace TravelExp.Prism.ViewModels
{
    public class TripsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private List<TripItemViewModel> _trips;
        private bool _isRunning;
        private DelegateCommand _addTripCommand;

        public TripsPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Trips";
            LoadTripsAsync();
        }

        public DelegateCommand AddTripCommand => _addTripCommand ?? (_addTripCommand = new DelegateCommand(AddTripAsync));

        private async void AddTripAsync()
        {
            await _navigationService.NavigateAsync("AddTripPage");
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }


        public List<TripItemViewModel> Trips
        {
            get => _trips;
            set => SetProperty(ref _trips, value);
        }

        private async void LoadTripsAsync()
        {
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<TripResponse>(
                url,
                "/api",
                "/Trips");
            IsRunning = false;
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            List<TripResponse> list = (List<TripResponse>)response.Result;
            Trips = list.Select(t => new TripItemViewModel(_navigationService)
            {
                EndDate = t.EndDate,
                TripDetails = t.TripDetails,
                Id = t.Id,
                Employee = t.Employee,
                City = t.City,
                TotalAmount = t.TotalAmount,
                StartDate = t.StartDate
            }).ToList();

        }
    }

}
