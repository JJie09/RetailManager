using Caliburn.Micro;
using RMDesktopUI.EventModels;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>, IHandle<RegisterEvent>
    {
        private SalesViewModel _salesVM;
        private IEventAggregator _events;
        private ILoggedInUserModel _user;

        public bool IsLoggedIn
        {
            get { return !String.IsNullOrWhiteSpace(_user.Token); }
        }

        public ShellViewModel(SalesViewModel salesVM, IEventAggregator events, ILoggedInUserModel user)
        {
            _salesVM = salesVM;
            _user = user;

            _events = events;
            _events.Subscribe(this);

            ActivateItem(IoC.Get<LoginViewModel>());
        }
        public void ExitApplication()
        {
            TryClose();
        }
        public void Logout()
        {
            _user.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
        public void Handle(RegisterEvent message)
        {
            ActivateItem(IoC.Get<RegisterUserViewModel>());
        }
    }
}
