using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelExp.Common.Models;
using TravelExp.Prism.Helpers;

namespace TravelExp.Prism.ViewModels
{
    public class TripDetailsPageViewModel : ViewModelBase
    {
        private int tripid;
        private TripResponse _trip;
        private EmployeeResponse _employee;
        private List<TripDetailResponse> _tripDetails; 
        private DelegateCommand _addTripDetailCommand;
        private readonly INavigationService _navigationService;

        public TripDetailsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.TripDetails;
            _navigationService = navigationService;
        }

        public DelegateCommand AddTripDetailCommand => _addTripDetailCommand ?? (_addTripDetailCommand = new DelegateCommand(AddTripDetailAsync));

        private async void AddTripDetailAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "trip", Trip }
            };
            await _navigationService.NavigateAsync("AddTripDetailPage", parameters);
        }
        public TripResponse Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }

        public EmployeeResponse Employee
        {
            get => _employee;
            set => SetProperty(ref _employee, value);
        }
        public List<TripDetailResponse> TripDetails
        {
            get => _tripDetails;
            set => SetProperty(ref _tripDetails, value);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("trip"))
            {
                _trip = parameters.GetValue<TripResponse>("trip");
                Title = Languages.TripTo +" "+ _trip.City.Name;
                TripDetails = _trip.TripDetails;
                if (_tripDetails.Count <= 0)
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Atention, Languages.NoExpenses, Languages.Accept);
                }

            }

            if (parameters.ContainsKey("employee"))
            {
                tripid = parameters.GetValue<int>("tripid");
                _employee = parameters.GetValue<EmployeeResponse>("employee");
                List<TripResponse> list = _employee.Trips;
                Trip = list.Select(t => new TripResponse
                {
                    EndDate = t.EndDate,
                    TripDetails = t.TripDetails,
                    Id = t.Id,
                    Employee = t.Employee,
                    City = t.City,
                    TotalAmount = t.TotalAmount,
                    StartDate = t.StartDate
                }).FirstOrDefault(t => t.Id == tripid);
                Title = "Trip to " + _trip.City.Name;
                TripDetails = _trip.TripDetails;
            }
        }

    }
}
