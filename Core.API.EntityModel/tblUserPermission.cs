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
    public partial class tblUserPermission
    {
        public string UserID { get; set; }
        public int ConnectionID { get; set; }
        public string PrefixOrJob { get; set; }
        public string PrefixOrJobNo { get; set; }
        public string Type { get; set; }
        public byte[] RwVersion { get; set; }
    }
    
}
