using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.Entities
{
 
    [Serializable]
    [XmlRoot("Groups")]
    public class UserGroups
    {
        public UserGroups()
        {
        }

        [XmlElement("Groups")]
        public List<UserGroup> LstUserGroups { get; set; }
    }

    [Serializable]
    public class UserGroup
    {
        public UserGroup()
        {
        }

        [XmlElement("ID")]
        public int UserGroupID { get; set; }

        [XmlElement("Name")]
        public string GroupDescription { get; set; }

        [XmlElement("Type")]
        public string ActionType { get; set; }
    }
}
