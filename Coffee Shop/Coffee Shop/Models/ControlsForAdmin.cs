using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Coffee_Shop.Models
{
    internal struct ControlsForAdmin
    {
        public string CultureName = string.Empty;
        public DockPanel PanelAddNewProduct { get; set;}
        public DockPanel PanelOtherParameters { get; set;}
        public DockPanel PanelPersonDataBase { get; set;}
        public DockPanel PanelProductDataBase { get; set; }
        public ControlsForAdmin(string CultureName, DockPanel product, DockPanel parameters, DockPanel personDB, DockPanel productDB)
        {
            this.CultureName = CultureName;
            this.PanelAddNewProduct = product;
            this.PanelOtherParameters = parameters;
            this.PanelPersonDataBase = personDB;
            this.PanelProductDataBase = productDB;
        }
    }
}
