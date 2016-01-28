using Hick.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;
using System.Data;
using IGNITE_MODEL;
using IGNITE_BLL;
using IGNITE.DBUtility;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Data.SqlTypes;
using Newtonsoft.Json.Linq;
using System.ServiceModel.Web;
using IGNITE_MODEL.PHPView;
namespace Hick.PHP
{
    [ServiceContract(Namespace = "")] 
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PHPService
    {
        [OperationContract]
        public ClinicalDocumentList GetClinicalDoc(string action,long Id, long userId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("@action",action),
                 new SqlParameter("@Id",Id),
                 new SqlParameter("@PatientId",userId)
            };

            var list = new List<IGNITE_MODEL.PHPView.ClinicalDocument>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_GetClinicalDocument", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var clinicalobj = new IGNITE_MODEL.PHPView.ClinicalDocument();
                    clinicalobj.Id = DBHelper.getInt64(sqlobj, "Id");
                    clinicalobj.PatientId = DBHelper.getInt64(sqlobj, "PatientId");
                    clinicalobj.FileName = DBHelper.getString(sqlobj, "FileName");
                    clinicalobj.FileExt = DBHelper.getString(sqlobj, "FileExt");
                    clinicalobj.Size = DBHelper.getString(sqlobj, "Size");
                    clinicalobj.UploadedDate = DBHelper.getDateTime(sqlobj, "DateUploaded").ToShortDateString();
                    clinicalobj.UploadedFileName = DBHelper.getString(sqlobj, "UploadedFileName");
                    list.Add(clinicalobj);
                }
            }

            return new ClinicalDocumentList
            {
               ClinicalDocumentLists=list
            };

        }



        [OperationContract]
        public string InsertClinicalDoc(string action, long id, long patientId, string fileName, string fileExt, string size, string uploadedFileName)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("@Action",action),
                new SqlParameter("@Id",id),
                new SqlParameter("@PatientId",patientId),
                new SqlParameter("@FileName",fileName),
                new SqlParameter("@FileExt",fileExt),
                new SqlParameter("@Size",size),
                new SqlParameter("@UploadedFileName",uploadedFileName)
            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_InsertClinicalDocumentFile", sqlParms);
            return (string)obj;

        }

       
    }
}