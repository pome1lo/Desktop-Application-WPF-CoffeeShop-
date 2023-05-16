using Coffee_Shop.Database;
using Coffee_Shop.Views.Pages;
using CoffeeShop.Commands;
using CoffeeShop.Views;
using System;
using System.Threading.Tasks;
using System.Windows;
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
            Db = new UnitOfWork();
            ChangeTheme(CurrentUser.Theme);
            ChangeLanguage(CurrentUser.Language);
            ConfiguringApplicationForUserType();
            ShowPage(new HomeView());
        }

        #endregion

        #region Fields

        private Visibility visibilityAdminButton = Visibility.Collapsed;

        private bool isThemeFirstImage = true;
        private bool isLangFirstImage = true;

        private UnitOfWork Db;
        private DelegateCommand? showAdminPageCommand;
        private MainView? view;

        private ImageSource currentThemeImage = new BitmapImage(new Uri("\\StaticFiles\\Img\\ThemeDark.png", UriKind.Relative));
        private ImageSource currentLangImage = new BitmapImage(new Uri("\\StaticFiles\\Img\\en.png", UriKind.Relative));

        private DelegateCommand? showMenuCommand;
        private DelegateCommand<object> exitAccountCommand;
        private DelegateCommand? exitCommand;
        private DelegateCommand? showProfileCommand;
        private DelegateCommand? showMapCommand;
        private DelegateCommand? showAboutCommand;
        private DelegateCommand toggleImageLangCommand;
        private DelegateCommand? showHomeCommand;
        private DelegateCommand toggleImageThemeCommand;
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

        public ImageSource CurrentThemeImage
        {
            get => currentThemeImage;
            set
            {
                currentThemeImage = value;
                OnPropertyChanged(nameof(CurrentThemeImage));
            }
        }

        public ImageSource CurrentLangImage
        {
            get => currentLangImage;
            set
            {
                currentLangImage = value;
                OnPropertyChanged(nameof(CurrentLangImage));
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
        }

        private void SettingUpForAdmin()
        {
            VisibilityAdminButton = Visibility.Visible;
        }

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
            Db.Save();
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
            Db.Save();
        }

        #endregion

        #region Commands

        #region Show menu page

        public ICommand ShowMenuCommand
        {
            get
            {
                if (showMenuCommand == null)
                {
                    showMenuCommand = new DelegateCommand(() =>
                    {
                        ShowPage(new MenuView());
                    });
                }
                return showMenuCommand;
            }
        }

        #endregion

        #region Exit account

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

        #region Exit for application

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

        public ICommand ShowAboutCommand
        {
            get
            {
                if (showAboutCommand == null)
                {
                    showAboutCommand = new DelegateCommand(() =>
                    {
                        ShowPage(new AboutView());
                    });
                }
                return showAboutCommand;
            }
        }

        #endregion

        #region Show home page

        public ICommand ShowHomeCommand
        {
            get
            {
                if (showHomeCommand == null)
                {
                    showHomeCommand = new DelegateCommand(() =>
                    {
                        ShowPage(new HomeView());
                    });
                }
                return showHomeCommand;
            }
        }

        #endregion

        #region Show profile page

        public ICommand ShowProfileCommand
        {
            get
            {
                if (showProfileCommand == null)
                {
                    showProfileCommand = new DelegateCommand(() =>
                    {
                        ShowPage(new ProfileView());
                    });
                }
                return showProfileCommand;
            }
        }

        #endregion

        #region Show map page

        public ICommand ShowMapCommand
        {
            get
            {
                if (showMapCommand == null)
                {
                    showMapCommand = new DelegateCommand(() =>
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
                    showAdminPageCommand = new DelegateCommand(() =>
                    {
                        ShowPage(new AdminView());
                    });
                }
                return showAdminPageCommand;
            }
        }

        #endregion

        #region Toggle Image Theme Command

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
      
        #endregion

        #endregion
    }
}
