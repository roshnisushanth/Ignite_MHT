using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hick.Models;

namespace Hick.PatientLookUp.UserControls
{
    public partial class UCPatientList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                BindPatientGrid();            
            }
        }
        public void BindPatientGrid() {

            var data = Cache["PatientData"];

            patientList.DataSource = data;
            patientList.DataBind();
        }
    }
}