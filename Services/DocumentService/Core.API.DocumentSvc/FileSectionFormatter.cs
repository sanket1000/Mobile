using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using Core.API.Utilities;
using Core.API.Utilities.Helpers;

namespace Core.API.DocumentSvc
{
    [Serializable]
    [XmlRoot("File")]
    public class FileSectionFormatter
    {
        public int Id { get; set; } // invoice id
        public string FileName { get; set; }
        public long FileSize { get; set; } // invoice id
        public string FileType { get; set; }
        public DateTime CreatedOnUTC { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }

        public FileSectionFormatter()
        {

        }

        public FileSectionFormatter(int DocumentID)
        {
            Id = DocumentID;
        }

        public string GetFileSection()
        {
            string fileSection = string.Empty;
            try
            {
                // get document image path
                string imagePath = new DocumentDA().GetDocumentImagePath(Id);
                if (!string.IsNullOrEmpty(imagePath))
                {
                    // check if file exists
                    if (System.IO.File.Exists(imagePath))
                    {
                        this.SetFileProperties(imagePath);

                        fileSection = SerializeToString(this);
                        return fileSection;
                    }
                }
            }
            catch (Exception ex)
            {
                string innerExMsg = (ex.InnerException == null) ? "" : ex.InnerException.Message;
                new DocumentDA().LogError("CoreAPIDocumentSvc", "GetFileSection for document = " + Id.ToString(), ex.Message, innerExMsg, "", "");
            }

            return fileSection;
        }

        private void SetFileProperties(string imagePath)
        {
            // filename
            FileInfo fi = new FileInfo(imagePath);
            if (fi != null)
            {
                this.FileName = fi.Name.Split('.')[0];
                this.FileSize = fi.Length;
                this.FileType = fi.Extension;
                if (!string.IsNullOrEmpty(this.FileType) && this.FileType.StartsWith("."))
                {
                    this.FileType = this.FileType.Remove(0, 1);
                }
                this.CreatedOnUTC = fi.CreationTimeUtc;
                this.IsArchived = false;
                this.IsActive = true;
            }

        }

        static string SerializeToString(object obj)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            using (StringWriter writer = new StringWriter())
            {
                var xmlWriter = XmlWriter.Create(writer, settings);
                serializer.Serialize(xmlWriter, obj, ns);
                
                return writer.ToString();
            }
        }
    }
}
