using Coffee_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coffee_Shop.ViewModels
{
    internal class HomeViewModel : ViewModelBase
    {
		private List<News>? news;

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

		public HomeViewModel()
		{
			News = db.GetNewsList().ToList();
        }
	}
}
