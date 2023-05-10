using CoffeeShop.Commands;
using CoffeeShop.Data.Models;
using CoffeeShop.Views;
using DataEncryption;
using DataValidation;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using static DataValidation.Validator;

namespace Coffee_Shop.ViewModels
{
    internal class LogIOViewModel : ViewModelBase
    {
        #region Constructors

        public LogIOViewModel()
        {
            this.users = Db.GetUserList().ToList();
            this.validator = new Validator(this);
        }

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

            //return validator.CheckingForPasswordLength(Password, nameof(ErrorPasswordMessage)) &
            //    validator.ValidationOfPlainText(UserName, nameof(ErrorUserNameMessage));
        }
        private bool IsTheNewUserDataCorrect()
        {
            return IsTheUserDataCorrect() & validator.Verify(ValidationBased.Email, Email, nameof(ErrorEmailMessage)); //validator.ValidationOfEmail(Email, nameof(ErrorEmailMessage));
        }

        #endregion

        #region Fields

        private List<User> users { get; } = new();
        private User user = new();
        private Validator validator;
        private string errorPasswordMessage = string.Empty;
        private string errorUserNameMessage = string.Empty;
        private string errorEmailMessage = string.Empty;

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

        private DelegateCommand<LogIOView>? logInCommand;

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

        private DelegateCommand<LogIOView>? joinCommand;

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
                            if (!users.Any(x => x.UserName == user.UserName && x.Password == user.Password))
                            {
                                user.Password = CryptographerBuilder.Encrypt(user.Password);

                                Db.CreateUser(user);
                                Db.Save();
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

        #endregion
    }
}
