using LightForms;
using LightForms.Attributes;
using LightForms.Commands;
using LightForms.Core;
using LightForms.Extensions;
using LightForms.Services;
using LightForms.Validations;
using SharedCalendar.Extensions;
using SharedCalendar.Models;
using SharedCalendar.Resources;
using SharedCalendar.Services;
using SharedCalendar.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SharedCalendar.ViewModels
{
    public class SignUpViewModel : ViewModelBase<object>
    {
        #region Validatable Property Email
        private ValidatableObject<string> email = new ValidatableObject<string>
        {
            IsValid = false,
            Validations = new List<IValidationRule<string>>
            {
                new IsNotNullOrEmptyRule<string>
                {
                    ValidationMessage = "Email no válido"
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
                    ValidationMessage = "Contraseña no válida"
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
        public ICommand BtnSignUp
        {
            get { return btnLogin; }
            set { btnLogin = value; OnPropertyChanged(); }
        }
        #endregion

        public override void Appearing(string route)
        {
            base.Appearing(route);
            BtnSignUp = new AsyncCommand(BtnSignUp_Click, BtnLogin_CanExecute);
            Email.ValueChanged += Form_ValueChanged;
            Password.ValueChanged += Form_ValueChanged;
        }

        private void Form_ValueChanged(object sender, bool e)
        {
            BtnSignUp?.RaiseCanExecuteChanged();
        }

        private async Task BtnSignUp_Click(object arg)
        {
            if(Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Container.Create<IAlertPopup>().Show("Info", "Necesitas conexión a internet", "Aceptar");
                return;
            }
            var apiService = Container.Create<IApiService>();
            try
            {
                if (await apiService.SignUp(Email.Value, Password.Value))
                {
                    await Navigation.PushAsync(Routes.Menu, LightForms.Services.ReplaceAction.MasterDetailPage);
                    return;
                }
                await Container.Create<IAlertPopup>().Show("Info", "No podemos registrarte, intenta más tarde.", "Aceptar");
            }
            catch (ApiException ex)
            {
                await Container.Create<IAlertPopup>().Show("Info", ex.Message, "Aceptar");
            }
        }

        private bool BtnLogin_CanExecute(object arg)
        {
            return Email.IsValid && Password.IsValid;
        }
    }
}