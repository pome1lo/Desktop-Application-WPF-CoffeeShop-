﻿using Coffee_Shop.Database;
using Coffee_Shop.Models;
using System.Collections.Generic;
using System.Linq;

namespace Coffee_Shop.ViewModels
{
    internal class HomeViewModel : ViewModelBase
    {
        #region Constructor

        public HomeViewModel()
        {
            this.Db = new UnitOfWork();
            News = Db.News.GetIEnumerable().ToList();
        }

        #endregion

        #region Fields

        private UnitOfWork Db;
        private List<News>? news;

        #endregion

        #region Properties

        public List<News>? News
        {
            get
            {
                return news;
            }
            set
            {
                news = value;
                OnPropertyChanged(nameof(news));
            }
        }

        #endregion
    }
}
