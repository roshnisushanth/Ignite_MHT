using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Hick
{
    /// <summary>
    /// Summary description for ReceiveFile
    /// </summary>
    public class ReceiveFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string filname = Convert.ToString(context.Request.QueryString["filename"]);
                string peernam = Convert.ToString(context.Request.QueryString["peername"]).ToLower();
                string usernam = Convert.ToString(context.Request.QueryString["username"]).ToLower();

                var imageExtensions = Convert.ToString(ConfigurationManager.AppSettings["SendPictureAllowedExtensions"]).Split(',');
                bool fileIsImage = false;
                foreach (string extension in imageExtensions)
                {
                    if (filname.ToLower().EndsWith("." + extension.ToLower()))
                    {
                        fileIsImage = true;
                        break;
                    }
                }
                string sourcefilepath = string.Empty;
                string destfilepath = string.Empty;
                if (fileIsImage)
                {
                    sourcefilepath = "UserFiles/" + peernam + "/pictures";
                    destfilepath = "UserFiles/" + usernam + "/pictures";
                }
                else
                {
                    sourcefilepath = "UserFiles/" + peernam + "/documents";
                    destfilepath = "UserFiles/" + usernam + "/documents";
                }
                string filename = filname;
                string fileUrl = VirtualPathUtility.ToAbsolute(String.Format("~/{0}", Path.Combine(destfilepath, filename)));
                string peerFilesDir = context.Server.MapPath(sourcefilepath);
                string userFilesDir = context.Server.MapPath(destfilepath);
                if (!Directory.Exists(userFilesDir))
                    Directory.CreateDirectory(userFilesDir);

                sourcefilepath = peerFilesDir + @"\" + filename;
                destfilepath = userFilesDir + @"\" + filename;
                if (System.IO.File.Exists(sourcefilepath))
                {
                   
                    System.IO.File.Copy(sourcefilepath, destfilepath, true);
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filname + "\"");
                    //context.Response.AddHeader("Content-Length", filname.Length.ToString());
                    //context.Response.ContentType = "application/octet-stream";                    
                    context.Response.TransmitFile(context.Server.MapPath(fileUrl));                   
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    context.Response.Clear();
                    context.Response.Write("<script type='text/javascript'> parent.isdownloadfailed=1;alert('File not found. Please contact administrator');</script>");
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {                
                context.Response.Clear();
                context.Response.Write("<script type='text/javascript'> parent.isdownloadfailed=2;alert('Sorry an error has occured. Please contact administrator');</script>");
                HttpContext.Current.ApplicationInstance.CompleteRequest();
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