using IGNITE_MODEL.SuperUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.SuperUser.ASPX
{
    public partial class AjaxMUReportDetails : System.Web.UI.Page
    {
        SuperUserService service = new SuperUserService();
        MUReportDetailList mureportdetaillist = new MUReportDetailList();
        protected void Page_Load(object sender, EventArgs e)
        {
            int measure = (Request.QueryString["measure"].ToString() != "0") ? Convert.ToInt32(Request.QueryString["measure"].ToString()) : 0;
            GetMUReportDetails(measure);
        }

        public void GetMUReportDetails(int measure)
        {
            string[] filters = Session["MUReportFilters"].ToString().Split(',');
            mureportdetaillist = service.GetMUReportDetailList(Convert.ToInt64(filters[0]), filters[1], filters[2], filters[3], filters[4], filters[5], filters[6], filters[7], filters[8], measure);
            MUReportDetails.DataSource = mureportdetaillist.MUReportDetail_List;
            MUReportDetails.DataBind();
        }
    }
}