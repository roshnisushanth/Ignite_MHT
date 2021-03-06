﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Hick
{
    /// <summary>
    /// Summary description for FlashResource
    /// </summary>
    public class FlashResource : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            string resourceName = context.Request.Params["resname"];
            if (resourceName != "DetectWebcam")
                resourceName = resourceName + "_" + "fms";

            using (Stream swfFile = Assembly.GetExecutingAssembly().GetManifestResourceStream("Hick." + resourceName + ".swf"))
            {
                context.Response.ContentType = "application/x-shockwave-flash";
                context.Response.AddHeader("content-length", swfFile.Length.ToString());

                byte[] buffer = new byte[swfFile.Length];

                while (swfFile.Read(buffer, 0, buffer.Length) > 0)
                {
                    context.Response.BinaryWrite(buffer);
                }

                context.Response.Flush();
                context.Response.End();
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