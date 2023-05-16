using Coffee_Shop.Database;
using Coffee_Shop.Models;
using CoffeeShop.Commands;
using DataEncryption;
using DataValidation;
using LiveCharts.SeriesAlgorithms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using static DataValidation.Validator;

namespace Coffee_Shop.ViewModels
{
    internal class ProfileViewModel : ViewModelBase
    {
        #region Constructor

        public ProfileViewModel()
        {
            this.Db = new UnitOfWork();
            this.validator = new Validator(this);
        }

        #endregion

        #region Fields

        private UnitOfWork Db;

        private Validator validator;
        private string email = CurrentUser.Email;
        private string errorEmail = string.Empty;
        private string newPassword = string.Empty;
        private string errorUserName = string.Empty;
        private string errorTelegram = string.Empty;
        private string errorVkontakte = string.Empty;
        private string errorInstagram = string.Empty;
        private string confirmPassword = string.Empty;
        private string errorNewPassword = string.Empty;
        private string userName = CurrentUser.UserName;
        private string errorConfirmPassword = string.Empty;
        private string telegram = CurrentUser.SocialNetworks.Telegram;
        private string vkontakte = CurrentUser.SocialNetworks.Vkontakte;
        private string instagram = CurrentUser.SocialNetworks.Instagram;

        private DelegateCommand? goToTheVKCommand;
        private DelegateCommand profileSaveChangesCommand;
        private DelegateCommand saveChangesCommand;
        private DelegateCommand<Notification> closeItemCardCommand;
        private DelegateCommand? goToTheInstagramCommand;
        private DelegateCommand? goToTheTelegramCommand;

        #endregion

        #region Property

        #region Errors

        public string ErrorNewPassword
        {
            get
            {
                return errorNewPassword;
            }
            set
            {
                errorNewPassword = value;
                OnPropertyChanged(nameof(ErrorNewPassword));
            }
        }

        public string ErrorConfirmPassword
        {
            get
            {
                return errorConfirmPassword;
            }
            set
            {
                errorConfirmPassword = value;
                OnPropertyChanged(nameof(ErrorConfirmPassword));
            }
        }


        public string ErrorUserName
        {
            get
            {
                return errorUserName;
            }
            set
            {
                errorUserName = value;
                OnPropertyChanged(nameof(ErrorUserName));
            }
        }

        public string ErrorEmail
        {
            get
            {
                return errorEmail;
            }
            set
            {
                errorEmail = value;
                OnPropertyChanged(nameof(ErrorEmail));
            }
        }

        public string ErrorVkontakte
        {
            get
            {
                return errorVkontakte;
            }
            set
            {
                errorVkontakte = value;
                OnPropertyChanged(nameof(ErrorVkontakte));
            }
        }

        public string ErrorInstagram
        {
            get
            {
                return errorInstagram;
            }
            set
            {
                errorInstagram = value;
                OnPropertyChanged(nameof(ErrorInstagram));
            }
        }

        public string ErrorTelegram
        {
            get
            {
                return errorTelegram;
            }
            set
            {
                errorTelegram = value;
                OnPropertyChanged(nameof(ErrorTelegram));
            }
        }

        #endregion

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Vkontakte
        {
            get
            {
                return vkontakte;
            }
            set
            {
                vkontakte = value;
                OnPropertyChanged(nameof(Vkontakte));
            }
        }

        public string Instagram
        {
            get
            {
                return instagram;
            }
            set
            {
                instagram = value;
                OnPropertyChanged(nameof(Instagram));
            }
        }

        public string Telegram
        {
            get
            {
                return telegram;
            }
            set
            {
                telegram = value;
                OnPropertyChanged(nameof(Telegram));
            }
        }

        public string Picture
        {
            get => CurrentUser.Picture;
            set
            {
                CurrentUser.Picture = value;
                //Db.Save();
                OnPropertyChanged(nameof(Picture));
            }
        }

        public string CurrentPassword
        {
            get => new String('●', CryptographerBuilder.Decrypt(CurrentUser.Password).Count());
            set
            {
                CurrentUser.Password = value;
                OnPropertyChanged(nameof(CurrentPassword));
            }
        }

        public string NewPassword
        {
            get => newPassword;
            set
            {
                newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public List<Notification>? Notifications
        {
            get => CurrentUser.Notifications;
            set
            {
                CurrentUser.Notifications = value;
                OnPropertyChanged(nameof(Notifications));
            }
        }

        #endregion

        #region Commands

        #region Go to the social networks..

        #region VK

        public ICommand GoToTheVKCommand
        {
            get
            {
                if (goToTheVKCommand == null)
                {
                    goToTheVKCommand = new DelegateCommand(
                        () => { FollowTheLink(CurrentUser.SocialNetworks.Vkontakte); }
                    );
                }
                return goToTheVKCommand;
            }
        }

        #endregion

        #region Instagramm

        public ICommand GoToTheInstagramCommand
        {
            get
            {
                if (goToTheInstagramCommand == null)
                {
                    goToTheInstagramCommand = new DelegateCommand(
                        () => { FollowTheLink(CurrentUser.SocialNetworks.Instagram); }
                    );
                }
                return goToTheInstagramCommand;
            }
        }

        #endregion

        #region Telegram

        public ICommand GoToTheTelegramCommand
        {
            get
            {
                if (goToTheTelegramCommand == null)
                {
                    goToTheTelegramCommand = new DelegateCommand(
                        () => { FollowTheLink(CurrentUser.SocialNetworks.Telegram); }
                    );
                }
                return goToTheTelegramCommand;
            }
        }


        #endregion

        #endregion

        #region Password save changes

        public ICommand SaveChangesCommand
        {
            get
            {
                if (saveChangesCommand == null)
                {
                    saveChangesCommand = new DelegateCommand(
                        () =>
                        {
                            if (IsThePasswordCorrect())
                            {
                                CurrentUser.Password = CryptographerBuilder.Encrypt(NewPassword);
                                Db.Save();
                                SendToModalWindow("You have successfully changed your password.");
                            }
                        }
                    );
                }
                return saveChangesCommand;
            }
        }

        #endregion

        #region Close item card 

        public ICommand CloseItemCardCommand
        {
            get
            {
                if (closeItemCardCommand == null)
                {
                    closeItemCardCommand = new DelegateCommand<Notification>((Notification notification) =>
                    {
                        CurrentUser.Notifications.Remove(notification);
                        Db.Save();
                        Notifications = new(CurrentUser.Notifications);
                    });
                }
                return closeItemCardCommand;
            }
        }

        #endregion

        #region Profile save changes

        public ICommand ProfileSaveChangesCommand
        {
            get
            {
                if (profileSaveChangesCommand == null)
                {
                    profileSaveChangesCommand = new DelegateCommand(
                        () =>
                        {
                            if (IsTheProfileCorrect())
                            {
                                if (Db.Users.GetIEnumerable().Any(x => x.UserName == UserName))
                                {
                                    SendToModalWindow("Incorrect data.");
                                }
                                else
                                {
                                    CurrentUser.Email = Email;
                                    CurrentUser.UserName = UserName;
                                    CurrentUser.SocialNetworks.Telegram = Telegram;
                                    CurrentUser.SocialNetworks.Instagram = Instagram;
                                    CurrentUser.SocialNetworks.Vkontakte = Vkontakte;

                                    Db.Save();
                                    SendToModalWindow("Your data has been successfully changed");
                                }
                            }
                        }
                    );
                }
                return profileSaveChangesCommand;
            }
        }

        #endregion

        #endregion

        #region Methods

        private bool IsThePasswordCorrect()
        {
            if (validator.Verify(Validator.ValidationBased.Password, NewPassword, nameof(ErrorNewPassword)) &
                validator.Verify(Validator.ValidationBased.Password, ConfirmPassword, nameof(ErrorConfirmPassword)))
            {
                if (NewPassword == ConfirmPassword)
                {
                    return true;
                }
                else
                {
                    SendToModalWindow("The confirmation password does not match the new password.");
                }
            }
            return false;

        }


        private bool IsTheProfileCorrect()
        {
            return validator.Verify(ValidationBased.TextTo, UserName, nameof(ErrorUserName)) &
                    validator.Verify(ValidationBased.Email, Email, nameof(ErrorEmail)) &
                    validator.Verify(ValidationBased.Links, Vkontakte, nameof(ErrorVkontakte)) &
                    validator.Verify(ValidationBased.Links, Instagram, nameof(ErrorInstagram)) &
                    validator.Verify(ValidationBased.Links, Telegram, nameof(ErrorTelegram));
        }

        private void FollowTheLink(string link)
        {
            if (String.IsNullOrEmpty(link))
            {
                SendToModalWindow("The link is not specified.");
            }
            else
            {
                Process.Start(new ProcessStartInfo(link) { UseShellExecute = true });
            }
        }

        #endregion
    }
}
