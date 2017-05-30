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
    [XmlRoot("Members")]
    public class UserGroupMembers
    {
        public UserGroupMembers()
        {
        }

        [XmlElement("PrimaryMembers")]
        List<UserGroupMember> PrimaryMembers { get; set; }

        [XmlElement("AlternateMembers")]
        List<UserGroupMember> AlternateMembers { get; set; }
    }

    [Serializable]
    public class UserGroupMember
    {
        public UserGroupMember()
        {
        }

        [XmlElement("UserID")]
        public int UserID { get; set; }

        [XmlIgnore()]
        public string UserType { get; set; }
    }

    //[Serializable]
    //public class AlternateMember
    //{
    //    public AlternateMember()
    //    {
    //    }

    //    [XmlElement("UserID")]
    //    public int UserID { get; set; }
    //}
}
