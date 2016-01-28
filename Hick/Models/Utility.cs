using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Hick.Models
{
    public static class Utility
    {
        public const string GlobalDateFormat = "MM/dd/yyyy";
        public const string GlobalDateTimeFormat = "MM/dd/yy H:mm:ss";
        public const string GlobalDateMonthDayYearFormat = "MMM dd, yyyy";
        public const long CutOffSeconds = 1200; // 20 mins

        public static DateTime FirstDayOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static String GetBaseDirectory()
        {
            var basefolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Ignite");

            if (!Directory.Exists(basefolder))
            {
                Directory.CreateDirectory(basefolder);
            }

            return basefolder;
        }

        public static DateTime LastDayOfMonth(DateTime dateTime)
        {
            return FirstDayOfMonth(dateTime).AddMonths(1).AddDays(-1);
        }


        public static string PinValue
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["Bridge_Base"]);
            }
        }

        public static string ServiceUrl
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["serviceURL"]);
            }
        }
        public static string DBConnectionString
        {
            get
            {
                return Convert.ToString(ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString);
            }
        }
        public static string DBBridgeConnectionString
        {
            get
            {
                return Convert.ToString(ConfigurationManager.ConnectionStrings["ConnectionStringForHPF1"].ConnectionString);
            }
        }
        public static string AppVersion
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["HickVersion"]);
            }
        }
        public static string LogOutUrl
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["LogOutUrl"]);
            }
        }
        public static int LogOutCheckTime
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["LogOutCheckTime"]);
            }
        }
        public static int LogOutTimeInterval
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["LogOutTimeInterval"]);
            }
        }
        public static DateTime ConvertDateToLocal(string timezone, DateTime objdate)
        {
            DateTime dt;
            if (!String.IsNullOrEmpty(timezone))
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                dt = TimeZoneInfo.ConvertTimeFromUtc(objdate, tz);
            }
            else
            {
                dt = objdate.ToLocalTime();
            }
            return dt;
        }

        public static DateTime ConvertDateToUTC(string timezone, DateTime objdate)
        {
            DateTime dt;
            if (!String.IsNullOrEmpty(timezone))
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                //dt = TimeZoneInfo.ConvertTimeToUtc(objdate, tz);
                dt = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(objdate, timezone, "UTC");
            }
            else
            {
                dt = objdate.ToLocalTime();
            }
            return dt;
        }
        public static string EncryptText(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
                byte[] ba = UTF8.GetBytes(text);
                text = Convert.ToBase64String(ba);
            }
            return text;
        }
        public static string DecryptText(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {

                byte[] ba = Convert.FromBase64String(text);
                System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
                text = UTF8.GetString(ba);
            }
            return text;
        }

        //public static DateTime ConvertDateToUTC(string objdate)
        //{
        //    DateTime dt = new DateTime();
        //    if (!String.IsNullOrEmpty(objdate))
        //    {
        //        dt = DateTimeOffset.Parse(Convert.ToDateTime(objdate).ToUniversalTime().ToString()).UtcDateTime;
        //    } 
        //    return dt;
        //}

        public static string CustomerName
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["CustomerName"]);
            }
        }

        public static string LastLoggedInTime
        {

            get
            {

                if (HttpContext.Current.Session["TimeZone"] != null && HttpContext.Current.Session["LastLoggedIN"] != null)
                {
                    return Utility.ConvertDateToLocal(Convert.ToString(HttpContext.Current.Session["TimeZone"]), Convert.ToDateTime(HttpContext.Current.Session["LastLoggedIN"])).ToString("hh:mm tt");
                }
                else
                {
                    return string.Empty;
                }

            }
        }
        private static Dictionary<long, DateTime> _trackLoggedInUsers;
        public static Dictionary<long, DateTime> TrackLoggedInUsers
        {
            get
            {
                return _trackLoggedInUsers == null ? new Dictionary<long, DateTime>() : _trackLoggedInUsers;
            }
            set
            {
                _trackLoggedInUsers = value;
            }
        }

        public static List<T> PostRequest<T>(string uri, string postData)
        {
            List<T> objColl = new List<T>();
            try
            {               

                var request = (HttpWebRequest)WebRequest.Create(uri);
                var data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                JavaScriptSerializer oJS = new JavaScriptSerializer();
                objColl = oJS.Deserialize<List<T>>(responseString);              

            }
            catch (Exception exc)
            {

            }
            return objColl;
        }
        public static string PostRequestForSave(string uri, string postData)
        {
            string result = string.Empty;
            try
            {

                var request = (HttpWebRequest)WebRequest.Create(uri);
                var data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                JavaScriptSerializer oJS = new JavaScriptSerializer();
                result = oJS.Deserialize<string>(responseString);  
               

            }
            catch (Exception exc)
            {

            }
            return result;
        }

        public static string GetServiceUrl(string url)
        {
            return Path.Combine(Utility.ServiceUrl, url);
        }

        public static TimeSpan CalculateTimeSpan(ref TimeSpan duration, string value)
        {
            TimeSpan otp = new TimeSpan();
            if (TimeSpan.TryParse(value, out otp))
            {
                duration += TimeSpan.Parse(value);
            }
            else
            {
                string[] invalidValue = value.Split(new char[] { ':' });
                if (invalidValue.Length == 3)
                {
                    long tosecs = Convert.ToInt64(invalidValue[0]) * 3600 +
                         Convert.ToInt64(invalidValue[1]) * 3600 +
                          Convert.ToInt64(invalidValue[2]) * 3600;
                    duration += TimeSpan.FromSeconds(tosecs);

                }
                else if (invalidValue.Length == 2)
                {
                    long tosecs = Convert.ToInt64(invalidValue[1]) * 3600 +
                          Convert.ToInt64(invalidValue[2]) * 3600;
                    duration += TimeSpan.FromSeconds(tosecs);

                }
            }

            return duration;
        }
    }

    public class IgJObject : JObject
    {
        public IgJObject()
        {
            string pValue = ConfigurationManager.AppSettings["Bridge_Base"];
            this.Add("Pin", pValue);
        }

        public void Add(string propertyName, JToken value)
        {
            try
            {
                base.Add(propertyName, value);
            }
            catch { }
        }

    }
}