using Coffee_Shop.Database;
using Coffee_Shop.Models;
using Coffee_Shop.Views;
using CoffeeShop.Commands;
using DataValidation;
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

        private string errorCVV = string.Empty;
        private string errorNumber = string.Empty;
        private string errorHolderName = string.Empty;
        private string errorCardPeriod = string.Empty;

        private DelegateCommand<BankCardView>? goToTheCheckoutCommand;

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
                    goToTheCheckoutCommand = new DelegateCommand<BankCardView>((BankCardView view) =>
                    {
                        if (IsBankCardValidate())
                        {
                            CurrentUser.BankCard = this.bankCard;
                            Db.Save();
                            view?.Close();
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

        #endregion
    }
}
