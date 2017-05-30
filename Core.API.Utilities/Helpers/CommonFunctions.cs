using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Data;


namespace Core.API.Utilities.Helpers
{
    public class CommonFunctions
    {
        public static string TestNullString(object vTest)
        {
            string returnValue = null;

            if (vTest == null)
            {
                returnValue = "";
            }
            else
            {
                returnValue = System.Convert.ToString(vTest);
            }
            return returnValue;
        }

        public static object TestNullNumber(object vTest)
        {
            object returnValue = null;

            if (vTest == null || vTest == DBNull.Value || string.IsNullOrWhiteSpace(vTest.ToString()))
            {
                returnValue = 0;
            }
            else
            {
                returnValue = vTest;
            }
         
            return returnValue;
        }

        //public static object QNApos(object vItem)
        //{
        //    object returnValue = "";
        //    if (vItem == null || vItem == System.DBNull.Value)
        //    {
        //        return "Null";
        //    }
        //    if (Strings.Trim(System.Convert.ToString(vItem)).Length == 0)
        //    {
        //        return "Null";
        //    }
        //    returnValue = "\'" + Strings.Replace(System.Convert.ToString(vItem), "\'", "\'\'", 1, -1, CompareMethod.Text) + "\'";
        //    return returnValue;
        //}

        public static bool IsNullOrEmpty(string str)
        {
            return string.IsNullOrEmpty(TestNullString(str).ToString().Trim());
        }

        public static string QCom(object vItem)
        {
            string returnValue = null;
            returnValue = vItem + ", ";
            return returnValue;
        }


        public static string QNAposCom(object vItem)
        {
            string returnValue = null;
            returnValue = QNApos(vItem) + ", ";
            return returnValue;
        }
        public static string QNAValSt(string vItem)
        {
            return Strings.Replace(System.Convert.ToString(vItem), "\'", "\'\'", 1, -1, CompareMethod.Text);
        }
        public static object QNApos(object vItem)
        {
            object returnValue = "";
            if (vItem == null || vItem == System.DBNull.Value)
            {
                return "Null";
            }
            if (Strings.Trim(System.Convert.ToString(vItem)).Length == 0)
            {
                return "Null";
            }
            returnValue = "\'" + Strings.Replace(System.Convert.ToString(vItem), "\'", "\'\'", 1, -1, CompareMethod.Text) + "\'";
            return returnValue;
        }

        public static string QNAposSt(string vItem)
        {
            string returnValue = "";
            if (vItem == "")
            {
                return "''";
            }
            returnValue = "\'" + Strings.Replace(System.Convert.ToString(vItem), "\'", "\'\'", 1, -1, CompareMethod.Text) + "\'";
            return returnValue;
        }

        public static string GetCurrentDateAndTimeOnSqlServer()
        {
            string dateFormat = string.Empty;
            DataTable dt = new DataTable();

            APIUtilDataAccessHelper.TimberScanData.FillDataTable(dt, "SELECT getdate()",
                null, true, false);

            DateTime CurDateTime = Convert.ToDateTime(dt.Rows[0][0]);

            dt.Dispose();

            return CurDateTime.ToString("yyyy-MM-dd HH:mm:ss:fff");
        }

        public static string QNValCom(object vItem)
        {
            string returnValue = null;
            returnValue = TestNullNumber(vItem) + ", ";
            return returnValue;
        }

        public static string SQLDate(object dt)
        {
            string returnValue = null;
            if (!Information.IsDate(dt))
            {
                return "Null";

            }
            else
                if (Information.IsDBNull(dt) || Strings.Len(dt) == 0)
                {
                    return "Null";

                }

            returnValue = "\'" + Convert.ToDateTime(dt).ToString("yyyy-MM-dd HH:mm:ss") + "\'";
            return returnValue;
        }

        public static bool StringHasAllZeroes(string stToTest)
        {
            bool stHasAllZeroes = false;
            if (stToTest.Length > 0)
            {
                bool stDoesNotHaveNonZeroChars = true;
                for (int i = 0; i < stToTest.Length; i++)
                {
                    if (stToTest.Substring(i, 1) != "0")
                    {
                        stDoesNotHaveNonZeroChars = false;
                    }
                }
                if (stDoesNotHaveNonZeroChars)
                    stHasAllZeroes = true;
            }
            return stHasAllZeroes;
        }
    }
}
