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
		#region Constructor

		public HomeViewModel()
		{
			News = Db.GetNewsList().ToList();
        }

		#endregion

		#region Fields

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
