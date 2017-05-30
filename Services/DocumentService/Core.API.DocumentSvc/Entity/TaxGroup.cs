using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.API.DocumentSvc.Entity
{
    internal class clsTaxGroup
    {
        string mvarTax;
        string mvarTaxGroup;
        string mvarDescription;
        double mvarTax_Percent;
        string mvarState;
        double mvarTaxAmount;
        double mvarTaxLiabilty;
        double mvarTaxCalcAmount;
        double mvarTaxCalcLiabilty;
        double mvarSQLTaxAmount;
        double mvarSQLTaxLiabilty;

        int mvarInvoiceID;
        int mvarDistSeq;
        int mvarTaxSeq;
        int mvarGridRow;

        bool mvarDeleted;


        private void Init()
        {
            mvarTax = "";
            mvarTaxGroup = "";
            mvarDescription = "";
            mvarTax_Percent = 0;
            mvarState = "";
            mvarTaxAmount = 0;
            mvarTaxLiabilty = 0;
            mvarTaxCalcAmount = 0;
            mvarTaxCalcLiabilty = 0;
            mvarSQLTaxAmount = 0;
            mvarSQLTaxLiabilty = 0;
            mvarInvoiceID = 0;
            mvarDistSeq = 0;
            mvarTaxSeq = 0;
            mvarGridRow = 0;
            mvarDeleted = false;
        }
        public clsTaxGroup()
        {
            Init();
        }


        public int GridRow
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarGridRow;
                return returnValue;
            }
            set
            {
                mvarGridRow = value;
            }
        }
        
        public bool Deleted
        {
            get
            {
                bool returnValue = false;
                returnValue = mvarDeleted;
                return returnValue;
            }
            set
            {
                mvarDeleted = value;
            }
        }

        public int InvoiceID
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarInvoiceID;
                return returnValue;
            }
            set
            {
                mvarInvoiceID = value;
            }
        }


        public int DistSeq
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarDistSeq;
                return returnValue;
            }
            set
            {
                mvarDistSeq = value;
            }
        }


        public int TaxSeq
        {
            get
            {
                int returnValue = 0;
                returnValue = mvarTaxSeq;
                return returnValue;
            }
            set
            {
                mvarTaxSeq = value;
            }
        }


        public string Tax
        {
            get
            {
                string returnValue = null;
                returnValue = mvarTax;
                return returnValue;
            }
            set
            {
                mvarTax = value;
            }
        }


        public string TaxGroup
        {
            get
            {
                string returnValue = null;
                returnValue = mvarTaxGroup;
                return returnValue;
            }
            set
            {
                mvarTaxGroup = value;
            }
        }


        public string Description
        {
            get
            {
                string returnValue = null;
                returnValue = mvarDescription;
                return returnValue;
            }
            set
            {
                mvarDescription = value;
            }
        }


        public double Tax_Percent
        {
            get
            {
                double returnValue;
                returnValue = mvarTax_Percent;
                return returnValue;
            }
            set
            {
                mvarTax_Percent = value;
            }
        }


        public string State
        {
            get
            {
                string returnValue = null;
                returnValue = mvarState;
                return returnValue;
            }
            set
            {
                mvarState = value;
            }
        }


        public double TaxAmount
        {
            get
            {
                double returnValue;
                returnValue = mvarTaxAmount;
                return returnValue;
            }
            set
            {
                mvarTaxAmount = value;
            }
        }


        public double TaxLiabilty
        {
            get
            {
                double returnValue;
                returnValue = mvarTaxLiabilty;
                return returnValue;
            }
            set
            {
                mvarTaxLiabilty = value;
            }
        }


        public double TaxCalcAmount
        {
            get
            {
                double returnValue;
                returnValue = mvarTaxCalcAmount;
                return returnValue;
            }
            set
            {
                mvarTaxCalcAmount = value;
            }
        }


        public double TaxCalcLiabilty
        {
            get
            {
                double returnValue;
                returnValue = mvarTaxCalcLiabilty;
                return returnValue;
            }
            set
            {
                mvarTaxCalcLiabilty = value;
            }
        }


        public double SQLTaxAmount
        {
            get
            {
                double returnValue;
                returnValue = mvarSQLTaxAmount;
                return returnValue;
            }
            set
            {
                mvarSQLTaxAmount = value;
            }
        }


        public double SQLTaxLiabilty
        {
            get
            {
                double returnValue;
                returnValue = mvarSQLTaxLiabilty;
                return returnValue;
            }
            set
            {
                mvarSQLTaxLiabilty = value;
            }
        }
    }
}

