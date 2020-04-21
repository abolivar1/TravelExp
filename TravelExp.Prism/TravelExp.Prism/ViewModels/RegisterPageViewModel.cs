using Xamarin.Forms;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelExp.Common.Helpers;
using TravelExp.Common.Services;
using TravelExp.Common.Models;
using System.Threading.Tasks;
using TravelExp.Prism.Helpers;
using Plugin.Media.Abstractions;
using Plugin.Media;

namespace TravelExp.Prism.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IRegexHelper _regexHelper;
        private readonly IApiService _apiService;
        private readonly IFilesHelper _filesHelper;
        private ImageSource _image;
        private UserRequest _user;
        private bool _isRunning;
        private bool _isEnabled;
        private MediaFile _file;
        private DelegateCommand _changeImageCommand;
        private DelegateCommand _registerCommand;

        public RegisterPageViewModel(
            INavigationService navigationService,
            IRegexHelper regexHelper,
            IApiService apiService,
            IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _regexHelper = regexHelper;
            _apiService = apiService;
            _filesHelper = filesHelper;
            Title = "Register";
            Image = App.Current.Resources["UrlNoImage"].ToString();
            IsEnabled = true;
            User = new UserRequest();
        }

        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(RegisterAsync));
        public DelegateCommand ChangeImageCommand => _changeImageCommand ?? (_changeImageCommand = new DelegateCommand(ChangeImageAsync));

        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public UserRequest User
        {
            get => _user;
            set => SetProperty(ref _user, value);
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

        private async void RegisterAsync()
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

            byte[] imageArray = null;
            if (_file != null)
            {
                imageArray = _filesHelper.ReadFully(_file.GetStream());
            }

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

            await App.Current.MainPage.DisplayAlert("Ok", response.Message, Languages.Accept);
            await _navigationService.GoBackAsync();

        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(User.Document))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "You must enter a Document", Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.FirstName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "You must enter a first name", Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.LastName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "You must enter a last name", Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.Address))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "You must enter an Address", Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.Email) || !_regexHelper.IsValidEmail(User.Email))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.Password) || User.Password?.Length < 6)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "You must enter the confirm password", Languages.Accept);
                return false;
            }

            if (User.Password != User.PasswordConfirm)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "Passwords don't match", Languages.Accept);
                return false;
            }

            return true;
        }
        private async void ChangeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            string source = await Application.Current.MainPage.DisplayActionSheet(
                "Select Source",
                "Cancel",
                null,
                "From Gallery",
                "From Camera");

            if (source == "Cancel")
            {
                _file = null;
                return;
            }

            if (source == "From Camera")
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


    }

}
