using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Coffee_Shop.Database.ApplicationContext;

namespace Coffee_Shop.Models.Entities
{
    [Serializable]
    public class ConfigurationSettings
    {
        public string Theme { get; set; } = null!;
        public string Language { get; set; } = null!;
        public byte Database { get; set; } = default;
    }
}
