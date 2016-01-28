using Hick.Models;
using IGNITE.DBUtility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.PatientLookUp.ASPX
{
    public partial class ClinicalDocuments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
      public static string DeleteClinicalDocuments(long Id)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("@ClinicalDocumentsID",Id),               
            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_DeleteClinicalDocumentFile", sqlParms);
            return (string)obj;

        }
        
    }
}