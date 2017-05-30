using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.API.TLSettings.Models;

namespace Core.API.TLSettings.BusinessObject
{
    public class TimberlineBO
    {
        internal static APDistributionSettings GetAPDistributionSettings()
        {
            return new APDistributionSettings();
        }

        internal static APInvoiceSettings GetAPInvoiceSettings()
        {
            return new APInvoiceSettings();
        }
    }
}
