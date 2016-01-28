using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;

namespace Hick
{
    public class MvcApplication : System.Web.HttpApplication
    {
        Thread thread = null;
        public int timeinterval = Hick.Models.Utility.LogOutTimeInterval;
        public int checktime = Hick.Models.Utility.LogOutCheckTime;
        protected void Application_Start()
        {
            //thread = new Thread(new ThreadStart(ThreadFunc));
            //thread.IsBackground = true;
            //thread.Name = "ThreadFunc";
            //thread.Start();

        }
        protected void Session_Start()
        {
            //if (thread != null && thread.ThreadState != ThreadState.Running)
            //{
            //    thread = new Thread(new ThreadStart(ThreadFunc));
            //    thread.IsBackground = true;
            //    thread.Name = "ThreadFunc";
            //    thread.Start();
            //}
            thread = new Thread(new ThreadStart(ThreadFunc));
            thread.IsBackground = true;
            thread.Name = "ThreadFunc";
            thread.Start();
           
        }

        protected void ThreadFunc()
        {
            System.Timers.Timer t = new System.Timers.Timer();
            t.Elapsed += new System.Timers.ElapsedEventHandler(TimerWorker);
            t.Interval = timeinterval;
            t.Enabled = true;
            t.AutoReset = true;
            t.Start();
        }

        protected void TimerWorker(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {              
               
                var r = new Dictionary<long,DateTime>(Hick.Models.Utility.TrackLoggedInUsers);
                if (r != null && r.Count > 0)
                {
                    DateTime ct = DateTime.Now;
                    foreach (var item in r)
                    {
                        TimeSpan duration = new TimeSpan(ct.Ticks - item.Value.Ticks);
                        if (duration.Seconds > checktime)
                        {

                            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                            using (SqlConnection conn = new SqlConnection())
                            {
                                conn.ConnectionString = constr;
                                conn.Open();

                                string updateString = @"update Hick_Users set Status='0', StatusMessage='Offline' where ID='" + item.Key.ToString() + "' ";

                                SqlCommand cmd = new SqlCommand(updateString, conn);
                                SqlDataAdapter adp = new SqlDataAdapter();
                                adp.UpdateCommand = cmd;
                                int res = adp.UpdateCommand.ExecuteNonQuery();
                                if (res >= 0)
                                {
                                    Hick.Models.Utility.TrackLoggedInUsers.Remove(item.Key);                                 
                                }

                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {


            }
        }
    }
}
