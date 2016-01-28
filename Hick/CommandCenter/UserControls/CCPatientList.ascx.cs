using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hick.Models;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using IGNITE.DBUtility;
using Dal.Encryption;

namespace Hick.CommandCenter.UserControls
{
    public partial class CCPatientList : System.Web.UI.UserControl
    {
        EncryptDecryptUtil ecd = new EncryptDecryptUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var pagekey = Page.MetaKeywords;
                int flag = 0;
                if (pagekey == "NotComplete")
                {
                    mylabel.Text = "Patient List Not Complete";
                    flag = 2;
                }
                else if (pagekey == "Complete")
                {
                    mylabel.Text = "Patient List Complete";
                    flag = 1;
                }
                else
                {
                    mylabel.Text = "Patient List";
                }
                BindPatientsGrid(flag);

            }

        }
        public void BindPatientsGrid(int flag)
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

                        using (SqlCommand command = new SqlCommand("sp_HickPatientList", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@CurrentUserId", Convert.ToInt64(Session["userid"]));
                            command.Parameters.AddWithValue("@CurrentDate", DateTime.UtcNow);
                            command.Parameters.AddWithValue("@Flag", flag);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    objuser = new Users();
                                    string lastname = !String.IsNullOrEmpty(Convert.ToString(reader["Lastname"])) ? Convert.ToString(reader["Lastname"]) : "NA";
                                    objuser.Lastname = ecd.DecryptData(lastname, ecd.GetEncryptType());
                                    string firstname = !String.IsNullOrEmpty(Convert.ToString(reader["Firstname"])) ? Convert.ToString(reader["Firstname"]) : "NA";
                                    objuser.Firstname = ecd.DecryptData(firstname, ecd.GetEncryptType());
                                    string dob = !String.IsNullOrEmpty(Convert.ToString(reader["dateofbirth"])) ? Convert.ToString(reader["dateofbirth"]) : "NA";
                                    objuser.DateOfBirth = ecd.DecryptData(dob, ecd.GetEncryptType());
                                    
                                    string PhoneNumber = !String.IsNullOrEmpty((reader["phone_number"]).ToString()) ? (reader["phone_number"]).ToString() : "NA";
                                    objuser.Physician = ecd.DecryptData(reader["PhysicianFirst"].ToString(), ecd.GetEncryptType());
                                    objuser.Physician = objuser.Physician + " " + ecd.DecryptData(reader["PhysicianLast"].ToString(), ecd.GetEncryptType()); 

                                    objuser.ReferenceID = DBHelper.getInt64(reader, "referenceid");
                                    objuser.TotalChatDuration = !String.IsNullOrEmpty(Convert.ToString(reader["TotalDuration"])) ? Convert.ToString(reader["TotalDuration"]) : "NA";

                                    if (objuser.TotalChatDuration != "NA")
                                    {
                                        TimeSpan obj = TimeSpan.Parse(objuser.TotalChatDuration);
                                        string hrs = obj.Hours > 0 ? obj.Hours + "h " : string.Empty;
                                        string mts = obj.Minutes > 0 ? obj.Minutes + "m " : string.Empty;
                                        string sec = obj.Seconds > 0 ? obj.Seconds + "s " : string.Empty;
                                        objuser.TotalChatDuration = String.Format("{0}{1}{2}", hrs, mts, sec);
                                    }
                                    objUserColl.Add(objuser);
                                }
                            }


                        }
                    }
                }
                gvccpatientlist.DataSource = objUserColl;
                gvccpatientlist.DataBind();
            }
            catch (Exception)
            {

            }

        }

        protected void btnexportpatientlist_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }
       
        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "PatientList" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvccpatientlist.GridLines = GridLines.Both;
            gvccpatientlist.HeaderStyle.Font.Bold = true;
            gvccpatientlist.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();



        }

        protected void btndownloadpdf_Click(object sender, EventArgs e)
        {
            ExportGridToPDF(); 
        }
        private void ExportGridToPDF()
        {

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=PatientList.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvccpatientlist.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            gvccpatientlist.AllowPaging = true;
            gvccpatientlist.DataBind();
        }  
        
    }
}