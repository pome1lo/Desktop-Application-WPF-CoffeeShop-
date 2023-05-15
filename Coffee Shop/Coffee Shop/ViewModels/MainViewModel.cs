using Coffee_Shop.Views.Pages;
using CoffeeShop.Commands;
using CoffeeShop.Views;
using CustomControl;
using MaterialDesignThemes.Wpf;
using Microsoft.Web.WebView2.Core;
using System;
using System.CodeDom;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Coffee_Shop.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        #region Constructor

        public MainViewModel()
        {
            ConfiguringApplicationForUserType();
            ShowPage(new HomeView());
        }

        //public MainViewModel(HeaderButton adminButton)
        //{
            

        //    ConfiguringApplicationForUserType();
        //}

        #endregion

        #region Fields

        private Visibility visibilityAdminButton = Visibility.Collapsed;

        private DelegateCommand? showAdminPageCommand;
        private MainView? view;
        #endregion

        #region Property

        public Visibility VisibilityAdminButton
        {
            get => visibilityAdminButton;
            set
            {
                visibilityAdminButton = value;
                OnPropertyChanged(nameof(VisibilityAdminButton));
            }
        }

        #endregion

        #region Methods

        private void ConfiguringApplicationForUserType()
        {
            if (CurrentUser.IsAdmin)
            {
                SettingUpForAdmin();
            }
            //}
            //else
            //{
            //    SettingUpForUser();
            //}
        }

        private void SettingUpForAdmin()
        {
            //App.ConnectionTheAdminViewModel();
            VisibilityAdminButton = Visibility.Visible;
        }

        //private void SettingUpForUser()
        //{
        //    App.ConnectionTheHomeViewModel();
        //}

        private void ChangeLanguage(string Language)
        {
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(
                        String.Format($"StaticFiles/Resources/Lang{Language}.xaml"),
                        UriKind.Relative
                    )
                }
            );
            CurrentUser.Language = Language;
        }

        private void ChangeTheme(string Theme)
        {
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(
                        String.Format($"StaticFiles/Themes/{Theme}.xaml"),
                        UriKind.Relative
                    )
                }
            );
            CurrentUser.Theme = Theme;
        }

        #endregion

        #region Commands

        #region Show menu page

        private DelegateCommand? showMenuCommand;

        public ICommand ShowMenuCommand
        {
            get
            {
                if (showMenuCommand == null)
                {
                    showMenuCommand = new DelegateCommand(/*App.ConnectionTheMenuViewModel*/() => 
                    { 
                        ShowPage(new MenuView()); 
                    });
                }
                return showMenuCommand;
            }
        }

        #endregion

        #region Exit account

        private DelegateCommand<object> exitAccountCommand;

        public ICommand ExitAccountCommand
        {
            get
            {
                if (exitAccountCommand == null)
                {
                    exitAccountCommand = new DelegateCommand<object>((object obj) =>
                    {
                        new LogIOView().Show();
                        (obj as MainView)?.Close();
                    });
                }
                return exitAccountCommand; 
            }
        }

        #endregion

        #region Show basket page

        private DelegateCommand? showBasketCommand;

        public ICommand ShowBasketCommand
        {
            get
            {
                if (showBasketCommand == null)
                {
                    showBasketCommand = new DelegateCommand(/*App.ConnectionTheBasketViewModel*/() =>
                    {
                        ShowPage(new BasketView());
                    });
                }
                return showBasketCommand;
            }
        }

        #endregion

        #region Exit for application

        private DelegateCommand? exitCommand;


        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new DelegateCommand(
                        System.Windows.Application.Current.Shutdown
                        );
                }
                return exitCommand;
            }
        }

        #endregion

        #region Show about page

        private DelegateCommand? showAboutCommand;

        public ICommand ShowAboutCommand
        {
            get
            {
                if (showAboutCommand == null)
                {
                    showAboutCommand = new DelegateCommand(/*App.ConnectionTheAboutView*/() => 
                    {
                        ShowPage(new AboutView());
                    });
                }
                return showAboutCommand;
            }
        }

        #endregion

        #region Show home page

        private DelegateCommand? showHomeCommand;

        public ICommand ShowHomeCommand
        {
            get
            {
                if (showHomeCommand == null)
                {
                    showHomeCommand = new DelegateCommand(/*App.ConnectionTheHomeViewModel*/() => 
                    {
                        ShowPage(new HomeView());
                    });
                }
                return showHomeCommand;
            }
        }

        #endregion

        #region Show profile page

        private DelegateCommand? showProfileCommand;

        public ICommand ShowProfileCommand
        {
            get
            {
                if (showProfileCommand == null)
                {
                    showProfileCommand = new DelegateCommand(/*App.ConnectionTheProfileViewModel*/() => 
                    {
                        ShowPage(new ProfileView());
                    });
                }
                return showProfileCommand;
            }
        }

        #endregion

        #region Show map page

        private DelegateCommand? showMapCommand;

        public ICommand ShowMapCommand
        {
            get
            {
                if (showMapCommand == null)
                {
                    showMapCommand = new DelegateCommand(/*App.ConnectionTheMapView*/ () => 
                    {
                        ShowPage(new MapView());
                    });
                }
                return showMapCommand;
            }
        }

        #endregion

        #region Show admin page


        public ICommand ShowAdminPageCommand
        {
            get
            {
                if (showAdminPageCommand == null)
                {
                    showAdminPageCommand = new DelegateCommand(/*App.ConnectionTheAdminViewModel*/() =>
                    {
                        ShowPage(new AdminView());
                    });
                }
                return showAdminPageCommand;
            }
        }

        #endregion

        #region Toggle Image Theme Command

        private DelegateCommand toggleImageThemeCommand;
        public ICommand ToggleImageThemeCommand
        {
            get
            {
                if (toggleImageThemeCommand == null)
                {
                    toggleImageThemeCommand = new DelegateCommand(() => 
                    {
                        if (isThemeFirstImage)
                        {
                            CurrentThemeImage = new BitmapImage(new Uri("\\StaticFiles\\Img\\ThemeLight.png", UriKind.Relative));
                            new Task(() => { ChangeTheme("Light"); }).Start();
                            isThemeFirstImage = false;
                        }
                        else
                        { 
                            CurrentThemeImage = new BitmapImage(new Uri("\\StaticFiles\\Img\\ThemeDark.png", UriKind.Relative));
                            new Task(() => { ChangeTheme("Dark"); }).Start();
                            isThemeFirstImage = true;
                        }
                    });
                }
                return toggleImageThemeCommand;
            }
        }
        private DelegateCommand toggleImageLangCommand;
        public ICommand ToggleImageLangCommand
        {
            get
            {
                if (toggleImageLangCommand == null)
                {
                    toggleImageLangCommand = new DelegateCommand(() =>
                    {
                        if (isLangFirstImage)
                        {
                            CurrentLangImage = new BitmapImage(new Uri("\\StaticFiles\\Img\\rus.png", UriKind.Relative));
                            ChangeLanguage(".ru-RU");
                            isLangFirstImage = false;
                        }
                        else
                        {
                            CurrentLangImage = new BitmapImage(new Uri("\\StaticFiles\\Img\\en.png", UriKind.Relative));
                            ChangeLanguage(".en-US");
                            isLangFirstImage = true;
                        }
                    });
                }
                return toggleImageLangCommand;
            }
        }
        private ImageSource currentThemeImage = new BitmapImage(new Uri("\\StaticFiles\\Img\\ThemeDark.png", UriKind.Relative));
        public ImageSource CurrentThemeImage
        {
            get => currentThemeImage;
            set
            {
                currentThemeImage = value;
                OnPropertyChanged(nameof(CurrentThemeImage));
            }
        } 
        private ImageSource currentLangImage = new BitmapImage(new Uri("\\StaticFiles\\Img\\en.png", UriKind.Relative));
        public ImageSource CurrentLangImage
        {
            get => currentLangImage;
            set
            {
                currentLangImage = value;
                OnPropertyChanged(nameof(CurrentLangImage));
            }
        }
        private bool isThemeFirstImage = true;
        private bool isLangFirstImage = true;

        #endregion

        #endregion
    }
}
