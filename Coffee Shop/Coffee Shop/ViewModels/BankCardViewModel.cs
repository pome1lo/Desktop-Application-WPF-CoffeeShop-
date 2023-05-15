using Coffee_Shop.Database;
using Coffee_Shop.Models;
using CoffeeShop.Commands;
using DataValidation;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using static DataValidation.Validator;

namespace Coffee_Shop.ViewModels
{
    internal class BankCardViewModel : ViewModelBase
    {
        #region Constructor

        public BankCardViewModel()
        {
            this.Db = new UnitOfWork();
            this.validator = new Validator(this);
        }

        #endregion

        #region Fields

        private BankCard bankCard = new();

        private UnitOfWork Db;
        private Validator validator;

        private string errorCVV = string.Empty ;
        private string errorNumber = string.Empty;
        private string errorHolderName = string.Empty;
        private string errorCardPeriod = string.Empty;

        private DelegateCommand? goToTheCheckoutCommand;

        #endregion

        #region Property

        #region Card info

        public string CVV
        {
            get => bankCard.CVV;
            set
            {
                bankCard.CVV = value;
                OnPropertyChanged(nameof(CVV));
            }
        }

        public string HolderName
        {
            get => bankCard.HolderName;
            set
            {
                bankCard.HolderName = value;
                OnPropertyChanged(nameof(HolderName));
            }
        }

        public string Number
        {
            get => bankCard.Number;
            set
            {
                bankCard.Number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        public string CardPeriod
        {
            get => bankCard.CardPeriod;
            set
            {
                bankCard.CardPeriod = value;
                OnPropertyChanged(nameof(CardPeriod));
            }
        }


        #endregion

        #region Errors

        public string ErrorCVV
        {
            get => errorCVV;
            set
            {
                errorCVV = value;
                OnPropertyChanged(nameof(ErrorCVV));
            }
        }

        public string ErrorHolderName
        {
            get => errorHolderName;
            set
            {
                errorHolderName = value;
                OnPropertyChanged(nameof(ErrorHolderName));
            }
        }

        public string ErrorNumber
        {
            get => errorNumber;
            set
            {
                errorNumber = value;
                OnPropertyChanged(nameof(ErrorNumber));
            }
        }
        
        public string ErrorCardPeriod
        {
            get => errorCardPeriod;
            set
            {
                errorCardPeriod = value;
                OnPropertyChanged(nameof(ErrorCardPeriod));
            }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand GoToTheCheckoutCommand
        {
            get
            {
                if (goToTheCheckoutCommand == null)
                {
                    goToTheCheckoutCommand = new DelegateCommand(() =>
                    {
                        if (IsBankCardValidate())//&& IsNummberValidate(Number))
                        {
                            SendToModalWindow("ВСЕ ОК");
                            CurrentUser.BankCard = this.bankCard;
                            Db.Save();
                        }
                    });
                }
                return goToTheCheckoutCommand;
            }
        }



        #endregion


        #region Methods

        private bool IsBankCardValidate()
        {
            return validator.Verify(ValidationBased.BankCVV, CVV, nameof(ErrorCVV)) &
                validator.Verify(ValidationBased.BankPeriod, CardPeriod, nameof(ErrorCardPeriod)) &
                validator.Verify(ValidationBased.BankNumber, Number, nameof(ErrorNumber)) &
                validator.Verify(ValidationBased.TextTo, HolderName, nameof(ErrorHolderName))
                ;
        }
        //private bool IsHolderNameValide(string value)
        //{
        //    Regex regex = new Regex("^[a-zA-Z]+$");
        //    if (!regex.IsMatch(value))
        //    {
        //        ErrorHolderName = "Incorrect Holdername";
        //        return false;
        //    }
        //    ErrorHolderName = "";
        //    return true;
        //}

        ////private bool IsNummberValidate(string value)
        ////{
        ////    Regex regex = new Regex();
        ////    if (!regex.IsMatch(value))
        ////    {
        ////        ErrorNumber = "Incorrect number";
        ////        return false;
        ////    }
        ////    ErrorNumber = "";
        ////    return true;
        ////}

        //private bool IsCvvValidate(short value)
        //{
        //    if (value > 1000 || value < 0)
        //    {
        //        ErrorCVV = "Incorrect CVV";
        //        return false;
        //    }
        //    ErrorCVV = "";
        //    return true;
        //}

        #endregion
    }
}
