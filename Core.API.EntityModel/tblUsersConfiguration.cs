//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Core.API.EntityModel
{
    public partial class tblUsersConfiguration
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public Nullable<int> ConfigurationID { get; set; }
        public string ConfigurationValue { get; set; }
    
        public virtual tblConfiguration tblConfiguration { get; set; }
    }
    
}