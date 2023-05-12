using CoffeeShop.Commands;
using CoffeeShop.Views;
using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Coffee_Shop.Models;
using CoffeeShop.Data.Models;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Reflection;
using DataValidation;
using static DataValidation.Validator;
using Coffee_Shop.Models.Entities;
using Coffee_Shop.Views;

namespace Coffee_Shop.ViewModels
{
    internal class CreateElementViewModel : ViewModelBase
    {
        #region Constructor 

        public CreateElementViewModel() 
        {
            this.validator = new Validator(this);
            product.ProductType = Db.GetProductType("Coffee");
        }

        #endregion

        #region Fields

        private Product product = new();
        private News news = new();
        private User user = new();
        private Validator validator;
        private bool whetherToSendANewsletter;

        private string errorProductName = string.Empty;
        private string errorProductPrice = string.Empty;
        private string errorProductSodium = string.Empty;
        private string errorProductProtein = string.Empty;
        private string errorProductCalories = string.Empty;
        private string errorProductTotalFat = string.Empty;
        private string errorProductTransFat = string.Empty;
        private string errorProductCaffeine = string.Empty;
        private string errorProductCholesterol = string.Empty;
        private string errorProductSaturatedFat = string.Empty;
        private string errorProductTotalCarbohydrates = string.Empty;

        private string errorUserName = string.Empty;
        private string errorPassword = string.Empty;

        private string errorTitle = string.Empty;
        private string errorContent = string.Empty;
        private string errorImg = string.Empty;

        #endregion

        #region Property

        #region Create new product

        public string ProductName
        {
            get => product.Name;
            set
            {
                product.Name = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }
        public decimal ProductPrice
        {
            get => product.Price;
            set
            {
                product.Price = value;
                OnPropertyChanged(nameof(ProductPrice));
            }
        }
        public string ProductImg
        {
            get => product.Img;
            set
            {
                product.Img = @"\StaticFiles\Img\" + value;
                OnPropertyChanged(nameof(ProductImg));
            }
        }
        public ushort ProductCalories
        {
            get => product.Calories;
            set
            {
                product.Calories = value;
                OnPropertyChanged(nameof(ProductCalories));
            }
        }
        public ushort ProductTotalFat
        {
            get => product.Description.TotalFat;
            set
            {
                product.Description.TotalFat = value;
                OnPropertyChanged(nameof(ProductTotalFat));
            }
        }
        public ushort ProductSaturatedFat
        {
            get => product.Description.SaturatedFat;
            set
            {
                product.Description.SaturatedFat = value;
                OnPropertyChanged(nameof(ProductSaturatedFat));
            }
        }
        public ushort ProductTransFat
        {
            get => product.Description.TransFat;
            set
            {
                product.Description.TransFat = value;
                OnPropertyChanged(nameof(ProductTransFat));
            }
        }
        public ushort ProductCholesterol
        {
            get => product.Description.Cholesterol;
            set
            {
                product.Description.Cholesterol = value;
                OnPropertyChanged(nameof(ProductCholesterol));
            }
        }
        public ushort ProductSodium
        {
            get => product.Description.Sodium;
            set
            {
                product.Description.Sodium = value;
                OnPropertyChanged(nameof(ProductSodium));
            }
        }
        public ushort ProductTotalCarbohydrates
        {
            get => product.Description.TotalCarbohydrates;
            set
            {
                product.Description.TotalCarbohydrates = value;
                OnPropertyChanged(nameof(ProductTotalCarbohydrates));
            }
        }
        public ushort ProductProtein
        {
            get => product.Description.Protein;
            set
            {
                product.Description.Protein = value;
                OnPropertyChanged(nameof(ProductProtein));
            }
        }
        public ushort ProductCaffeine
        {
            get => product.Description.Caffeine;
            set
            {
                product.Description.Caffeine = value;
                OnPropertyChanged(nameof(ProductCaffeine));
            }
        }

        public ProductType ProductType
        {
            get => product.ProductType;
            set
            {
                product.ProductType = value;
                OnPropertyChanged(nameof(ProductType));
            }
        }

        #endregion
        #region Errors

        public string ErrorProductName
        {
            get => errorProductName;
            set
            {
                errorProductName = value;
                OnPropertyChanged(nameof(ErrorProductName));
            }
        }

        public string ErrorProductPrice
        {
            get => errorProductPrice;
            set
            {
                errorProductPrice = value; 
                OnPropertyChanged(nameof(ErrorProductPrice));
            }
        }

        public string ErrorProductCalories
        {
            get => errorProductCalories;
            set
            {
                errorProductCalories = value; 
                OnPropertyChanged(nameof(ErrorProductCalories));
            }
        }
        public string ErrorProductTotalFat
        {
            get => errorProductTotalFat;
            set
            {
                errorProductTotalFat = value; 
                OnPropertyChanged(nameof(ErrorProductTotalFat));
            }
        } 
        public string ErrorProductSaturatedFat
        {
            get => errorProductSaturatedFat;
            set
            {
                errorProductSaturatedFat = value; 
                OnPropertyChanged(nameof(ErrorProductSaturatedFat));
            }
        }
        public string ErrorProductTransFat
        {
            get => errorProductTransFat;
            set
            {
                errorProductTransFat = value; 
                OnPropertyChanged(nameof(ErrorProductTransFat));
            }
        }
        public string ErrorProductCholesterol
        {
            get => errorProductCholesterol;
            set
            {
                errorProductCholesterol = value; 
                OnPropertyChanged(nameof(ErrorProductCholesterol));
            }
        }
        public string ErrorProductSodium
        {
            get => errorProductSodium;
            set
            {
                errorProductSodium = value; 
                OnPropertyChanged(nameof(ErrorProductSodium));
            }
        }
        public string ErrorProductTotalCarbohydrates
        {
            get => errorProductTotalCarbohydrates;
            set
            {
                errorProductTotalCarbohydrates = value; 
                OnPropertyChanged(nameof(ErrorProductTotalCarbohydrates));
            }
        }
        public string ErrorProductProtein
        {
            get => errorProductProtein;
            set
            {
                errorProductProtein = value; 
                OnPropertyChanged(nameof(ErrorProductProtein));
            }
        }
        public string ErrorProductCaffeine
        {
            get => errorProductCaffeine;
            set
            {
                errorProductCaffeine = value; 
                OnPropertyChanged(nameof(ErrorProductCaffeine));
            }
        }


        #endregion

        #region Create new user

        public string UserName
        {
            get => user.UserName;
            set
            {
                user.UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get => user.Password;
            set
            {
                user.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        #endregion
        #region Errors

        public string ErrorUserName
        {
            get => errorUserName;
            set
            {
                errorUserName = value;
                OnPropertyChanged(nameof(ErrorUserName));
            }
        }
        
        public string ErrorPassword
        {
            get => errorPassword;
            set
            {
                errorPassword = value;
                OnPropertyChanged(nameof(ErrorPassword));
            }
        }

        #endregion

        #region Create new news

        public string Title
        {
            get => news.Title;
            set
            {
                news.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Content
        {
            get => news.Content;
            set
            {
                news.Content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        public string? Img
        {
            get => news.Img;
            set
            {
                news.Img = value;
                OnPropertyChanged(nameof(Img));
            }
        }

        #endregion
        #region Errors

        public string ErrorTitle
        {
            get => errorTitle;
            set
            {
                errorTitle = value;
                OnPropertyChanged(nameof(ErrorTitle));
            }
        }

        //public string ErrorDate
        //{
        //    get => errorDate;
        //    set
        //    {
        //        errorDate = value;
        //        OnPropertyChanged(nameof(ErrorDate));
        //    }
        //}

        public string ErrorContent
        {
            get => errorContent;
            set
            {
                errorContent = value;
                OnPropertyChanged(nameof(ErrorContent));
            }
        }

        public string ErrorImg
        {
            get => errorImg;
            set
            {
                errorImg = value;
                OnPropertyChanged(nameof(ErrorImg));
            }
        }

        #endregion


        #region Whether to send a newsletter

        public bool WhetherToSendANewsletter
        {
            get => whetherToSendANewsletter;
            set
            {
                whetherToSendANewsletter = value;
                OnPropertyChanged(nameof(WhetherToSendANewsletter));
            }
        }

        #endregion

        #endregion

        #region Commands

        #region Add New Product

        private DelegateCommand<CreateElement>? addNewProductCommand;

        public ICommand AddNewProductCommand
        {
            get
            {
                if (addNewProductCommand == null)
                {
                    addNewProductCommand = new DelegateCommand<CreateElement>((CreateElement view) =>
                    {
                        if (NewProductValidate())
                        {
                            //product.ProductType = Db.GetProductType("Coffee");
                            Db.CreateProduct(product);
                            Db.Save();
                            SendToModalWindow("The product was successfully added to the database.");
                            view.Close();
                            if (WhetherToSendANewsletter == true)
                            {
                                // почта отправка епта
                            }
                        }
                    });
                }
                return addNewProductCommand;
            }
        } 

        #endregion

        #region Add New News

        private DelegateCommand<CreateElement>? addNewNewsCommand;

        public ICommand AddNewNewsCommand
        {
            get
            {
                if (addNewNewsCommand == null)
                {
                    addNewNewsCommand = new DelegateCommand<CreateElement>((CreateElement view) => 
                    {
                        if (NewNewsValidate())
                        {

                            Db.CreateNews(news);
                            Db.Save();
                            SendToModalWindow("The news was successfully added to the database.");
                            view.Close();
                            if (WhetherToSendANewsletter == true)
                            {
                                // почта отправка епта
                            }

                        }
                    });
                }
                return addNewNewsCommand;
            }
        }


        #endregion

        #region Add New User

        private DelegateCommand<CreateElement>? addNewUserCommand;

        public ICommand AddNewUserCommand
        {
            get
            {
                if (addNewUserCommand == null)
                {
                    addNewUserCommand = new DelegateCommand<CreateElement>((CreateElement view) =>
                    {
                        if (NewUserValidate())
                        {
                            Db.CreateUser(user);
                            Db.Save();
                            SendToModalWindow("The user was successfully added to the database.");
                            view.Close();
                            if (WhetherToSendANewsletter == true)
                            {
                                SendANewsLetter();
                            }
                        }
                    });
                }
                return addNewUserCommand;
            }
        }


        #endregion

        #region Checked type

        private DelegateCommand<ProdType> checkedTypeCommand;
        public ICommand CheckedTypeCommand
        {
            get
            {
                if (checkedTypeCommand == null)
                {
                    checkedTypeCommand = new DelegateCommand<ProdType>((ProdType obj) =>
                    {
                        //switch (obj)
                        //{
                        //    case ProdType.Meal: product.ProductType = Db.GetProductType(obj.ToString()); break;
                        //    case ProdType.Coffee: product.ProductType = Db.GetProductType(obj.ToString()); break;
                        //    case ProdType.Drinks: product.ProductType = Db.GetProductType(obj.ToString()); break;
                        //}
                        product.ProductType = Db.GetProductType(obj.ToString());
                    });
                }
                return checkedTypeCommand;
            }
        } 

        #endregion

        #endregion

        #region Methods

        private void SendANewsLetter()
        {
            throw new NotImplementedException();
        }

        private bool NewProductValidate()
        {
            return validator.Verify(ValidationBased.TextTo, ProductName, nameof(ErrorProductName)) &
                validator.Verify(ValidationBased.OrdinaryDigits, ProductPrice.ToString(), nameof(ErrorProductPrice)) &
                validator.Verify(ValidationBased.OrdinaryDigits, ProductCalories.ToString(), nameof(ErrorProductCalories)) &
                validator.Verify(ValidationBased.OrdinaryDigits, ProductTotalFat.ToString(), nameof(ErrorProductTotalFat)) &
                validator.Verify(ValidationBased.OrdinaryDigits, ProductSaturatedFat.ToString(), nameof(ErrorProductSaturatedFat)) &
                validator.Verify(ValidationBased.OrdinaryDigits, ProductTransFat.ToString(), nameof(ErrorProductTransFat)) &
                validator.Verify(ValidationBased.OrdinaryDigits, ProductCholesterol.ToString(), nameof(ErrorProductCholesterol)) &
                validator.Verify(ValidationBased.OrdinaryDigits, ProductSodium.ToString(), nameof(ErrorProductSodium)) &
                validator.Verify(ValidationBased.OrdinaryDigits, ProductTotalCarbohydrates.ToString(), nameof(ErrorProductTotalCarbohydrates)) &
                validator.Verify(ValidationBased.OrdinaryDigits, ProductProtein.ToString(), nameof(ErrorProductProtein)) &
                validator.Verify(ValidationBased.OrdinaryDigits, ProductCaffeine.ToString(), nameof(ErrorProductCaffeine));
        }
        
        private bool NewNewsValidate()
        {
            return validator.Verify(ValidationBased.TextTo, Title, nameof(ErrorTitle)) &
                //validator.Verify(ValidationBased.Image, Img, nameof(ErrorImg)) &
                ImgValidate() &
                validator.Verify(ValidationBased.TextTo, Content, nameof(ErrorContent));
                //validator.Verify(ValidationBased.Date, Date.ToString(), nameof(ErrorDate));
        }

        private bool NewUserValidate()
        {
            return validator.Verify(ValidationBased.TextTo, UserName, nameof(ErrorUserName)) &
                validator.Verify(ValidationBased.TextTo, Password, nameof(ErrorPassword));
        }


        private bool ImgValidate()
        {
            if (string.IsNullOrEmpty(Img))
            {
                ErrorImg = "Empty field";
                return false;
            }
            ErrorImg = "";
            return true;
        }

        #endregion
    }
}