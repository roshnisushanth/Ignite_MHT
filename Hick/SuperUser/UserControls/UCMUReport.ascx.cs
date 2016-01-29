using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hick.SuperUser;
using IGNITE_MODEL.SuperUser;
using System.Globalization;
using ClosedXML.Excel;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
namespace Hick.SuperUser.UserControls
{
    public partial class UCMUReport : System.Web.UI.UserControl
    {
        string Q1_start = null, Q2_start = null, Q3_start = null, Q4_start = null;
        string Q1_end = null, Q2_end = null, Q3_end = null, Q4_end = null;
        long providerId = 0;
        int measure_1 = 0, measure_2 = 0, measure_3 = 0;
        SuperUserService service = new SuperUserService();
        ProviderList providelist = new ProviderList();
        MUReportList mureportlist = new MUReportList();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ProviderListBinding();
                gdmureport.DataSource = dt;
                gdmureport.DataBind();
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            bindvaluetovariable();
            Reportbinding();
        }


        private void bindvaluetovariable()
        {
            lblmeasures.Text = "";
            reportingperiodValue.Text = "";
            if (calenderyear.Checked == true)
            {
                reportingperiod.Text = "Calender Year :";
                if (CYQ1.Checked == true)
                {
                    Q1_start = "01/01/" + DateTime.Now.Year.ToString();
                    Q1_end = "31/03/" + DateTime.Now.Year.ToString();
                    reportingperiodValue.Text = reportingperiodValue.Text + ",CYQ1";
                }
                if (CYQ2.Checked == true)
                {
                    Q2_start = "01/04/" + DateTime.Now.Year.ToString();
                    Q2_end = "30/06/" + DateTime.Now.Year.ToString();
                    reportingperiodValue.Text = reportingperiodValue.Text + ",CYQ2";
                }
                if (CYQ3.Checked == true)
                {
                    Q3_start = "01/07/" + DateTime.Now.Year.ToString();
                    Q3_end = "30/09/" + DateTime.Now.Year.ToString();
                    reportingperiodValue.Text = reportingperiodValue.Text + ",CYQ3";
                }
                if (CYQ4.Checked == true)
                {
                    Q4_start = "01/10/" + DateTime.Now.Year.ToString();
                    Q4_end = "31/12/" + DateTime.Now.Year.ToString();
                    reportingperiodValue.Text = reportingperiodValue.Text + ",CYQ4";
                }
            }
            if (Federalyear.Checked == true)
            {
                reportingperiod.Text = "Federal Fiscal Year :";
                if (FFQ1.Checked == true)
                {
                    Q1_start = "01/10/" + DateTime.Now.Year.ToString();
                    Q1_end = "31/12/" + DateTime.Now.Year.ToString();
                    reportingperiodValue.Text = reportingperiodValue.Text + ",CYQ1";
                }
                if (FFQ2.Checked == true)
                {
                    Q2_start = "01/01/" + DateTime.Now.Year.ToString();
                    Q2_end = "31/03/" + DateTime.Now.Year.ToString();
                    reportingperiodValue.Text = reportingperiodValue.Text + ",CYQ2";
                }
                if (FFQ3.Checked == true)
                {
                    Q3_start = "01/04/" + DateTime.Now.Year.ToString();
                    Q3_end = "30/06/" + DateTime.Now.Year.ToString();
                    reportingperiodValue.Text = reportingperiodValue.Text + ",CYQ3";
                }
                if (FFQ4.Checked == true)
                {
                    Q4_start = "01/07/" + DateTime.Now.Year.ToString();
                    Q4_end = "30/09/" + DateTime.Now.Year.ToString();
                    reportingperiodValue.Text = reportingperiodValue.Text + ",CYQ4";
                }
            }

            if (customdate.Checked == true)
            {
                ////DateTime from_date = DateTime.ParseExact(fromdate.Text, "dd/MM/yyyy", null);
                ////Q1_start = from_date.ToShortDateString();
                ////DateTime to_date = DateTime.ParseExact(todate.Text, "dd/MM/yyyy", null);
                ////Q1_end = to_date.ToShortDateString();
                string[] fromdatesplit;
                string[] todatesplit;
                Q1_start = fromdate.Text;
                fromdatesplit = Q1_start.Split('/');
                Q1_start = fromdatesplit[1] + "/" + fromdatesplit[0] + "/" + fromdatesplit[2];
                Q1_end = todate.Text;
                todatesplit = Q1_end.Split('/');
                Q1_end = todatesplit[1] + "/" + todatesplit[0] + "/" + todatesplit[2];
                customfromdate.Text = Q1_start;
                customtodate.Text = Q1_end;
            }

            if (dropprovider.SelectedValue == "ALL")
            {
                providerId = 0;
                provider.Text = "ALL";
            }
            else
            {
                providerId = Convert.ToInt64(dropprovider.SelectedValue);
                provider.Text = dropprovider.SelectedItem.Text;
            }

            if (measure1.Checked == true)
            {
                measure_1 = 1;
                lblmeasures.Text = lblmeasures.Text+ ",Patient Electronic Access Measure 1 Stage 1";
            }
            if (measure2.Checked == true)
            {
                measure_2 = 1;
                lblmeasures.Text = lblmeasures.Text + ",Patient Electronic Access Measure 1 Stage 2";
            }
            if (measure3.Checked == true)
            {
                measure_3 = 1;
                lblmeasures.Text = lblmeasures.Text + ",Patient Electronic Access Measure 2 Stage 2";
            }
            lblmeasures.Text = lblmeasures.Text.Trim().TrimStart(',');
            reportingperiodValue.Text= reportingperiodValue.Text.Trim().TrimStart(',');

            Session["MUReportFilters"] = providerId + "," + Q1_start + "," + Q1_end + "," + Q2_start + "," + Q2_end + "," + Q3_start + "," + Q3_end + "," + Q4_start + "," + Q4_end;
        }
        private void Reportbinding()
        {
            mureportlist = service.GetMUReportList(providerId, Q1_start, Q1_end, Q2_start, Q2_end, Q3_start, Q3_end, Q4_start, Q4_end, measure_1, measure_2, measure_3);        
            gdmureport.DataSource = mureportlist.MUReport_List;
            gdmureport.DataBind();
            grdprintreport.DataSource = mureportlist.MUReport_List;
            grdprintreport.DataBind();
            Session["MUReport"] = mureportlist;
        }
        private void ProviderListBinding()
        {
            providelist = service.GetProvideList("GetProvider");
            dropprovider.DataSource = providelist.Provider_List;
            dropprovider.DataTextField = "FirstName";
            dropprovider.DataValueField = "Id";

            dropprovider.DataBind();
            dropprovider.Items.Insert(0, new ListItem("ALL", "ALL"));
        }

        protected void gdmureport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string lbl_measure = e.Row.Cells[0].Text;
                    string lbl_percent = e.Row.Cells[3].Text;
          

                    if (lbl_measure == "Patient Electronic Access Measure2 stage2")
                    {
                        if (Convert.ToInt32(lbl_percent) <= 5)
                        {
                            e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                            e.Row.Cells[3].Font.Bold = true;
                            e.Row.Cells[4].Text = "No";             
                        }
                        else
                        {
                            e.Row.Cells[4].Text = "Yes";
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(lbl_percent) <= 50)
                        {
                            e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                            e.Row.Cells[3].Font.Bold = true;
                            e.Row.Cells[4].Text = "No";
                        }
                        else
                        {
                            e.Row.Cells[4].Text = "Yes";
                        }
    
                    }

                }
            }
            catch
            {

            }
        }

        protected void ExportExcel(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("GridView_Data");
            foreach (TableCell cell in gdmureport.HeaderRow.Cells)
            {
               
                dt.Columns.Add(cell.Text);
              
            }
            dt.Columns.RemoveAt(5);
            foreach (GridViewRow row in gdmureport.Rows)
            {
                dt.Rows.Add();
                for (int i = 0; i < 5; i++)
                {
                    dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text;
                }
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Meaningful_Use_Report.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void XMLEXPORT_Click(object sender, EventArgs e)
        {
            if (Session["MUReport"] != null)
            {
                mureportlist = (MUReportList)Session["MUReport"];
            }
            else
            {
                bindvaluetovariable();
                mureportlist = service.GetMUReportList(providerId, Q1_start, Q1_end, Q2_start, Q2_end, Q3_start, Q3_end, Q4_start, Q4_end, measure_1, measure_2, measure_3);
            }
            var serializer = new XmlSerializer(typeof(List<MUReport>));
            MemoryStream memoryStream = new MemoryStream();
            XmlTextWriter tw = new XmlTextWriter(memoryStream, new UTF8Encoding(true, true));
            byte[] data;
            using (var memStm = new MemoryStream())
            using (var xw = XmlWriter.Create(memStm))
            {
                serializer.Serialize(xw, mureportlist.MUReport_List);
                data = memStm.ToArray();
            }
            memoryStream.Flush();
            memoryStream.Close();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "text/xml";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename= Meaningful_Use_Report.xml");
            HttpContext.Current.Response.AddHeader("Content-Length", data.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.BinaryWrite(data);
            HttpContext.Current.Response.End();
        }

    }
}