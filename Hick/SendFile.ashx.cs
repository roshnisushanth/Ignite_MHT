using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Hick
{
    /// <summary>
    /// Summary description for SendFile
    /// </summary>
    public class SendFile : IHttpHandler
    {



        public void ProcessRequest(HttpContext context)
        {
            bool isImage = false;
            var request = context.Request;
            var files = request.Files;
            var converstionid = Convert.ToString(request.QueryString["conversationid"]);
            var usernam = Convert.ToString(request.QueryString["username"]).ToLower();
            if (!String.IsNullOrEmpty(usernam))
            {
                var flag = Convert.ToString(request.QueryString["flag"]);
                if (flag == "image")
                {
                    isImage = true;
                }
                else
                {
                    isImage = false;
                }
                if (files.Count == 0)
                {
                    ReturnResponse(context, "Please select a file!");
                    return;
                }
                var fileName = Path.GetFileName(files[0].FileName);
                string[] allowedExtensions;
                if (isImage)
                {
                    allowedExtensions = Convert.ToString(ConfigurationManager.AppSettings["SendPictureAllowedExtensions"]).Split(',');
                }
                else
                {
                    allowedExtensions = Convert.ToString(ConfigurationManager.AppSettings["SendFileAllowedExtensions"]).Split(',');
                }
                bool fileIsAllowed = false;
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
                if (isImage)
                {
                    userFilesPath = "UserFiles/" + usernam + "/pictures";
                }
                else
                {
                    userFilesPath = "UserFiles/" + usernam + "/documents";
                }

                string userFilesDir = context.Server.MapPath(userFilesPath);
                if (!Directory.Exists(userFilesDir))
                    Directory.CreateDirectory(userFilesDir);
                string filename = fileName;
                //string filename = fileName
                //    .Replace('\\', '_').Replace('/', '_')
                //    .Replace(' ', '_').Replace('\t', '_')
                //    .Replace('-', '_').Replace('<', '_')
                //    .Replace('>', '_');
                string fileUrl = VirtualPathUtility.ToAbsolute(String.Format("~/{0}", Path.Combine(userFilesPath, filename)));
                filename = userFilesDir + @"\" + filename;

                using (Stream file = File.OpenWrite(filename))
                {
                    CopyStream(files[0].InputStream, file);
                }

                //var imageExtensions = new[] { "png", "gif", "bmp", "jpg" };
                //bool fileIsImage = false;
                //foreach (string extension in imageExtensions)
                //{
                //    if (filename.ToLower().EndsWith("." + extension.ToLower()))
                //    {
                //        fileIsImage = true;
                //        break;
                //    }
                //}

                ReturnResponse(context, String.Empty);
            }
            else
            {
                ReturnResponse(context, "Sorry an error has occured. Please contact administrator");
            }
        }

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