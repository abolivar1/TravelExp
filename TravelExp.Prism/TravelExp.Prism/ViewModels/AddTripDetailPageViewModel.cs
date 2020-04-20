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

namespace TravelExp.Prism.ViewModels
{
    public class AddTripDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private ImageSource _image;
        private ExpenseTypeResponse _expenseType;
        private ObservableCollection<ExpenseTypeResponse> _expenseTypes;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _AddTripDetailCommand;
        public AddTripDetailPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            Title = "Add Trip Detail";
            _navigationService = navigationService;
            _apiService = apiService;
            IsEnabled = true;
            Image = App.Current.Resources["UrlNoImage"].ToString();
            LoadExpenseTypesAsync();
        }

        public DelegateCommand AddTripCommand => _AddTripDetailCommand ?? (_AddTripDetailCommand = new DelegateCommand(AddTripDetailAsync));
        public ExpenseTypeResponse ExpenseType
        {
            get => _expenseType;
            set => SetProperty(ref _expenseType, value);
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

        private async void AddTripDetailAsync()
        {
            /*bool isValid = await ValidateDataAsync();
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

            

            User.TeamId = Team.Id;
            User.PictureArray = imageArray;
            User.CultureInfo = Languages.Culture;

            Response response = await _apiService.RegisterUserAsync(url, "/api", "/Account", User);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, response.Message, Languages.Accept);
            await _navigationService.GoBackAsync();

            /*TripDetailRequest request = new TripDetailRequest
            {
                StartDate = SelectedRange.StartDate,
                EndDate = SelectedRange.EndDate,
                CityId = City.Id,
                EmployeeId = employee.Id,
                CultureInfo = Languages.Culture
            };*/
            //Response response = await _apiService.AddTripAsync(url, "api", "/Trips", "bearer", token.Token, request);
            /*if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error has ocurred saving the trip", Languages.Accept);
                return;
            }
            IsRunning = false;
            await App.Current.MainPage.DisplayAlert("Ok", response.Message, Languages.Accept);
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
                { "employee", userResponse }
            };
            await _navigationService.GoBackAsync(parameters);*/
        }

        private async Task<bool> ValidateDataAsync()
        {
            /*if (City == null)
             {
                 await App.Current.MainPage.DisplayAlert(Languages.Error, "You must select a city", Languages.Accept);
                 return false;
             }

             if (SelectedRange == null)
             {
                 await App.Current.MainPage.DisplayAlert(Languages.Error, "You must select a date rage", Languages.Accept);
                 return false;
             }*/

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
                await App.Current.MainPage.DisplayAlert(Languages.Error, "An error has ocurred looking for data", Languages.Accept);
                return;
            }

            List<ExpenseTypeResponse> list = (List<ExpenseTypeResponse>)response.Result;
            ExpenseTypes = new ObservableCollection<ExpenseTypeResponse>(list.OrderBy(t => t.Name));
        }
    }
}
