using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.Entities
{
    [Serializable()]
    [XmlRoot("Permissions")]
    public class UserPermissions
    {
        public UserPermissions()
        {
        }

        public List<UserPermission> UserPermissionList { get; set; }
    }

    [Serializable()]
    [XmlRoot("Permission")]
    public class UserPermission
    {
         public UserPermission()
         {
         }

         [XmlElement("UserId")]
         public string UserID { get; set; }

         [XmlElement("GLPrefix")]
         public string GLPrefix { get; set; }

         [XmlElement("JobId")]
         public string JobID { get; set; }

        [XmlIgnore]
         public int ConnectionID { get; set; }

        [XmlIgnore]
         public string PrefixOrJob { get; set; }

        [XmlIgnore]
         public string PrefixOrJobNo { get; set; }

        [XmlIgnore]
         public string Type { get; set; }
    }
}
