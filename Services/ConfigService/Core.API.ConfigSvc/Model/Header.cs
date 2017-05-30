using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.ConfigSvc.Model
{
    [Serializable]
    [XmlRoot("Header")]
    public class Header
    {
        public string Name { get; set; }

        public string IsRequired { get; set; }

        public string CustomLable { get; set; }

        public string Index { get; set; }

        public string Type { get; set; }

        public string Length { get; set; }

        public string Format { get; set; }

        public Header()
        {
        }
    }
}
