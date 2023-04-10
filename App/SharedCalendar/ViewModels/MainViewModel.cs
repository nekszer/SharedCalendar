using LightForms.Commands;
using LightForms.Validations;
using SharedCalendar.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SharedCalendar.ViewModels
{
    public class MainViewModel : ViewModelBase<object>
    {
        #region Validatable Property Email
        private ValidatableObject<string> email = new ValidatableObject<string>
        {
            IsValid = false,
            Validations = new List<IValidationRule<string>>
            {
                new IsNotNullOrEmptyRule<string>
                {
                    ValidationMessage = "Invalid Email"
                }
            }
        };
        public ValidatableObject<string> Email
        {
            get => email;
            set { email = value; OnPropertyChanged(); }
        }
        #endregion

        #region Validatable Property Password
        private ValidatableObject<string> password = new ValidatableObject<string>
        {
            IsValid = false,
            Validations = new List<IValidationRule<string>>
            {
                new IsNotNullOrEmptyRule<string>
                {
                    ValidationMessage = "Invalid Password"
                }
            }
        };
        public ValidatableObject<string> Password
        {
            get => password;
            set { password = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property BtnLogin
        /// <summary>
        /// BtnLogin
        /// </summary>
        private ICommand btnLogin;
        public ICommand BtnLogin
        {
            get { return btnLogin; }
            set { btnLogin = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property BtnSignUp
        /// <summary>
        /// BtnSignUp
        /// </summary>
        private ICommand btnSignUp;
        public ICommand BtnSignUp
        {
            get { return btnSignUp; }
            set { btnSignUp = value; OnPropertyChanged(); }
        }
        #endregion

        public override void Appearing(string route)
        {
            base.Appearing(route);
            BtnLogin = new AsyncCommand(BtnLogin_Click, BtnLogin_CanExecute);
            Email.ValueChanged += Form_ValueChanged;
            Password.ValueChanged += Form_ValueChanged;
            BtnSignUp = new AsyncCommand(BtnSignUp_Click);

            #region DEBUG
            Email.Value = "nekszer@gmail.com";
            Password.Value = "123456";
            #endregion

            var apiService = Container.Create<IApiService>();
            if (apiService.IsAuthenticated())
                Navigation.PushAsync(Routes.Menu, LightForms.Services.ReplaceAction.MasterDetailPage);
        }

        private Task BtnSignUp_Click(object arg)
        {
            return Navigation.PushAsync(Routes.SignUp, LightForms.Services.ReplaceAction.Push);
        }

        private void Form_ValueChanged(object sender, bool e)
        {
            BtnLogin?.RaiseCanExecuteChanged();
        }

        private async Task BtnLogin_Click(object arg)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Container.Create<IAlertPopup>().Show("Info", "Necesitas conexión a internet", "Aceptar");
                return;
            }
            var apiService = Container.Create<IApiService>();
            if(await apiService.SignIn(Email.Value, Password.Value))
            {
                await Navigation.PushAsync(Routes.Menu, LightForms.Services.ReplaceAction.MasterDetailPage);
                return;
            }
            await Container.Create<IAlertPopup>().Show("Info", "No reconocemos tus datos", "Aceptar");
        }

        private bool BtnLogin_CanExecute(object arg)
        {
            return Email.IsValid && Password.IsValid;
        }
    }
}