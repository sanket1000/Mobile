
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.API.DocumentSvc.Entity
{
    [DataContract]
    public class AnnotationDto
    {
        public AnnotationDto()
        {
            Size = new SizeDto();
            Location = new PointDto();
        }

       

        /// <summary>
        /// Gets or sets the annotation location measured from the top left of the image. Point is of the top left of the unrotated AABB
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        /// 

        [DataMember]
        public PointDto Location { get; set; }

        /// <summary>
        /// Gets or sets the unrotated width / height
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        [DataMember]
        public SizeDto Size { get; set; }

        [DataMember]
        public float Rotation { get; set; }

        [DataMember]
        public bool CanMirror { get; set; }

        [DataMember]
        public bool CanMove { get; set; }

        [DataMember]
        public bool CanResize { get; set; }

        [DataMember]
        public bool CanRotate { get; set; }

        [DataMember]
        public bool CanSelect { get; set; }

        [DataMember]
        public DateTime CreationTime { get; set; }

        [DataMember]
        public DateTime ModifiedTime { get; set; }

        [DataMember]
        public string ToolTip { get; set; }

        [DataMember]
        public bool Visible { get; set; }

        
    }
}