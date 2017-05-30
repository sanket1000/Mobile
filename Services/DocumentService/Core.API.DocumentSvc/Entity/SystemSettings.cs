using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Core.API.DocumentSvc.Entity
{
    public class SystemSettings
    {
        public bool AllocateTaxes = false;
        public bool RestrictGLJobAccess = false;
        public bool AllowNoJobOrGLEntry = false;
        public bool IgnoreThresholdOnReRoute = false;
        public bool AllowOverrideUnitCost = false;
        public SystemSettings()
        {
            Init();
        }

        private void Init()
        {
            DataTable dt = new DocumentDA().GetSystemSettings();
            if (dt != null && dt.Rows.Count > 0)
            {
                bool.TryParse(dt.Rows[0]["AllocateTaxes"].ToString().Trim(), out AllocateTaxes);
                bool.TryParse(dt.Rows[0]["RestrictGLJobAccess"].ToString().Trim(), out RestrictGLJobAccess);
                bool.TryParse(dt.Rows[0]["AllowNoJobOrGLEntry"].ToString().Trim(), out AllowNoJobOrGLEntry);
                bool.TryParse(dt.Rows[0]["AllowOverrideUnitCost"].ToString().Trim(), out AllowOverrideUnitCost);
            }
        }
    }
}
