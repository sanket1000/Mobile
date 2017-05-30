using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Core.API.Utilities.Helpers;
using Core.API.TLSettings.Data;

namespace Core.API.TLSettings.Models
{
    public class JCCostControls
    {
       DataTable adoJCCntrlsRs;

       public bool bolJCSystemActive;
       public bool bolDontAllowOverride;
       public bool bolRetrieveAcctPrfxFrmJob;

       //string strPrefixType;
       public string strCategoryUsage;
       public string strGlRetrieveMethod;
       public string strDefaultCostAcct;
       public string strTrackExtras;


       public bool bolEquipTablesExist;
       public bool bolEq_RetrievePrefixFromEq;
       public string strEq_DefaultPrefix;
       public string strEq_CostDebitAcct;
       public bool bolEq_UseAccountTable;
       public bool bolEq_AllowOverride;
       public bool bolEq_UseJCCostAcct;
       public int intEQ_TableColumns;

       public bool UseCategories
       {
           get
           {
               bool returnValue = true;
               returnValue = (strCategoryUsage.ToUpper() == "ALWAYS USE");
               return returnValue;
           }
       }

       public bool JCSystemActive
       {
           get
           {
               return this.bolJCSystemActive;
           }
       }

       public bool TrackExtras
       {
           get
           {
               bool returnValue = true;
               returnValue = strTrackExtras.ToUpper() == "TRACKING";
               return returnValue;
           }
       }


       public JCCostControls(int ConnectionID)
       {
           Init(ConnectionID);
       }


       private void Init(int ConnectionID)
       {
           adoJCCntrlsRs = TimberlineDA.GetCM_MASTER__JC_COST_CONTROLS(ConnectionID);
           if (adoJCCntrlsRs != null && adoJCCntrlsRs.Rows.Count > 0)
           {

               bolJCSystemActive = true;
               strGlRetrieveMethod = adoJCCntrlsRs.Rows[0]["GL_Retrieval_Method"].ToString();
               strCategoryUsage = adoJCCntrlsRs.Rows[0]["Category_Usage"].ToString();
               strDefaultCostAcct = adoJCCntrlsRs.Rows[0]["Cost_Account"].ToString();
               strTrackExtras = adoJCCntrlsRs.Rows[0]["Track_Extras"].ToString();
               //strGlRetrieveMethod = adoJCCntrlsRs.Rows[0]["GL_Retrieval_Method"].ToString();
               bolDontAllowOverride = System.Convert.ToBoolean(adoJCCntrlsRs.Rows[0]["Automatic_GL"]);
               //bolDontAllowOverride = System.Convert.ToBoolean(adoJCCntrlsRs.Rows[0]["Automatic_GL"]);
               bolRetrieveAcctPrfxFrmJob = System.Convert.ToBoolean(adoJCCntrlsRs.Rows[0]["Use_Prefix"]);

           }

           DataTable adoEQCntrlsRs = new DataTable();
           adoEQCntrlsRs = TimberlineDA.GetEQM_MASTER__EQ_CONTROLS(ConnectionID);

           if (adoEQCntrlsRs != null && adoEQCntrlsRs.Rows.Count > 0)
           {
               bolEquipTablesExist = true;
               strEq_DefaultPrefix = adoEQCntrlsRs.Rows[0]["Default_Prefix"].ToString();
               strEq_CostDebitAcct = CommonFunctions.TestNullString(adoEQCntrlsRs.Rows[0]["Cost_Debit_Account"].ToString());
               bolEq_UseAccountTable = Convert.ToBoolean(CommonFunctions.TestNullNumber(adoEQCntrlsRs.Rows[0]["Cost_DR_Use_Acct_Table"]));
               bolEq_AllowOverride = Convert.ToBoolean(CommonFunctions.TestNullNumber(adoEQCntrlsRs.Rows[0]["Cost_DR_Allow_Override"]));
               bolEq_UseJCCostAcct = Convert.ToBoolean(CommonFunctions.TestNullNumber(adoEQCntrlsRs.Rows[0]["Cost_DR_Use_JC_Acct"]));
               intEQ_TableColumns = System.Convert.ToInt32(CommonFunctions.TestNullNumber(adoEQCntrlsRs.Rows[0]["Cost_DR_Table_Columns"].ToString()));

           }

           adoEQCntrlsRs = new DataTable();
           adoEQCntrlsRs = TimberlineDA.GetEQS_STANDARD__EQ_RETRIEVAL_CONTROLS(ConnectionID);
           if (adoEQCntrlsRs != null && adoEQCntrlsRs.Rows.Count > 0)
           {

               bolEq_RetrievePrefixFromEq = (bool)(adoEQCntrlsRs.Rows[0]["Retrieve_prefix_from_Equipment"]);

           }

           if (bolEq_RetrievePrefixFromEq)
           {
               var tsctl = new TLSettings.Models.ActualTSCTL(ConnectionID);
               string Pnct = "";
               if(tsctl != null)
                Pnct = tsctl.DeterminePunctuation();


               string eqPrf = strEq_DefaultPrefix + Pnct;
               if (!strEq_CostDebitAcct.Contains(eqPrf))
                   strEq_CostDebitAcct = eqPrf + strEq_CostDebitAcct;
           }

       }
    }
}
