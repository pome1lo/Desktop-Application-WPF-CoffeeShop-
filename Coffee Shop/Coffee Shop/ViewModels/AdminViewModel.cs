using CoffeeShop.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CoffeeShop.Data.Models;
using System.Text.RegularExpressions;
using System.IO;
using CoffeShop.Models;
using CoffeShop.Data;
using System.Collections.ObjectModel;
using Coffee_Shop.Models;
using System.Windows.Controls;
using Coffee_Shop.Database;

namespace Coffee_Shop.ViewModels
{
    internal class AdminViewModel : ViewModelBase
    {
        public ObservableCollection<Person>? Persons { get; set; } = new();
        public ObservableCollection<Product>? Products { get; set; } = new();
        private ControlsForAdmin controls { get; set; }
        
        #region Constructor

        public AdminViewModel(ControlsForAdmin controlsForAdmin)
        {
            this.controls = controlsForAdmin;
            this.language = controlsForAdmin.CultureName;
            this.theme = controlsForAdmin.Theme;
            this.current = controlsForAdmin.PanelAddNewProduct;

            GetDataFromDatabase();
        }

        #endregion

        #region Property

        #region Change language

        private string? language;
        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                language = new String(value.Skip(38).ToArray());

                ChangeLanguage();
                SaveCultureChanges();

                OnPropertyChanged(nameof(Language));
            }
        }

        #endregion

        #region Change theme

        private string? theme;
        public string Theme
        {
            get
            {
                return theme;
            }
            set
            {
                theme = new String(value.Skip(38).ToArray());
                ChangeTheme();
                SaveThemeChanges();

                OnPropertyChanged(nameof(Theme));
            }
        }

        #endregion

        #region Selected Item For Persons Database

        private Person? selectedItemForPersonsDB;
        public Person SelectedItemForPersonsDB
        {
            get
            {
                return selectedItemForPersonsDB;
            }
            set
            {
                selectedItemForPersonsDB = value;
                OnPropertyChanged(nameof(SelectedItemForPersonsDB));
            }
        }

        #endregion

        #region Selected Item For Products Database

        private Product? selectedItemForProductsDB;
        public Product SelectedItemForProductsDB
        {
            get
            {
                return selectedItemForProductsDB;
            }
            set
            {
                selectedItemForProductsDB = value;
                OnPropertyChanged(nameof(SelectedItemForProductsDB));
            }
        }

        #endregion

        #region Create new product

        private Product product = new Product();

        public string ProductName
        {
            get
            {
                return product.Name;
            }
            set
            {
                product.Name = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }
        public decimal ProductPrice
        {
            get
            {
                return product.Price;
            }
            set
            {
                product.Price = value;
                OnPropertyChanged(nameof(ProductPrice));
            }
        }
        public string ProductImg
        {
            get
            {
                return product.Img;
            }
            set
            {
                product.Img = value;
                OnPropertyChanged(nameof(ProductImg));
            }
        }
        public ushort ProductCalories
        {
            get
            {
                return product.Calories;
            }
            set
            {
                product.Calories = value;
                OnPropertyChanged(nameof(ProductCalories));
            }
        }
        public ushort ProductTotalFat
        {
            get
            {
                return product.Description.TotalFat;
            }
            set
            {
                product.Description.TotalFat = value;
                OnPropertyChanged(nameof(ProductTotalFat));
            }
        }
        public ushort ProductSaturatedFat
        {
            get
            {
                return product.Description.SaturatedFat;
            }
            set
            {
                product.Description.SaturatedFat = value;
                OnPropertyChanged(nameof(ProductSaturatedFat));
            }
        }
        public ushort ProductTransFat
        {
            get
            {
                return product.Description.TransFat;
            }
            set
            {
                product.Description.TransFat = value;
                OnPropertyChanged(nameof(ProductTransFat));
            }
        }
        public ushort ProductCholesterol
        {
            get
            {
                return product.Description.Cholesterol;
            }
            set
            {
                product.Description.Cholesterol = value;
                OnPropertyChanged(nameof(ProductCholesterol));
            }
        }
        public ushort ProductSodium
        {
            get
            {
                return product.Description.Sodium;
            }
            set
            {
                product.Description.Sodium = value;
                OnPropertyChanged(nameof(ProductSodium));
            }
        }
        public ushort ProductTotalCarbohydrates
        {
            get
            {
                return product.Description.TotalCarbohydrates;
            }
            set
            {
                product.Description.TotalCarbohydrates = value;
                OnPropertyChanged(nameof(ProductTotalCarbohydrates));
            }
        }
        public ushort ProductProtein
        {
            get
            {
                return product.Description.Protein;
            }
            set
            {
                product.Description.Protein = value;
                OnPropertyChanged(nameof(ProductProtein));
            }
        }
        public ushort ProductCaffeine
        {
            get
            {
                return product.Description.Caffeine;
            }
            set
            {
                product.Description.Caffeine = value;
                OnPropertyChanged(nameof(ProductCaffeine));
            }
        }


        #endregion

        #region Create new news

        private News news = new News();

        public string NewsTitle
        {
            get
            {
                return news.Title;
            }
            set
            {
                news.Title = value;
                OnPropertyChanged(nameof(News));
            }
        }

        public string NewsImg
        {
            get
            {
                return news.Img;
            }
            set
            {
                news.Img = value;
                OnPropertyChanged(nameof(News));
            }
        }

        public string NewsContent
        {
            get
            {
                return news.Content;
            }
            set
            {
                news.Content = value;
                OnPropertyChanged(nameof(News));
            }
        }

        public string NewsDate
        {
            get
            {
                return news.Date.ToString();
            }
            set
            {
                news.Date = DateTime.Parse(value);
            }
        }

        #endregion

        #endregion

        #region Methods

        private void GetDataFromDatabase()
        {
            //using (ApplicationContext db = ApplicationContext.GetContext())
            //{
            //    db.Products.ToList().ForEach(x => Products?.Add(x));
            //    db.Persons.ToList().ForEach(x => Persons?.Add(x));

            //    Products?.ToList().ForEach(x => x.Description = db.Descriptions.ToList()[x.Id - 1]);
            //}
            db.GetProductList().ToList().ForEach(x => Products?.Add(x));
            db.GetPersonList().ToList().ForEach(x => Persons?.Add(x));

            Products?.ToList().ForEach(x => x.Description = db.GetDescriptionList().ToList()[x.Id - 1]);
        }

        private void ChangeLanguage()
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

        private void ChangeTheme()
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

        private void SaveCultureChanges()
        {
            File.WriteAllText(@"../../../StaticFiles/CultureSettings.txt", Language);
        }
        private void SaveThemeChanges()
        {
            File.WriteAllText(@"../../../StaticFiles/ThemeSettings.txt", Theme);
        }

        #endregion

        #region Commands

        #region Delete Person

        private DelegateCommand? deletePersonCommand;

        public ICommand DeletePersonCommand
        {
            get
            {
                if (deletePersonCommand == null)
                {
                    deletePersonCommand = new DelegateCommand(DeletePerson);
                }
                return deletePersonCommand;
            }
        }

        private void DeletePerson()
        {
            if(SelectedItemForPersonsDB == null)
            {
                MessageBox.Show("First select the item to delete.");
            }
            else
            {
                //using (ApplicationContext db = ApplicationContext.GetContext())
                //{
                //    db.Persons.Remove(selectedItemForPersonsDB);
                //    db.SaveChanges();
                //}
                db.DeletePerson(selectedItemForPersonsDB.Id);
                MessageBox.Show("The item was successfully deleted.");
            }
        }
        #endregion

        #region Delete Product

        private DelegateCommand? deleteProductCommand;

        public ICommand DeleteProductCommand
        {
            get
            {
                if (deleteProductCommand == null)
                {
                    deleteProductCommand = new DelegateCommand(DeleteProduct);
                }
                return deleteProductCommand;
            }
        }

        private void DeleteProduct()
        {
            if (SelectedItemForProductsDB == null)
            {
                MessageBox.Show("First select the item to delete.");
            }
            else
            {
                //using (ApplicationContext db = ApplicationContext.GetContext())
                //{
                //    db.Products.Remove(SelectedItemForProductsDB);
                //    db.SaveChanges();
                //}
                db.DeleteProduct(SelectedItemForProductsDB.Id);
                MessageBox.Show("The item was successfully deleted.");
            }
        }

        #endregion

        #region Add New Product

        private DelegateCommand? addNewProductCommand;

        public ICommand AddNewProductCommand
        {
            get
            {
                if (addNewProductCommand == null)
                {
                    addNewProductCommand = new DelegateCommand(AddNewProduct);
                }
                return addNewProductCommand;
            }
        }

        private void AddNewProduct()
        {
            //using (ApplicationContext db = ApplicationContext.GetContext())
            //{
            //    product.Id = db.Products.Count() + 1;
            //    db.Products.AddRange(product);
            //    db.SaveChanges();
            //}
            product.Id = db.GetProductList().Count() + 1;
            db.CreateProduct(product);
            db.Save();
            MessageBox.Show("The product was successfully added to the database.");
        }

        #endregion

        #region Add New News

        private DelegateCommand? addNewNewsCommand;

        public ICommand AddNewNewsCommand
        {
            get
            {
                if (addNewNewsCommand == null)
                {
                    addNewNewsCommand = new DelegateCommand(AddNewNews);
                }
                return addNewNewsCommand;
            }
        }

        private void AddNewNews()
        {
            news.Id = db.GetNewsList().Count() + 1;
            db.CreateNews(news);
            db.Save();
            MessageBox.Show("The news was successfully added to the database.");
        }

        #endregion

        private DockPanel current;

        #region Show User DataBase

        private DelegateCommand? showUserDataBaseCommand;

        public ICommand ShowUserDataBaseCommand
        {
            get
            {
                if (showUserDataBaseCommand == null)
                {
                    showUserDataBaseCommand = new DelegateCommand(ShowUserDataBase);
                }
                return showUserDataBaseCommand;
            }
        }

        private void ShowUserDataBase()
        {
            ChangeCurrentDockPanelFor(controls.PanelPersonDataBase);
        }

        private void ChangeCurrentDockPanelFor(DockPanel dockPanel)
        {
            current.Visibility = Visibility.Collapsed;
            dockPanel.Visibility = Visibility.Visible;
            current = dockPanel;
        }

        #endregion

        #region Show Product DataBase

        private DelegateCommand? showProductDataBaseCommand;

        public ICommand ShowProductDataBaseCommand
        {
            get
            {
                if (showProductDataBaseCommand == null)
                {
                    showProductDataBaseCommand = new DelegateCommand(ShowProductDataBase);
                }
                return showProductDataBaseCommand;
            }
        }

        private void ShowProductDataBase()
        {
            ChangeCurrentDockPanelFor(controls.PanelProductDataBase);
        }

        #endregion

        #region Show Add New Product DataBase

        private DelegateCommand? showAddNewProductCommand;

        public ICommand ShowAddNewProductCommand
        {
            get
            {
                if (showAddNewProductCommand == null)
                {
                    showAddNewProductCommand = new DelegateCommand(ShowAddNewProduct);
                }
                return showAddNewProductCommand;
            }
        }

        private void ShowAddNewProduct()
        {
            ChangeCurrentDockPanelFor(controls.PanelAddNewProduct);
        }

        #endregion

        #region Show Add New Product DataBase

        private DelegateCommand? showOtherParamerersCommand;

        public ICommand ShowOtherParamerersCommand
        {
            get
            {
                if (showOtherParamerersCommand == null)
                {
                    showOtherParamerersCommand = new DelegateCommand(ShowOtherParamerers);
                }
                return showOtherParamerersCommand;
            }
        }

        private void ShowOtherParamerers()
        {
            ChangeCurrentDockPanelFor(controls.PanelOtherParameters);
        }

        #endregion

        #endregion
    }
}
