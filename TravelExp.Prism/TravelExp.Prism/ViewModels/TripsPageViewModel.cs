using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using TravelExp.Common.Helpers;
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
        private EmployeeResponse _employee;

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

        public EmployeeResponse Employee
        {
            get => _employee;
            set => SetProperty(ref _employee, value);
        }

        private void LoadTripsAsync()
        {
            IsRunning = true;
            EmployeeResponse userResponse = JsonConvert.DeserializeObject<EmployeeResponse>(Settings.User);
            List<TripResponse> list = (List<TripResponse>)userResponse.Trips;
            Trips = list.Select(t => new TripItemViewModel(_navigationService)
            {
                Description = t.Description,
                EndDate = t.EndDate,
                TripDetails = t.TripDetails,
                Id = t.Id,
                Employee = t.Employee,
                City = t.City,
                TotalAmount = t.TotalAmount,
                StartDate = t.StartDate
            }).OrderByDescending(t => t.Id).ToList();
            IsRunning = false;
        }
        
    }

}
