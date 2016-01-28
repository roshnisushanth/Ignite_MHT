using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Hick.Models;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Hick.CommandCenter.ASPX
{
    /// <summary>
    /// Summary description for UploadConsentForm
    /// </summary>
    public class UploadConsentForm : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try {
                var request = context.Request;
                var files = request.Files;
                var userid = Convert.ToString(request.QueryString["userid"]);
                var username = Convert.ToString(request.QueryString["username"]).ToLower();
                var patid = Convert.ToString(request.QueryString["patid"]).ToLower();
                var refpatid = Convert.ToString(request.QueryString["refpatid"]).ToLower();
                if (!String.IsNullOrEmpty(username))
                {
                    if (files.Count == 0)
                    {
                        ReturnResponse(context, "Please select a file!");
                        return;
                    }

                    string allowedExts = "pdf,doc,docx";
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(ConfigurationManager.AppSettings["ConsentFormFormatAllowed"])))
                    {
                        allowedExts = Convert.ToString(ConfigurationManager.AppSettings["ConsentFormFormatAllowed"]);
                    }

                    string[] allowedExtensions = allowedExts.Split(',');

                    bool fileIsAllowed = false;
                    var fileName = Path.GetFileName(files[0].FileName);
                    var fileExt = Path.GetExtension(files[0].FileName);
                    foreach (string extension in allowedExtensions)
                    {
                        if (fileName.ToLower().EndsWith("." + extension.Trim().ToLower()))
                        {
                            fileIsAllowed = true;
                            break;
                        }
                    }

                    if (!fileIsAllowed)
                    {
                        ReturnResponse(context, "The file type is not allowed!");
                        return;
                    }

                    string userFilesPath = string.Empty;
                    userFilesPath = Path.Combine("../../userfiles", "consentforms", patid);

                    string userFilesDir = context.Server.MapPath(userFilesPath);
                    if (!Directory.Exists(userFilesDir))
                        Directory.CreateDirectory(userFilesDir);

                    string filename = fileName;
                    //string fileUrl = VirtualPathUtility.ToAbsolute(String.Format("~/{0}/", Path.Combine(userFilesDir, patid)));
                    filename = userFilesDir + @"\" + patid + fileExt;

                    using (Stream file = File.OpenWrite(filename))
                    {
                        CopyStream(files[0].InputStream, file);
                    }

                    UpdateFlag(refpatid,patid, fileExt);

                    ReturnResponse(context, "success");
                }
                else
                {
                    ReturnResponse(context, "Sorry an error has occured. Please contact administrator");
                }
            }
            catch (Exception ex)
            {
                ReturnResponse(context, ex.StackTrace);
            }
        }

        private void UpdateFlag(string refpatid, string patid, string fileExt)
        {
            HickChatEngine sc = new HickChatEngine();
            long patientId = 0;
            long.TryParse(patid, out patientId);

            long refpatientId = 0;
            long.TryParse(refpatid, out refpatientId);
            if (patientId > 0)
            {
                
                sc.UpdateConsented(patientId, fileExt);
            }
            if (refpatientId > 0)
            {
                UpdateConsentedInBridge(refpatientId); //to update isconsented in bridge
            }
        }

        private void UpdateConsentedInBridge(long refpatientId)
        {
            try {
                var uri = Utility.GetServiceUrl("updateconsented");
                IgJObject postData = new IgJObject();
                postData.Add("PatientID", refpatientId);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));
            }
            catch(Exception)
            { }
        }

        //End : to update isconsented in bridge
        private void ReturnResponse(HttpContext context, string error)
        {
            var files = context.Request.Files;
            var fileName = Path.GetFileName(files[0].FileName);

            context.Response.Clear();
            context.Response.Write("{");
            error = error + "|" + fileName;
            context.Response.Write("error: '" + error + "'");
            context.Response.Write("}");
            context.Response.Flush();
        }

        private static void CopyStream(Stream input, Stream output)
        {
            var buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}