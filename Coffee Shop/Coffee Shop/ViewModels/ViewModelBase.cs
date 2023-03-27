using Coffee_Shop.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Coffee_Shop.Database.ApplicationContext;

namespace Coffee_Shop.ViewModels
{
    class ViewModelBase : INotifyPropertyChanged
    {
        protected static IRepository? db { get; private set; }

        public static void SetDatabase(byte database)
        {
            SetConnectionString(database);
            db = GetTheCurrentDatabase();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}