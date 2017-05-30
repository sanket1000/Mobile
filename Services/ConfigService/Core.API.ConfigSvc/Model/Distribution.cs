using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.ConfigSvc.Model
{
    [Serializable]
   // [XmlRoot("Distribution")]
    public class Distribution
    {
        private string p;
        private TLSettings.Models.APDistributionSettings apSettings;
        private string p_2;
        private string p_3;
        private string p_4;
        private string p_5;
        private string p_6;

        public string Name { get; set; }

        public string IsRequired { get; set; }

        public string CustomLable { get; set; }

        public string Index { get; set; }

        public string Type { get; set; }

        public string Length { get; set; }

        public string Format { get; set; }

        public Distribution()
        {

        }

        public Distribution(string name, string customLabel, string type, string length, string format, string isrequired, string index)
        {
            Name = name;
            IsRequired = isrequired;
            CustomLable = customLabel;
            Index = index;
            Type = type;
            Length = length;
            Format = format;

        }

      
    }
}
