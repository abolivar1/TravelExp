using System.Globalization;
using TravelExp.Prism.Interfaces;
using TravelExp.Prism.Resources;
using Xamarin.Forms;

namespace TravelExp.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }

        public static string Accept => Resource.Accept;

        public static string ConnectionError => Resource.ConnectionError;

        public static string Error => Resource.Error;

        public static string Email => Resource.Email;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string EmailError => Resource.EmailError;

        public static string Password => Resource.Password;

        public static string PasswordError => Resource.PasswordError;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Register => Resource.Register;

        public static string Login => Resource.Login;

        public static string LoginError => Resource.LoginError;

        public static string ForgotPassword => Resource.ForgotPassword;

        public static string Loading => Resource.Loading;

        public static string Trips => Resource.Trips;

        public static string StartDate => Resource.StartDate;

        public static string EndDate => Resource.EndDate;

        public static string TripDetails => Resource.TripDetails;

        public static string NoExpenses => Resource.NoExpenses;

        public static string Atention => Resource.Atention;

        public static string TripTo => Resource.TripTo;

        public static string Date => Resource.Date;

        public static string Amount => Resource.Amount;

        public static string Description => Resource.Description;

        public static string NoTrips => Resource.NoTrips;

        public static string ChangePassword => Resource.ChangePassword;

        public static string ModifyUser => Resource.ModifyUser;

        public static string Logout => Resource.Logout;

        public static string FirstName => Resource.FirstName;

        public static string FirstNamePlaceholder => Resource.FirstNamePlaceholder;

        public static string LastName => Resource.LastName;

        public static string LastNamePlaceholder => Resource.LastNamePlaceholder;

        public static string Document => Resource.Document;

        public static string DocumentPlaceholder => Resource.DocumentPlaceholder;

        public static string Address => Resource.Address;

        public static string AddressPlaceholder => Resource.AddressPlaceholder;

        public static string Ok => Resource.Ok;

        public static string DataSaved => Resource.DataSaved;

        public static string NoDocument => Resource.NoDocument;

        public static string NoAddress => Resource.NoAddress;

        public static string NoFirstName => Resource.NoFirstName;

        public static string NoLastName => Resource.NoLastName;

        public static string Source => Resource.Source;

        public static string Cancel => Resource.Cancel;

        public static string Gallery => Resource.Gallery;

        public static string Camera => Resource.Camera;

        public static string Save => Resource.Save;

        public static string CurrentPassword => Resource.CurrentPassword;

        public static string CurrentPasswordPlaceholder => Resource.CurrentPasswordPlaceholder;

        public static string NewPassword => Resource.NewPassword;

        public static string NewPasswordPlaceholder => Resource.NewPasswordPlaceholder;

        public static string ConfirmNewPassword => Resource.ConfirmNewPassword;

        public static string ConfirmNewPasswordPlaceholder => Resource.ConfirmNewPasswordPlaceholder;

        public static string NoCurrentPassword => Resource.NoCurrentPassword;

        public static string NoNewPassword => Resource.NoNewPassword;

        public static string NoConfirmPassword => Resource.NoConfirmPassword;

        public static string PasswordsDontMatch => Resource.PasswordsDontMatch;

        public static string AddTrip => Resource.AddTrip;

        public static string City => Resource.City;

        public static string NoCity => Resource.NoCity;

        public static string NoDates => Resource.NoDates;

        public static string NoDescription => Resource.NoDescription;

        public static string DescriptionPlaceholder => Resource.DescriptionPlaceholder;

        public static string Time => Resource.Time;

        public static string ExpenseType => Resource.ExpenseType;

        public static string AddExpense => Resource.AddExpense;

        public static string AddTripDetail => Resource.AddTripDetail;

        public static string NoExpenseType => Resource.NoExpenseType;

        public static string NoDate => Resource.NoDate;

        public static string NoTime => Resource.NoTime;

        public static string NoAmount => Resource.NoAmount;

        public static string NoExpImage => Resource.NoExpImage;

        public static string RecoverPassword => Resource.RecoverPassword;

        public static string UserExist => Resource.UserExist;

    }

}
