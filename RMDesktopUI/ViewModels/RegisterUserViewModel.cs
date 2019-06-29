using Caliburn.Micro;
using Newtonsoft.Json;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMDesktopUI.ViewModels
{
    public class RegisterUserViewModel : Screen
    {
        private string _emailAddress="";
        private string _firstName = "";
        private string _lastName = "";
        private string _password = "";
        private string _confirmPassword = "";
        private string _errorMessage = "";
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;

        public RegisterUserViewModel(IAPIHelper apiHelper, IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _events = events;
            EmailAddress = "jj@1234";
            Password = "pass1234";
        }

        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = value;
                NotifyOfPropertyChange(() => EmailAddress);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                NotifyOfPropertyChange(() => ConfirmPassword);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }

        public bool IsErrorVisible
        {
            get { return ErrorMessage?.Length > 0; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }

        public bool CanRegister
        {
            get
            {
                if (EmailAddress?.Length > 0 && Password?.Length > 0 && ConfirmPassword.Length > 0 && LastName.Length > 0 && FirstName.Length > 0)
                {
                    return Password.Equals(ConfirmPassword);
                }
                return false;
            }
        }
        public void ConfirmPasswordChanged()
        {

        }
        public async Task Register()
        {
            try
            {
                ErrorMessage = "";
                LoggedInUserModel registerUser = new LoggedInUserModel()
                {
                    EmailAddress = EmailAddress,
                    FirstName = FirstName,
                    LastName = LastName
                };
                LoggedInUserModel registeredUser = await _apiHelper.RegisterUser(registerUser, Password);
                if (registeredUser != null)
                {
                    MessageBox.Show($"{JsonConvert.SerializeObject(registeredUser,Formatting.Indented)}", "Registration Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.TryClose();

                }
                //_events.PublishOnUIThread(new LogOnEvent());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
