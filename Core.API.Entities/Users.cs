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
    [XmlRoot("Users")]
    public class Users
    {
        public Users()
        {
        }

        [XmlElement("Users")]
        List<Users> UserList { get; set; }
    }

    [Serializable()]
    public class User
    {
        public User()
        {
        }

        [XmlElement("Id")]
        public int UserIDNo { get; set; }

        [XmlElement("UserName")]
        public string UserID { get; set; }

        [XmlElement("Email")]
        public string email { get; set; }

        [XmlElement("FirstName")]
        public string FirstName { get; set; }

        [XmlElement("LastName")]
        public string LastName { get; set; }

        [XmlElement("IsActive")]
        public bool AccountEnabled { get; set; }

        [XmlElement("IsMobileUser")]
        public bool IsMobileUser { get; set; }

        //[XmlElement("Version")]
        //public long RwVersion { get; set; }

    }

}


