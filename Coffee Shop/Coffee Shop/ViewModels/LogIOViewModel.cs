using Coffee_Shop.Views.Pages;
using CoffeeShop.Commands;
using CoffeeShop.Data.Models;
using CoffeeShop.Views;
using CoffeShop.Data;
using CoffeShop.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Coffee_Shop.ViewModels
{
    internal class LogIOViewModel : ViewModelBase
    {
        public Person person { get; set; } = new();
        private List<Person> persons { get; set; } = InitializePersonsCollection();
        private string passwordConfirmation { get; set; } = string.Empty;

        private LogIOView? IOView;

        public LogIOViewModel(ref LogIOView logIOView)
        {
            this.IOView = logIOView;
        }

        private static List<Person> InitializePersonsCollection()
        {
            var temp = new List<Person>();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Persons.ToList().ForEach(x => temp.Add(x));
            }
            return temp;
        }

        #region Property
        public string UserName
        {
            get
            {
                return person.UserName;
            }
            set
            {
                person.UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get
            {
                return person.Password;
            }
            set
            {
                person.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string PasswordConfirmation
        {
            get
            {
                return passwordConfirmation;
            }
            set
            {
                passwordConfirmation = value;
                OnPropertyChanged(nameof(PasswordConfirmation));
            }
        } 
        #endregion

        #region Commands

        #region Sign In

        private DelegateCommand? signInCommand;

        public ICommand SignInCommand
        {
            get
            {
                if (signInCommand == null)
                {
                    signInCommand = new DelegateCommand(SignIn);
                }
                return signInCommand;
            }
        }

        private void SignIn()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (persons.Any(x => x.UserName == person.UserName && x.Password == person.Password))
                {
                    person = persons.First(x => x.UserName == person.UserName);

                    InitializingTheUserType();

                    App.СonfiguringTheMainView(UserType);
                    this.IOView?.Close();
                }
                else
                {
                    MessageBox.Show("Данные введены неверно.");
                }
            }
        }
        private string UserType = string.Empty; // стрем переделать

        private void InitializingTheUserType()
        {
            if(person.IsAdmin)
            {
                UserType = "Admin";
            }    
            else
            {
                UserType = "User";
            }
        }



        #endregion

        #region Join

        private DelegateCommand? joinCommand;

        public ICommand JoinCommand
        {
            get
            {
                if (joinCommand == null)
                {
                    joinCommand = new DelegateCommand(Join);
                }
                return joinCommand;
            }
        }

        private void Join()
        {
            if (person.Password == PasswordConfirmation)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    person.Id = db.Persons.Count() + 1;
                    db.Persons.AddRange(person);
                    db.SaveChanges();
                }
                new MainView().Show();
            }
        }

        #endregion

        #region Clear

        private DelegateCommand? clearCommand;

        public ICommand ClearCommand
        {
            get
            {
                if (clearCommand == null)
                {
                    clearCommand = new DelegateCommand(Clear);
                }
                return clearCommand;
            }
        }

        private void Clear()
        {
            person.Id = 0;
            person.UserName = string.Empty;
            person.Password = string.Empty;
            passwordConfirmation = string.Empty;
        }


        #endregion

        #endregion
    }
}
