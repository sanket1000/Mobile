using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Core.API.TLSettings.Models;
 
namespace Core.API.TLSettings
{
    public class TimberlineSettings
    {
        public APDistributionSettings GetAPDistributionSettings(int ConnectionID)
        {
            return new APDistributionSettings(ConnectionID);
        }

        public APInvoiceSettings GetAPInvoiceSettings(int ConnectionID)
        {
            return new APInvoiceSettings(ConnectionID);
        }

        public string GetJobAuthorization(string Job, int ConnectionID)
        {
            string jobAuth = "";
            DataTable dt = Core.API.TLSettings.Data.TimberlineDA.GetJobAuthorization(Job, ConnectionID);

            if (dt != null)
            {
                jobAuth = dt.Rows[0]["Authorization"].ToString();
            }

            return jobAuth;
        }

        public DataTable GetTSCTLSettings(int ConnectionID)
        {
            return Core.API.TLSettings.Data.TimberlineDA.GetTSCTLSettings(ConnectionID);
        }
    }
}
