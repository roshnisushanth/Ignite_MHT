using Hick.PHP;
using IGNITE_MODEL.PHPView;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.CommandCenter.ASPX
{
    public partial class AddEditClinicalDocuments : System.Web.UI.Page
    {
        PHPService PHPService = new PHPService();

        protected void Page_Load(object sender, EventArgs e)
        {
                string action = Request.QueryString["Action"].ToString();
                long clinicalId = !String.IsNullOrEmpty(Request.QueryString["id"]) ? Convert.ToInt64(Request.QueryString["id"]) : 0;
                if (!Page.IsPostBack)
                {
                    if (action == "Edit" && clinicalId != 0)
                    {
                        hdnEvent.Value = "Update";
                        GetClinicalDocOnId(clinicalId);
                    }
                    else
                    {
                        hdnEvent.Value = "Insert";
                    }
                }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            if (hdnEvent.Value=="Insert")
            {
                if (fileupload.HasFile)
                {
                    UploadClinicalDocument("Insert");
                }
                else
                {
                    StatusLabel.Visible = true;
                    StatusLabelsucess.Visible = false;
                    StatusLabel.Text = "Please select valid file";
                    StatusLabel.ForeColor = System.Drawing.Color.Red;
                }
            }

            else
            {
                if (fileupload.HasFile)
                {
                    UploadClinicalDocument("Update");
                }
                else
                {
                    string fileName = txt_clinicaldoc.Text;
                    string result = PHPService.InsertClinicalDoc("Update", 
                                                                  Convert.ToInt64(hdnClinicalDocId.Value),
                                                                  Convert.ToInt64(Session["PatientID"].ToString()),
                                                                  fileName,
                                                                  hdnFileExt.Value,
                                                                  hdnFileSize.Value,
                                                                  hdnUploadedFileName.Value);
                }

            }
        }

        protected void GetClinicalDocOnId(long clinicalId)
        {
            ClinicalDocumentList clinicaldoc = new ClinicalDocumentList();
            clinicaldoc = PHPService.GetClinicalDoc("SelectOnId", clinicalId, Convert.ToInt32(Session["PatientID"].ToString()));
            txt_clinicaldoc.Text = clinicaldoc.ClinicalDocumentLists.Select(m => m.FileName).FirstOrDefault();
            hdnClinicalDocId.Value = clinicalId.ToString();
            hdnUploadedFileName.Value = clinicaldoc.ClinicalDocumentLists.Select(m => m.UploadedFileName).FirstOrDefault();
            hdnFileSize.Value = clinicaldoc.ClinicalDocumentLists.Select(m => m.Size).FirstOrDefault();
            hdnFileExt.Value = clinicaldoc.ClinicalDocumentLists.Select(m => m.FileExt).FirstOrDefault();
        }


        protected void UploadClinicalDocument(string action)
        {
            try
            {
                string fileExt = Path.GetExtension(fileupload.FileName).Trim('.');
                string clinicalDocumentFormatAllowed = ConfigurationManager.AppSettings["ClinicalDocumentsFormatAllowed"];
                if (string.IsNullOrWhiteSpace(clinicalDocumentFormatAllowed))
                {
                    clinicalDocumentFormatAllowed = "pdf,doc,docx,xls,xlsx";
                }

                bool containsApp = clinicalDocumentFormatAllowed.Split(',')
                   .Where(s => string.Compare(fileExt, s, true) == 0)
                   .Count() > 0;
                if (containsApp == true)
                {
                    string fileName = txt_clinicaldoc.Text;
                    string fileSize = fileupload.FileContent.Length.ToString();
                    //string fileOriginalName = "Patient_Clinical" + "_" + fileName + "_" + Session["PatientID"].ToString() + "_" + DateTime.Now.ToString("MMddyyyyHHmmss").ToString() + "." + fileExt;
                    string fileOriginalName = string.Format("Patient_Clinical_{0}_{1}_{2}.{3}", fileName.Replace(" ","").Trim(), Session["PatientID"].ToString(), DateTime.Now.ToString("MMddyyyyHHmmss").ToString(), fileExt);

                    string path = Server.MapPath("~/UserFiles/ClinicalForms/");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    fileupload.SaveAs(Path.Combine(path, fileOriginalName));

                    long documentId = hdnClinicalDocId.Value == "" ? 0 : Convert.ToInt64(hdnClinicalDocId.Value);
                    string result = PHPService.InsertClinicalDoc(action,
                                                                  documentId,
                                                                  Convert.ToInt64(Session["PatientID"].ToString()),
                                                                  fileName,
                                                                  fileExt,
                                                                  fileSize,
                                                                  fileOriginalName);
                    StatusLabel.Visible = false;
                    StatusLabelsucess.Visible = true;
                    StatusLabelsucess.ForeColor = System.Drawing.Color.Green;
                    StatusLabelsucess.Text = "File uploaded!";
                   
                }
                else
                {
                    StatusLabel.Visible = true;
                    StatusLabelsucess.Visible = false;
                    StatusLabel.Text = "Only " + clinicalDocumentFormatAllowed + "files are accepted!";
                    StatusLabel.ForeColor = System.Drawing.Color.Red;
                }
                 

            }
            catch (Exception ex)
            {
                StatusLabel.Text = "The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
    }
}