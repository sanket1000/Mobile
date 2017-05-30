using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Core.API.TLSettings;
using Core.API.TLSettings.Models;
using Core.API.ConfigSvc.Model;

namespace Core.API.ConfigSvc.Model
{   
    [Serializable]
    //[XmlRoot("DistributionConfigurations")]
    public class DistConfigs
    {
        [XmlElement("DistributionConfiguration")]
        public List<DistConfig> DistConfigList { get; set; }

        [XmlIgnore]
        private APDistributionSettings apSettings = null;

        private ActualTSCTL actualTSCTL = null;
        internal DistConfigs()
        {
        }
        internal DistConfigs(int ConnectionID)
        {
            DistConfigList = new List<DistConfig>();
            Init(ConnectionID);
        }

        internal void Init(int ConnectionID)
        {
            TimberlineSettings ts = new TimberlineSettings();
            apSettings = ts.GetAPDistributionSettings(ConnectionID);
            actualTSCTL = new ActualTSCTL(ConnectionID);
            InitVTSubContractor();
            InitVTSupplier();
            InitVTOther();
            InitVTSummary();
            InitVTEquipSupplier();

        }

        internal void InitVTSubContractor()
        {
           // apSettings.VendorTypeLevel = actualTSCTL == null ? ConfigConstants.VT_SUBCONTRACTOR.ToString() : actualTSCTL.VendorType1;
            apSettings.VendorTypeLevel = "Vendor_Type_1";
            DistConfig dc = new DistConfig(actualTSCTL == null ? ConfigConstants.VT_SUBCONTRACTOR.ToString() : actualTSCTL.VendorType1, apSettings);
            DistConfigList.Add(dc);
        }

        internal void InitVTSupplier()
        {
            //apSettings.VendorTypeLevel = actualTSCTL == null ? ConfigConstants.VT_SUPPLIER.ToString() : actualTSCTL.VendorType2; 
            apSettings.VendorTypeLevel = "Vendor_Type_2";
            DistConfig dc = new DistConfig(actualTSCTL == null ? ConfigConstants.VT_SUPPLIER.ToString() : actualTSCTL.VendorType2 , apSettings);
            DistConfigList.Add(dc);
        }

        internal void InitVTOther()
        {
            //apSettings.VendorTypeLevel = actualTSCTL == null ? ConfigConstants.VT_OTHER.ToString() : actualTSCTL.VendorType3;
            apSettings.VendorTypeLevel = "Vendor_Type_3";
            DistConfig dc = new DistConfig(actualTSCTL == null ? ConfigConstants.VT_OTHER.ToString() : actualTSCTL.VendorType3, apSettings);
            DistConfigList.Add(dc);
        }
        internal void InitVTSummary()
        {
            //apSettings.VendorTypeLevel = actualTSCTL == null ? ConfigConstants.VT_SUMMARY.ToString() : actualTSCTL.VendorType5;
            apSettings.VendorTypeLevel = "Vendor_Type_4";
            DistConfig dc = new DistConfig(actualTSCTL == null ? ConfigConstants.VT_SUMMARY.ToString() : actualTSCTL.VendorType4, apSettings);
            DistConfigList.Add(dc);
        }
        internal void InitVTEquipSupplier()
        {
            apSettings.VendorTypeLevel = "Vendor_Type_5";
            //DistConfig dc = new DistConfig(ConfigConstants.VT_EQUIP_SUPPLIER_DESC.ToString(), apSettings);
            DistConfig dc = new DistConfig(actualTSCTL == null ? ConfigConstants.VT_EQUIP_SUPPLIER_DESC.ToString() : actualTSCTL.VendorType5, apSettings);
            DistConfigList.Add(dc);
        }

       

       
    }
}
