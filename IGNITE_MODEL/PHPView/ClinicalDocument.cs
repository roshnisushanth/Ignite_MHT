using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL.PHPView
{
    public class ClinicalDocument
    {
        public long Id { get; set; }
        public long PatientId { get; set; }
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public string Size { get; set; }
        public string UploadedDate { get; set; }
        public string UploadedFileName { get; set; }       
    }

    public class ClinicalDocumentList
     {

         public List<ClinicalDocument> ClinicalDocumentLists { get; set; }
     }
}
