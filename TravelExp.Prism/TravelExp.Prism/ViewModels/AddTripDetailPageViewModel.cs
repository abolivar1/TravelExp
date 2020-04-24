using Xamarin.Forms;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TravelExp.Common.Helpers;
using TravelExp.Common.Models;
using TravelExp.Common.Services;
using TravelExp.Prism.Helpers;
using Plugin.Media.Abstractions;
using System;
using Plugin.Media;

namespace TravelExp.Prism.ViewModels
{
    public class AddTripDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IFilesHelper _filesHelper;
        private bool _isRunning;
        private bool _isEnabled;
        private DateTime _date;
        private TimeSpan _time;
        private ObservableCollection<ExpenseTypeResponse> _expenseTypes;
        private ImageSource _image;
        private ExpenseTypeResponse _expenseType;
        private MediaFile _file;
        private TripDetailRequest _detail;
        private TripResponse _trip;
        private DelegateCommand _changeImageCommand;
        private DelegateCommand _AddTripDetailCommand;
        public AddTripDetailPageViewModel(
            INavigationService navigationService,
            IApiService apiService,
            IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _filesHelper = filesHelper;
            IsEnabled = true;
            Image = App.Current.Resources["UrlNoImage"].ToString();
            Detail = new TripDetailRequest();
            Date = DateTime.Today;
            LoadExpenseTypesAsync();
        }

        public DelegateCommand ChangeImageCommand => _changeImageCommand ?? (_changeImageCommand = new DelegateCommand(ChangeImageAsync));

        

        public DelegateCommand AddTripDetailCommand => _AddTripDetailCommand ?? (_AddTripDetailCommand = new DelegateCommand(AddTripDetailAsync));
        public ExpenseTypeResponse ExpenseType
        {
            get => _expenseType;
            set => SetProperty(ref _expenseType, value);
        }
        public TripDetailRequest Detail
        {
            get => _detail;
            set => SetProperty(ref _detail, value);
        }
        public TripResponse Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }
        public ObservableCollection<ExpenseTypeResponse> ExpenseTypes
        {
            get => _expenseTypes;
            set => SetProperty(ref _expenseTypes, value);
        }

        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
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
        public DateTime Date 
        { 
            get => _date; 
            set => SetProperty(ref _date, value);
        }
        public TimeSpan Time 
        { 
            get => _time;
            set => SetProperty(ref _time, value);
        }

        private async void ChangeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            string source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.Source,
                Languages.Cancel,
                null,
                Languages.Gallery,
                Languages.Camera);

            if (source == Languages.Cancel)
            {
                _file = null;
                return;
            }

            if (source == Languages.Camera)
            {
                _file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                _file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file != null)
            {
                Image = ImageSource.FromStream(() =>
                {
                    System.IO.Stream stream = _file.GetStream();
                    return stream;
                });
            }
        }
        private async void AddTripDetailAsync()
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

            
            if (_file == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoExpImage, Languages.Accept);
                return;
            }
            byte[] imageArray = null;
            imageArray = _filesHelper.ReadFully(_file.GetStream());
            Detail.ExpenseTypeId = ExpenseType.Id;
            Detail.PictureArray = imageArray;
            Detail.CultureInfo = Languages.Culture;
            Detail.TripId = Trip.Id;
            Detail.Date = Date + Time;
            
            Response response = await _apiService.AddTripDetailAsync(url, "/api", "/Trips/AddTripDetail", "bearer", token.Token, Detail);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, response.Message, Languages.Accept);

            EmailRequest request2 = new EmailRequest
            {
                CultureInfo = Languages.Culture,
                Email = employee.Email
            };

            Response response2 = await _apiService.GetUserByEmail(url, "api", "/Account/GetUserByEmail", "bearer", token.Token, request2);
            EmployeeResponse userResponse = (EmployeeResponse)response2.Result;

            Settings.User = JsonConvert.SerializeObject(userResponse);
            var parameters = new NavigationParameters
            {
                { "token", token },
                { "employee", userResponse },
                { "tripid", Trip.Id}
            };
            await _navigationService.NavigateAsync("/TravelExpMasterDetailPage/NavigationPage/TripsPage/TripDetailsPage", parameters);
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (ExpenseType == null)
             {
                 await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoExpenseType, Languages.Accept);
                 return false;
             }

             if (Date == null)
             {
                 await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoDate, Languages.Accept);
                 return false;
             }
            if (Time == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoTime, Languages.Accept);
                return false;
            }
            if (Detail.Description == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoDescription, Languages.Accept);
                return false;
            }

            if (Detail.Amount <= 0)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoAmount, Languages.Accept);
                return false;
            }
            return true;
        }

        private async void LoadExpenseTypesAsync()
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
            Response response = await _apiService.GetListAsync<ExpenseTypeResponse>(url, "/api", "/ExpenseTypes", "bearer", token.Token);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<ExpenseTypeResponse> list = (List<ExpenseTypeResponse>)response.Result;
            ExpenseTypes = new ObservableCollection<ExpenseTypeResponse>(list.OrderBy(t => t.Name));
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("trip"))
            {
                _trip = parameters.GetValue<TripResponse>("trip");
                Title = Languages.AddTripDetail + " " + _trip.City.Name;
            }

        }
    }
}
