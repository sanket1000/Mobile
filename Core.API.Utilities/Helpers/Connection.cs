using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.API.Utilities.Helpers
{
    internal class clsConnection
    {

        int lngConnectionID;
        string strDescription;
        string strSystemType;
        string strPath;
        bool bolDefault;

        //2/9/07 - Version 2.0.7
        string strPrefixADesc;
        string strPrefixABDesc;
        string strPrefixABCDesc;
        //------------------------------

        //2/22/07 - Version 2.0.9
        int intJobSec1Size;
        string strJobPunct1;
        int intJobSec2Size;
        string strJobPunct2;
        int intJobSec3Size;
        int intCostCodeSec1Size;
        string strCostCodePunct1;
        int intCostCodeSec2Size;
        string strCostCodePunct2;
        int intCostCodeSec3Size;
        string strCostCodePunct3;
        int intCostCodeSec4Size;

        private void Class_Initialize_Renamed()
        {
            lngConnectionID = 0;
            strDescription = "";
            strSystemType = "";
            strPath = "";
            bolDefault = false;
            strPrefixADesc = "";
            strPrefixABDesc = "";
            strPrefixABCDesc = "";
            intJobSec1Size = 0;
            intJobSec2Size = 0;
            intJobSec3Size = 0;
            strJobPunct1 = "";
            strJobPunct2 = "";
            intCostCodeSec1Size = 0;
            strCostCodePunct1 = "";
            intCostCodeSec2Size = 0;
            strCostCodePunct2 = "";
            intCostCodeSec3Size = 0;
            strCostCodePunct3 = "";
            intCostCodeSec4Size = 0;
        }

        public clsConnection()
        {
            Class_Initialize_Renamed();
        }

        public int ConnectionID
        {
            get
            {
                int returnValue = 0;
                returnValue = lngConnectionID;
                return returnValue;
            }
            set
            {
                lngConnectionID = value;
            }
        }

        public string Description
        {
            get
            {
                string returnValue = null;
                returnValue = strDescription;
                return returnValue;
            }
            set
            {
                strDescription = value;
            }
        }

        public string SystemType
        {
            get
            {
                string returnValue = null;
                returnValue = strSystemType;
                return returnValue;
            }
            set
            {
                strSystemType = value;
            }
        }

        public string Path
        {
            get
            {
                string returnValue = null;
                returnValue = strPath;
                return returnValue;
            }
            set
            {
                strPath = value;
            }
        }

        public bool Default_Renamed
        {
            get
            {
                bool returnValue = true;
                returnValue = bolDefault;
                return returnValue;
            }
            set
            {
                bolDefault = value;
            }
        }

        private DateTime? _LastApproveImportedDate;
        public DateTime? LastApproveImportedDate
        {
            get
            {
                return this._LastApproveImportedDate;
            }
            set
            {
                this._LastApproveImportedDate = value;
            }
        }

        private DateTime _LastApproveRecurringDate;
        public DateTime LastApproveRecurringDate
        {
            get
            {
                return this._LastApproveRecurringDate;
            }
            set
            {
                this._LastApproveRecurringDate = value;
            }
        }

        private DateTime _LastApproveRMDate;
        public DateTime LastApproveRMDate
        {
            get
            {
                return this._LastApproveRMDate;
            }
            set
            {
                this._LastApproveRMDate = value;
            }
        }

        public string PrefixADesc
        {
            get
            {
                string returnValue = null;
                returnValue = strPrefixADesc;
                return returnValue;
            }
            set
            {
                strPrefixADesc = value;
            }
        }

        public string PrefixABDesc
        {
            get
            {
                string returnValue = null;
                returnValue = strPrefixABDesc;
                return returnValue;
            }
            set
            {
                strPrefixABDesc = value;
            }
        }

        public string PrefixABCDesc
        {
            get
            {
                string returnValue = null;
                returnValue = strPrefixABCDesc;
                return returnValue;
            }
            set
            {
                strPrefixABCDesc = value;
            }
        }

        public int JobSec1Size
        {
            get
            {
                int returnValue = 0;
                returnValue = intJobSec1Size;
                return returnValue;
            }
            set
            {
                intJobSec1Size = value;
            }
        }

        public int JobSec2Size
        {
            get
            {
                int returnValue = 0;
                returnValue = intJobSec2Size;
                return returnValue;
            }
            set
            {
                intJobSec2Size = value;
            }
        }

        public int JobSec3Size
        {
            get
            {
                int returnValue = 0;
                returnValue = intJobSec3Size;
                return returnValue;
            }
            set
            {
                intJobSec3Size = value;
            }
        }

        public string JobPunct1
        {
            get
            {
                string returnValue = null;
                returnValue = strJobPunct1;
                return returnValue;
            }
            set
            {
                strJobPunct1 = value;
            }
        }

        public string JobPunct2
        {
            get
            {
                string returnValue = null;
                returnValue = strJobPunct2;
                return returnValue;
            }
            set
            {
                strJobPunct2 = value;
            }
        }

        public int CostCodeSec1Size
        {
            get
            {
                int returnValue = 0;
                returnValue = intCostCodeSec1Size;
                return returnValue;
            }
            set
            {
                intCostCodeSec1Size = value;
            }
        }

        public int CostCodeSec2Size
        {
            get
            {
                int returnValue = 0;
                returnValue = intCostCodeSec2Size;
                return returnValue;
            }
            set
            {
                intCostCodeSec2Size = value;
            }
        }

        public int CostCodeSec3Size
        {
            get
            {
                int returnValue = 0;
                returnValue = intCostCodeSec3Size;
                return returnValue;
            }
            set
            {
                intCostCodeSec3Size = value;
            }
        }

        public int CostCodeSec4Size
        {
            get
            {
                int returnValue = 0;
                returnValue = intCostCodeSec4Size;
                return returnValue;
            }
            set
            {
                intCostCodeSec4Size = value;
            }
        }

        public string CostCodePunct1
        {
            get
            {
                string returnValue = null;
                returnValue = strCostCodePunct1;
                return returnValue;
            }
            set
            {
                strCostCodePunct1 = value;
            }
        }

        public string CostCodePunct2
        {
            get
            {
                string returnValue = null;
                returnValue = strCostCodePunct2;
                return returnValue;
            }
            set
            {
                strCostCodePunct2 = value;
            }
        }

        public string CostCodePunct3
        {
            get
            {
                string returnValue = null;
                returnValue = strCostCodePunct3;
                return returnValue;
            }
            set
            {
                strCostCodePunct3 = value;
            }
        }

        private string TestNullStrg(object vTest)
        {
            string returnValue = null;
            if (vTest == null)
            {
                returnValue = "";
            }
            else
                if (vTest == null)
                {
                    returnValue = "";
                }
                else
                {
                    returnValue = vTest.ToString();
                }
            return returnValue;
        }

        private object TestNullNum(object vTest)
        {
            object returnValue = null;
            if (vTest == null)
            {
                returnValue = 0;
            }
            else
                if (vTest == null)
                {
                    returnValue = 0;
                }
                else
                {
                    returnValue = vTest;
                }
            return returnValue;
        }

        private object TestNullDat(object vTest)
        {
            object returnValue = null;
            if (vTest == null)
            {
                returnValue = DateTime.Parse("1/1/100");
            }
            else
                if (vTest == null)
                {
                    returnValue = DateTime.Parse("1/1/100");
                }
                else
                {
                    returnValue = vTest;
                }
            return returnValue;
        }
    }
}
