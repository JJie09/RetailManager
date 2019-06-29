using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library
{
    public class ConfigHelper 
    {
        public static decimal GetTaxRate()
        {
            string rateText = ConfigurationManager.AppSettings["taxRate"];
            if (!Decimal.TryParse(rateText, out decimal taxRate))
            {
                throw new ConfigurationErrorsException("Invalid taxRate in App.config");
            }
            return taxRate;
        }
    }
}
