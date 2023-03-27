using Coffee_Shop.Views.Pages;
using CoffeeShop.Commands;
using CoffeeShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using static System.Net.Mime.MediaTypeNames;

namespace Coffee_Shop.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private Frame? mainFrame = new();
        public Frame MainFrame
        {
            get
            {
                return mainFrame;
            }
            set
            {
                mainFrame = value;
                OnPropertyChanged(nameof(MainFrame));
            }
        }

        #region Constructor

        private string? CultureName;
        public MainViewModel(Frame frame, Button adminButton, string CultureName, string UserType) // стрем переделать
        {
            this.mainFrame = frame;
            this.UserType = UserType;
            this.AdminButton = adminButton;
            this.CultureName = CultureName;

            ConfiguringApplicationForUserType();
        }

        #endregion

        #region Configuring Application For UserType

        private Button? AdminButton;
        private string UserType = string.Empty;
        private void ConfiguringApplicationForUserType()
        {
            switch (UserType)
            {
                case "Admin": SettingUpForAdmin(); break;
                case "User":  SettingUpForUser();  break;
                case "Guest": SettingUpForGuest(); break;
            }
        }

        private void SettingUpForGuest()
        {
            throw new NotImplementedException();
        }

        private void SettingUpForUser()
        {
            mainFrame.Content = new HomeView();
        }

        private void SettingUpForAdmin()
        {
            App.ConnectionViewModelWithAdmin();
            AdminButton.Visibility = Visibility.Visible;
        }

        #endregion

        #region Commands
        // chane for example About to Show About Page
        #region Menu

        private DelegateCommand? showMenuCommand;

        public ICommand ShowMenuCommand
        {
            get
            {
                if (showMenuCommand == null)
                {
                    showMenuCommand = new DelegateCommand(ShowMenu);
                }
                return showMenuCommand;
            }
        }

        private void ShowMenu()
        {
            App.ConnectionViewModelWithMenu();
        }

        #endregion

        #region Basket

        private DelegateCommand? showBasketCommand;

        public ICommand ShowBasketCommand
        {
            get
            {
                if (showBasketCommand == null)
                {
                    showBasketCommand = new DelegateCommand(ShowBasket);
                }
                return showBasketCommand;
            }
        }

        private void ShowBasket()
        {
            mainFrame.Content = new BasketView();
        }

        #endregion

        #region Exit

        private DelegateCommand? exitCommand;

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new DelegateCommand(Exit);
                }
                return exitCommand;
            }
        }

        private void Exit()
        {
            System.Windows.Application.Current.Shutdown();
        }

        #endregion

        #region About

        private DelegateCommand? showAboutCommand;

        public ICommand ShowAboutCommand
        {
            get
            {
                if (showAboutCommand == null)
                {
                    showAboutCommand = new DelegateCommand(ShowAbout);
                }
                return showAboutCommand;
            }
        }

        private void ShowAbout()
        {
            mainFrame.Content = new AboutView();
        }

        #endregion
        
        #region Home

        private DelegateCommand? showHomeCommand;

        public ICommand ShowHomeCommand
        {
            get
            {
                if (showHomeCommand == null)
                {
                    showHomeCommand = new DelegateCommand(ShowHome);
                }
                return showHomeCommand;
            }
        }

        private void ShowHome()
        {
            App.ConnectionTheHomeViewModel();
        }

        #endregion

        #region Admin

        private DelegateCommand? showAdminPageCommand;

        public ICommand ShowAdminPageCommand
        {
            get
            {
                if (showAdminPageCommand == null)
                {
                    showAdminPageCommand = new DelegateCommand(ShowAdminPage);
                }
                return showAdminPageCommand;
            }
        }

        private void ShowAdminPage()
        {
            App.ConnectionViewModelWithAdmin();
        }

        #endregion

        #endregion
    }
}
