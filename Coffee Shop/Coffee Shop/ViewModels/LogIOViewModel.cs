using APIForEmail;
using Coffee_Shop.Database;
using Coffee_Shop.Models;
using CoffeeShop.Commands;
using CoffeeShop.Data.Models;
using CoffeeShop.Views;
using DataEncryption;
using DataValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static DataValidation.Validator;

namespace Coffee_Shop.ViewModels
{
    internal class LogIOViewModel : ViewModelBase
    {
        #region Constructors

        public LogIOViewModel()
        {
            ChangeTheme("Dark");
            ChangeLanguage(".en-US");
            this.Db = new UnitOfWork();
            this.users = Db.Users.GetIEnumerable().ToList();
            this.validator = new Validator(this);
        }

        #endregion

        #region Fields

        private UnitOfWork Db;
        private List<User> users { get; set; } = new();
        private User user = new();
        private Validator validator;
        private string errorPasswordMessage = string.Empty;
        private string errorUserNameMessage = string.Empty;
        private string errorEmailMessage = string.Empty;

        private DelegateCommand? exitCommand;
        private DelegateCommand<LogIOView>? joinCommand;
        private DelegateCommand<LogIOView>? logInCommand;

        #endregion

        #region Methods

        private void GoToTheMainPage(LogIOView view)
        {
            ViewModelBase.CurrentUser = users.Find(x => x.UserName == user.UserName) ?? user;
            ShowMainWindow();

            view?.Close();
        }

        private bool IsTheUserDataCorrect()
        {
            return validator.Verify(ValidationBased.Password, Password, nameof(ErrorPasswordMessage)) &
                validator.Verify(ValidationBased.TextTo, UserName, nameof(ErrorUserNameMessage));
        }
        private bool IsTheNewUserDataCorrect()
        {
            return IsTheUserDataCorrect() & validator.Verify(ValidationBased.Email, Email, nameof(ErrorEmailMessage));
        }

        private Notification GetDefaultNotification()
        {
            Notification notification = new Notification();
            notification.Title = $"{user.UserName}, welcome to the Coffee Shop!";
            notification.Content =
                "Thanks to our application, you can view the menu catalog, " +
                "place orders, learn the history of the company, as well as " +
                "quickly learn about news and new products thanks to our newsletter.";
            notification.Date = DateTime.Now;
            return notification;
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
        }

        #endregion

        #region Property

        public string Password
        {
            get
            {
                return user.Password;
            }
            set
            {
                user.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string UserName
        {
            get
            {
                return user.UserName;
            }
            set
            {
                user.UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Email
        {
            get
            {
                return user.Email;
            }
            set
            {
                user.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        #region Errors

        public string ErrorEmailMessage
        {
            get
            {
                return errorEmailMessage;
            }
            set
            {
                errorEmailMessage = value;
                OnPropertyChanged(nameof(ErrorEmailMessage));
            }
        }

        public string ErrorPasswordMessage
        {
            get
            {
                return errorPasswordMessage;
            }
            set
            {
                errorPasswordMessage = value;
                OnPropertyChanged(nameof(ErrorPasswordMessage));
            }
        }

        public string ErrorUserNameMessage
        {
            get
            {
                return errorUserNameMessage;
            }
            set
            {
                errorUserNameMessage = value;
                OnPropertyChanged(nameof(ErrorUserNameMessage));
            }
        }

        #endregion

        #endregion

        #region Commands

        #region Log in

        public ICommand LogInCommand
        {
            get
            {
                if (logInCommand == null)
                {
                    logInCommand = new DelegateCommand<LogIOView>((LogIOView obj) =>
                    {
                        if (IsTheUserDataCorrect())
                        {
                            if (users.Any(x => x.UserName == user.UserName && CryptographerBuilder.Decrypt(x.Password) == user.Password))
                            {
                                GoToTheMainPage(obj);
                            }
                            else
                            {
                                SendToModalWindow("There is no such user.");
                            }
                        }
                    });
                }
                return logInCommand;
            }
        }

        #endregion

        #region Join

        public ICommand JoinCommand
        {
            get
            {
                if (joinCommand == null)
                {
                    joinCommand = new DelegateCommand<LogIOView>((LogIOView obj) =>
                    {
                        if (IsTheNewUserDataCorrect())
                        {
                            if (!users.Any(x => x.UserName == user.UserName))
                            {
                                user.Password = CryptographerBuilder.Encrypt(user.Password);
                                user.Notifications = new() { GetDefaultNotification() };

                                new Task(SendMailMessage).Start();


                                Db.Users.Create(user);
                                Db?.Save();
                                GoToTheMainPage(obj);
                            }
                            else
                            {
                                SendToModalWindow("A user with this name already exists.");
                            }
                        }
                    });
                }
                return joinCommand;
            }
        }

        private void SendMailMessage()
        {
            MailBuilder mail = new MailBuilder("Welcome to Coffee Shop!", TypeOfLetter.Welcome, Email,
                                    $"Добро пожаловать к нам, {UserName}! У нас ты можешь не только вкусно выпить кофе, но еще и закусить. Мы предлагаем Вам широкий ассортимент товаров с возможностью фильтрации и доставку в любую точку твоего города!");
            mail.Send();
        }

        #endregion

        #region Exit

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

        #endregion
    }
}
