using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hick.Models;
using System.Configuration;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Hick.CommandCenter.UserControls
{
    public partial class Dashboard : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindConditionChart();
            BindCCMChart();
            BindBillingChart();
            Bind20MinsCompletedGraph();
        }

        public void Bind20MinsCompletedGraph()
        {

        }
        public void BindConditionChart()
        {
            List<ICDconditions> objColl = null;
            try
            {
                var uri = Utility.GetServiceUrl("conditionsgraph");

                IgJObject postData = new IgJObject();
                postData.Add("PhysicianID", Convert.ToString( Session["PhysicianID"]));

                objColl = Utility.PostRequest<ICDconditions>(uri, postData.ToString(Formatting.None));
                
                if (objColl.Count > 0)
                {
                    double[] yValues = { objColl[0].COPD, objColl[0].CAD, objColl[0].HTN, objColl[0].CHF, objColl[0].DM, objColl[0].IVD };
                    string[] xValues = { "COPD", "CAD", "HTN", "CHF", "DM", "IVD" };
                    Chart1.Series["Default"].Points.DataBindXY(xValues, yValues);

                    Chart1.Series["Default"].Points[0].Color = ColorTranslator.FromHtml("#584A9F");
                    Chart1.Series["Default"].Points[1].Color = ColorTranslator.FromHtml("#CB8A26");
                    Chart1.Series["Default"].Points[2].Color = ColorTranslator.FromHtml("#AFBA20");
                    Chart1.Series["Default"].Points[3].Color = ColorTranslator.FromHtml("#0E63A3");
                    Chart1.Series["Default"].Points[4].Color = ColorTranslator.FromHtml("#269FA8");
                    Chart1.Series["Default"].Points[5].Color = ColorTranslator.FromHtml("#E4D532");


                    Chart1.Series["Default"].ChartType = SeriesChartType.Pie;

                    Chart1.Series["Default"]["PieLabelStyle"] = "Disabled";

                    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

                    Chart1.Legends[0].Enabled = true;
                }
            }
            catch
            {

            }


            //List<ICDconditions> objColl = null;
            //try
            //{
            //    string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
            //    var uri = ConfigurationManager.AppSettings["serviceURL"] + "/conditionsgraph";
            //    var postData = "{\"Pin\":\"" + PinValue + "\"}";
            //    objColl = Utility.PostRequest<ICDconditions>(uri, postData);

            //    int i = 0;
            //    int j = 0;
            //    if (objColl.Count > 0)
            //    {
            //        string[] xValues = new string[objColl.Count];
            //        int[] yValues = new int[objColl.Count];
            //        foreach (ICDconditions item in objColl)
            //        {
            //            xValues[i] = item.CHF;
            //            i++;
            //            yValues[j] = Convert.ToInt32(item.CAD);
            //            j++;
            //        }
            //        Chart1.Series["Default"].Points.DataBindXY(xValues, yValues);
            //        //     Chart1.Series["Default"].Points[0].Color = System.Drawing.Color.MediumSeaGreen;
            //        //     Chart1.Series["Default"].Points[1].Color = System.Drawing.Color.PaleGreen;
            //        //     Chart1.Series["Default"].Points[2].Color = System.Drawing.Color.LawnGreen;
            //        //Chart1.Series["Default"].ChartType = SeriesChartType.Pie;
            //        Chart1.Series["Default"]["PieLabelStyle"] = "Disabled";
            //        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            //    }

            //}
            //catch
            //{

            //}
        }

        public void BindCCMChart()
        {
            List<CCMConditions> objColl = null;
            try
            {
                var uri = Utility.GetServiceUrl("ccmpatientgraph");

                IgJObject postData = new IgJObject();
                postData.Add("PhysicianID", Convert.ToString(Session["PhysicianID"]));

                objColl = Utility.PostRequest<CCMConditions>(uri, postData.ToString(Formatting.None));


                if (objColl.Count > 0)
                {
                    double[] yValues = { objColl[0].Current, objColl[0].Deceased, objColl[0].InActive, objColl[0].Dropped };
                    string[] xValues = { "Current", "Deceased", "Inactive", "Dropped" };
                    ChartCCM.Series["Default"].Points.DataBindXY(xValues, yValues);

                    ChartCCM.Series["Default"].Points[0].Color = ColorTranslator.FromHtml("#AFBA20");
                    ChartCCM.Series["Default"].Points[1].Color = ColorTranslator.FromHtml("#51514F");
                    ChartCCM.Series["Default"].Points[2].Color = ColorTranslator.FromHtml("#E68733");
                    ChartCCM.Series["Default"].Points[3].Color = ColorTranslator.FromHtml("#6EC1D3");



                    ChartCCM.Series["Default"].ChartType = SeriesChartType.Doughnut;

                    ChartCCM.Series["Default"]["PieLabelStyle"] = "Disabled";

                    ChartCCM.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

                    ChartCCM.Legends[0].Enabled = true;
                }
            }
            catch
            {

            }
        }

        public void BindBillingChart()
        {
            try
            {
                string constr = Utility.DBConnectionString;
                List<Users> objUserColl = new List<Users>();
                if (Session["userid"] != null)
                {
                    Users objuser = null;
                    using (SqlConnection conn = new SqlConnection())
                    {

                        conn.ConnectionString = constr;
                        conn.Open();

                        using (SqlCommand command = new SqlCommand("SP_CommandCenterBillingGraph", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@CurrentUserId", Convert.ToInt64(Session["userid"]));
                            command.Parameters.AddWithValue("@CurrentDate", DateTime.UtcNow);
                            command.Parameters.AddWithValue("@Flag", 1);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    objuser = new Users();
                                    objuser.Completed = !String.IsNullOrEmpty(Convert.ToString(reader["Completed"])) ? Convert.ToString(reader["Completed"]) : "0";
                                    objuser.NotCompleted = !String.IsNullOrEmpty(Convert.ToString(reader["NotCompleted"])) ? Convert.ToString(reader["NotCompleted"]) : "0";
                                    objuser.NoTimerLog = !String.IsNullOrEmpty(Convert.ToString(reader["NoTimer"])) ? Convert.ToString(reader["NoTimer"]) : "0";
                                    objuser.CompletedPercentage = !String.IsNullOrEmpty(Convert.ToString(reader["Percentage"])) ? Convert.ToString(reader["Percentage"]) : "0";
                                    
                                    objUserColl.Add(objuser);
                                }
                            }

                        }
                    }
                }
                if (objUserColl.Count > 0)
                {
                    //float CompletedPercentage = 0;


                    //CompletedPercentage = (Convert.Tof(objUserColl[0].Completed) / (Convert.ToDouble(objUserColl[0].NotCompleted) + Convert.ToDouble(objUserColl[0].NoTimerLog))) * 100;

                    hdn_Completed.Value = objUserColl[0].Completed;
                    hdn_notcompleted.Value = objUserColl[0].NotCompleted;
                    hdn_notimerlog.Value = objUserColl[0].NoTimerLog;
                    hdn_Complpercentage.Value = objUserColl[0].CompletedPercentage;
                }

                
                //if (objUserColl.Count > 0)
                //{
                //    double[] yValues = { Convert.ToDouble(objUserColl[0].Completed), Convert.ToDouble(objUserColl[0].NotCompleted), Convert.ToDouble(objUserColl[0].NoTimerLog) };
                //    string[] xValues = { ">20m", "<20m", "None" };
                //    ChartBillng.Series["Default"].Points.DataBindXY(xValues, yValues);

                //    ChartBillng.Series["Default"].Points[0].Color = Color.Blue;
                //    ChartBillng.Series["Default"].Points[1].Color = Color.PaleGreen;
                //    ChartBillng.Series["Default"].Points[2].Color = Color.Purple;



                //    ChartBillng.Series["Default"].ChartType = SeriesChartType.Column;

                //    ChartBillng.Series["Default"]["PieLabelStyle"] = "Disabled";

                //    ChartBillng.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

                //    //ChartCCM.Legends[0].Enabled = true;
                //}
            }
            catch (Exception)
            {

            }
        }
    }
}