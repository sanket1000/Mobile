using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
namespace Core.API.Services.Test
{
    public partial class CoreAPITestMain : Form
    {
        string retXML = "";

        public CoreAPITestMain()
        {
            InitializeComponent();

        }

        private long GetRwVersion()
        {
            long rwVersion = 0;
            long.TryParse(this.txtRwVersion.Text, out rwVersion);
            return rwVersion;
        }

        private void DisplayResult(string resultXML)
        {
            this.richTextBoxUserSvcXML.Text = "";
            try
            {
                if (!string.IsNullOrEmpty(resultXML))
                {
                    XDocument xdoc = XDocument.Parse(resultXML);
                    this.richTextBoxUserSvcXML.Text = xdoc.ToString();
                }
            }
            catch (Exception ex)
            {
                this.richTextBoxUserSvcXML.Text = ex.Message;
            }
        }

        #region usersvc

        private void btnUsers_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.UserSvc.UsersClient client = new UserSvc.UsersClient())
                {
                    retXML = client.users(this.GetRwVersion());
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
            
        }

        private void btnUsersAll_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.UserSvc.UsersClient client = new UserSvc.UsersClient())
                {
                    retXML = client.usersall();
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
            
        }

        private void btnGroups_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.UserSvc.UsersClient client = new UserSvc.UsersClient())
                {
                    retXML = client.groups(this.GetRwVersion());
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
           
        }

        private void btnAllGroups_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.UserSvc.UsersClient client = new UserSvc.UsersClient())
                {
                    retXML = client.groupsall();
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
            
        }

        private void btnUserPermission_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.UserSvc.UsersClient client = new UserSvc.UsersClient())
                {
                    retXML = client.permission(this.GetRwVersion());
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
            
        }

        private void btnAllUserPermission_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.UserSvc.UsersClient client = new UserSvc.UsersClient())
                {
                    retXML = client.permissionall();
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
           
        }

        private void btnGroupMembership_Click(object sender, EventArgs e)
        {
            retXML = "";
            int grpId = 0;
            int.TryParse(this.txtGroupId.Text, out grpId);
            try
            {
                using (Core.API.Services.Test.UserSvc.UsersClient client = new UserSvc.UsersClient())
                {
                    retXML = client.groupmembership(grpId);
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }

        }

        private void btnAllGroupMemberShip_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.UserSvc.UsersClient client = new UserSvc.UsersClient())
                {
                    retXML = client.groupmembershipall();
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
           
        }

        private void btnGroupMembershipRwVersion_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.UserSvc.UsersClient client = new UserSvc.UsersClient())
                {
                    retXML = client.groupmembershipbyrwversion(this.GetRwVersion());
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
        }

        #endregion 


        #region configsvc


        private void btnConfig_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.ConfigSvc.ConfigClient client = new ConfigSvc.ConfigClient())
                {
                    retXML = client.configuration();
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
        }

        private void btnHeaderConfig_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.ConfigSvc.ConfigClient client = new ConfigSvc.ConfigClient())
                {
                    retXML = client.headerconfiguration();
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
        }

        private void btnDistConfig_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.ConfigSvc.ConfigClient client = new ConfigSvc.ConfigClient())
                {
                    retXML = client.distributionconfiguration();
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
        }

        #endregion


        #region documentsvc

        
        private void btnGetDocListInApproval_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.DocSvc.DocumentClient client = new DocSvc.DocumentClient())
                {
                     
                    retXML = client.GetDocuments();
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
        }

        private void btnDocumentGet_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.DocSvc.DocumentClient client = new DocSvc.DocumentClient())
                {
                    int id = 0;
                    DocSvc.AnnotationDataDto ent = null;
                    int.TryParse(this.txtDocNotesDocID.Text, out id);
                    
                    retXML = client.GetDocument(id);
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
        }
        private void btnDocGetNotes_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.DocSvc.DocumentClient client = new DocSvc.DocumentClient())
                {
                    int id = 0;
                    int.TryParse(this.txtDocNotesDocID.Text, out id);
                    //Core.API.Services.Test.DocumentSvc.AnnotationDataDto dto = new DocumentSvc.AnnotationDataDto();
                    //dto = client.GetDocumentAnnotation(70);
                    retXML = client.GetDocumentNotes(id);
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
        }

        private void btnCommentSave_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.DocSvc.DocumentClient client = new DocSvc.DocumentClient())
                {
                    int docid = 0;
                    int actionlvl = 0;
                    int userid = 0;
                    DateTime dt = DateTime.Now;

                    int.TryParse(this.txtCommentDocID.Text, out docid);
                    int.TryParse(this.txtCommentActionLevel.Text, out actionlvl);
                    int.TryParse(this.txtCommentUserID.Text, out userid);

                    Core.API.Services.Test.DocSvc.RetMessage retMsg = client.SaveComment(7, this.txtComments.Text, dt, userid);
                    DisplayResult(retMsg.Message);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
        }

        private void btnRejectDocument_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.DocSvc.DocumentClient client = new DocSvc.DocumentClient())
                {
                    int docid = 0;
                    int actionlvl = 0;
                    int userid = 0;
                    int rejectLevel = 0;
                    DateTime dt = DateTime.MinValue;

                    int.TryParse(this.txtRejectDocID.Text, out docid);
                    int.TryParse(this.txtRejectActionLevel.Text, out actionlvl);
                    int.TryParse(this.txtRejectUserID.Text, out userid);
                    int.TryParse(this.txtRejectRejectLevel.Text, out rejectLevel);
                    DateTime.TryParse(this.dtRejectDate.Text, out dt);

                    retXML = client.RejectDocument(docid, actionlvl, rejectLevel, this.txtRejectReason.Text, dt, userid);
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
        }

        private void btnDeleteDocument_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.DocSvc.DocumentClient client = new DocSvc.DocumentClient())
                {
                    int docid = 0;
                    int actionlvl = 0;
                    int userid = 0;

                    int.TryParse(this.txtDeleteDocID.Text, out docid);
                    int.TryParse(this.txtDeleteActionLevel.Text, out actionlvl);
                    int.TryParse(this.txtDeleteUserID.Text, out userid);
                    

                    retXML = client.DeleteDocument(docid, actionlvl, this.txtDeleteReason.Text, userid);
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
        }

        private void btnHoldDoc_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.DocSvc.DocumentClient client = new DocSvc.DocumentClient())
                {
                    int docid = 0;
                    int actionlvl = 0;
                    int userid = 0;
                    int rejectLevel = 0;
                    DateTime dt = DateTime.MinValue;

                    int.TryParse(this.txtHoldDocumentID.Text, out docid);
                    int.TryParse(this.txtHoldActionLevel.Text, out actionlvl);
                    int.TryParse(this.txtHoldUserID.Text, out userid);
                    int.TryParse(this.txtRejectRejectLevel.Text, out rejectLevel);
                    DateTime.TryParse(this.dtHoldDate.Text, out dt);

                    retXML = client.HoldDocument(docid, actionlvl, this.txtHoldReason.Text, dt, userid);
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
        }

        private void btnAPHoldDoc_Click(object sender, EventArgs e)
        {
            retXML = "";
            try
            {
                using (Core.API.Services.Test.DocSvc.DocumentClient client = new DocSvc.DocumentClient())
                {
                    int docid = 0;
                    int actionlvl = 0;
                    int userid = 0;
                 
                    DateTime dt = DateTime.MinValue;

                    int.TryParse(this.txtAPHoldDocID.Text, out docid);
                    int.TryParse(this.txtAPHoldActionLevel.Text, out actionlvl);
                    int.TryParse(this.txtAPHoldUserID.Text, out userid);
                 
                    DateTime.TryParse(this.dtAPHoldDate.Text, out dt);

                    retXML = client.APHoldDocument(docid, actionlvl, this.txtAPHoldReason.Text, this.chkAPHold.Checked, dt, userid);
                    DisplayResult(retXML);
                }
            }
            catch (Exception ex)
            {
                DisplayResult(ex.Message);
            }
        }

        #endregion

       

    }
}
