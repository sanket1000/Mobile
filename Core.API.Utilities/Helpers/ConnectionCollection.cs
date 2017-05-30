using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Core.API.Utilities.Helpers
{
    internal class colConnections : System.Collections.IEnumerable
    {

        //local variable to hold collection
        ArrayList mCol = new ArrayList();
        Hashtable htConnectionIdToIndexMapping = new Hashtable();

        public clsConnection Add(string Path, string sTyp, string Description, int ConnectionID, bool Default_Renamed, string sPrfxA, string sPrfxAB, string sPrfxABC, short iJobSec1Size, short iJobSec2Size, short iJobSec3Size, string sJobPunct1, string sJobPunct2, short iCostCodeSec1Size, short iCostCodeSec2Size, short iCostCodeSec3Size, short iCostCodeSec4Size, string sCostCodePunct1, string sCostCodePunct2, string sCostCodePunct3,
            DateTime LastApproveImportedDate, DateTime LastApproveRecurringDate, DateTime LastApproveRMDate, string sKey)
        {

            if (string.IsNullOrEmpty(sKey))
                throw new Exception("Cannot at a Connection without a key.");

            clsConnection returnValue;
            //create a new object
            clsConnection objNewMember;
            objNewMember = new clsConnection();


            //set the properties passed into the method

            objNewMember.Path = Path;
            objNewMember.SystemType = sTyp;
            //.Description = Description
            objNewMember.Description = Description;
            objNewMember.ConnectionID = ConnectionID;
            objNewMember.Default_Renamed = Default_Renamed;
            //2/9/07 - Version 2.0.7
            objNewMember.PrefixADesc = sPrfxA;
            objNewMember.PrefixABDesc = sPrfxAB;
            objNewMember.PrefixABCDesc = sPrfxABC;
            //----------------------------------
            //2/22/07 - Version 2.0.9
            objNewMember.JobSec1Size = iJobSec1Size;
            objNewMember.JobSec2Size = iJobSec2Size;
            objNewMember.JobSec3Size = iJobSec3Size;
            objNewMember.JobPunct1 = sJobPunct1;
            objNewMember.JobPunct2 = sJobPunct2;
            objNewMember.CostCodeSec1Size = iCostCodeSec1Size;
            objNewMember.CostCodeSec2Size = iCostCodeSec2Size;
            objNewMember.CostCodeSec3Size = iCostCodeSec3Size;
            objNewMember.CostCodeSec4Size = iCostCodeSec4Size;
            objNewMember.CostCodePunct1 = sCostCodePunct1;
            objNewMember.CostCodePunct2 = sCostCodePunct2;
            objNewMember.CostCodePunct3 = sCostCodePunct3;
            //----------------------------------

            objNewMember.LastApproveImportedDate = LastApproveImportedDate;
            objNewMember.LastApproveRecurringDate = LastApproveRecurringDate;
            objNewMember.LastApproveRMDate = LastApproveRMDate;

            this.htConnectionIdToIndexMapping.Add(sKey, objNewMember);

            if (sKey.Length == 0)
            {
                mCol.Add(objNewMember);//, null, null, null);
            }
            else
            {
                mCol.Add(objNewMember);//, sKey, null, null);
            }


            //return the object created
            returnValue = objNewMember;
            objNewMember = null;

            return returnValue;
        }


        public clsConnection this[string Key]
        {
            get
            {
                clsConnection returnValue = null;
                //used when referencing an element in the collection
                //vntIndexKey contains either the Index or Key to the collection,
                //this is why it is declared as a Variant
                //Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
                //returnValue = (clsConnection) mCol[System.Convert.ToInt32(Key)-1];
                if (this.htConnectionIdToIndexMapping.Contains(Key))
                {
                    returnValue = (clsConnection)htConnectionIdToIndexMapping[Key];
                }
                return returnValue;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">Zero based index of the clsConnection to get</param>
        /// <returns></returns>
        public clsConnection this[int index]
        {
            get
            {

                return (clsConnection)mCol[index];
            }
        }

        public int Count
        {
            get
            {
                int returnValue = 0;
                //used when retrieving the number of elements in the
                //collection. Syntax: Debug.Print x.Count
                returnValue = mCol.Count;
                return returnValue;
            }
        }


        //Public ReadOnly Property NewEnum() As stdole.IUnknown
        //Get
        //this property allows you to enumerate
        //this collection with the For...Each syntax
        //NewEnum = mCol._NewEnum
        //End Get
        //End Property


        //public System.Collections.IEnumerator GetEnumerator
        //{
        //    get
        //    {
        //        System.Collections.IEnumerator returnValue;
        //        //GetEnumerator = this.mCol.GetEnumerator();
        //        returnValue = this.mCol.GetEnumerator();
        //        return returnValue;
        //    }
        //}


        public void Remove(int vntIndexKey)
        {
            //used when removing an element from the collection
            //vntIndexKey contains either the Index or Key, which is why
            //it is declared as a Variant
            //Syntax: x.Remove(xyz)

            mCol.Remove(vntIndexKey);
        }

        private void Class_Initialize_Renamed()
        {
            //creates the collection when this class is created
            //mCol = new Collection();
        }

        public colConnections()
        {
            Class_Initialize_Renamed();
        }

        private void Class_Terminate_Renamed()
        {
            //destroys collection when this class is terminated
            mCol = null;
        }

        ~colConnections()
        {
            Class_Terminate_Renamed();
            //        base.Finalize();
        }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.mCol.GetEnumerator();
        }

        #endregion
    }
}
