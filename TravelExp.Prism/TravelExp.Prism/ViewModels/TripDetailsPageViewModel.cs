using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelExp.Common.Models;

namespace TravelExp.Prism.ViewModels
{
    public class TripDetailsPageViewModel : ViewModelBase
    {
        private TripResponse _trip;
        private List<TripDetailResponse> _tripDetails; 
        private DelegateCommand _addTripDetailCommand;
        private readonly INavigationService _navigationService;

        public TripDetailsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Trip Details";
            _navigationService = navigationService;
        }

        public DelegateCommand AddTripDetailCommand => _addTripDetailCommand ?? (_addTripDetailCommand = new DelegateCommand(AddTripDetailAsync));

        private async void AddTripDetailAsync()
        {
            await _navigationService.NavigateAsync("AddTripDetailPage");
        }
        public TripResponse Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }
        public List<TripDetailResponse> TripDetails
        {
            get => _tripDetails;
            set => SetProperty(ref _tripDetails, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("trip"))
            {
                _trip = parameters.GetValue<TripResponse>("trip");
                Title = "Trip to " + _trip.City.Name;
                TripDetails = _trip.TripDetails;
            }
        }

    }
}
