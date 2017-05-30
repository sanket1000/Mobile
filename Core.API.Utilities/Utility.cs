using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.API.Utilities.BusinessObject;
using Core.API.Utilities.Model;

namespace Core.API.Utilities
{
    public class Utility
    {

        public static SystemSettings GetSystemSettings()
        {
            return SystemSettingsBO.GetSystemSettings();
        }
    }
}
