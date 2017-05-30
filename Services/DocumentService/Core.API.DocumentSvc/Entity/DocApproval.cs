using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.DocumentSvc.Entity
{
    [Serializable]
    [XmlRoot("Approval")]
    public class DocApproval : Base
    {
        private string levelNameField;

        private int approvalLevelField;

        private int groupIdField;

        private int userIdField;

        private string reasonField;

        /// <remarks/>
        public string LevelName
        {
            get
            {
                return this.levelNameField;
            }
            set
            {
                this.levelNameField = value;
            }
        }

        /// <remarks/>
        public int ApprovalLevel
        {
            get
            {
                return this.approvalLevelField;
            }
            set
            {
                this.approvalLevelField = value;
            }
        }

        /// <remarks/>
        public int GroupId
        {
            get
            {
                return this.groupIdField;
            }
            set
            {
                this.groupIdField = value;
            }
        }

        /// <remarks/>
        public int UserId
        {
            get
            {
                return this.userIdField;
            }
            set
            {
                this.userIdField = value;
            }
        }

        /// <remarks/>
        public string Reason
        {
            get
            {
                return this.reasonField;
            }
            set
            {
                this.reasonField = value;
            }
        }
    }
}
