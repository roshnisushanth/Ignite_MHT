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
using IGNITE_MODEL.SuperUser;
using Dal.Encryption;

namespace Hick.SuperUser
{
    [ServiceContract(Namespace = "")] 
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SuperUserService
    {
        EncryptDecryptUtil ecd = new EncryptDecryptUtil();
        [OperationContract]
        public ProviderList GetProvideList(string action)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("@action",action)
            };

            var list = new List<IGNITE_MODEL.Contact>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_GetPhysician", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var providerobj = new IGNITE_MODEL.Contact();
                    providerobj.FirstName = ecd.DecryptData(DBHelper.getString(sqlobj, "Firstname"), ecd.GetEncryptType()) + " " + ecd.DecryptData(DBHelper.getString(sqlobj, "Lastname"), ecd.GetEncryptType());
                    providerobj.Id = (DBHelper.getInt64(sqlobj, "ID"));

                    list.Add(providerobj);
                }
            }

            return new ProviderList
            {
                Provider_List = list
            };

        }



        [OperationContract]
        public MUReportList GetMUReportList(long physicanid
                                             ,string Q1_start
                                             ,string Q1_end
                                             ,string Q2_start
                                             ,string Q2_end
                                             , string Q3_start
                                             , string Q3_end
                                             , string Q4_start
                                             , string Q4_end
                                             , int measure1stage1
                                             , int measure1stage2
                                             , int measure2stage2)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("@physicanid",physicanid),
                new SqlParameter("@Q1_start",Q1_start),
                new SqlParameter("@Q1_end",Q1_end),
                new SqlParameter("@Q2_start",Q2_start),
                new SqlParameter("@Q2_end",Q2_end),
                new SqlParameter("@Q3_start",Q3_start),
                new SqlParameter("@Q3_end",Q3_end),
                new SqlParameter("@Q4_start",Q4_start),
                new SqlParameter("@Q4_end",Q4_end),
                new SqlParameter("@measure1stage1",measure1stage1),
                new SqlParameter("@measure1stage2",measure1stage2),
                new SqlParameter("@measure2stage2",measure2stage2)
            };

            var list = new List<IGNITE_MODEL.SuperUser.MUReport>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_GetMUreport", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var MUobj = new IGNITE_MODEL.SuperUser.MUReport();
                    MUobj.MeasureNumber = DBHelper.getString(sqlobj, "MeasureNumber");
                    MUobj.Denominator_Count = DBHelper.getInt(sqlobj, "Tot_Population");
                    MUobj.Numerator_Count = DBHelper.getInt(sqlobj, "Elg_Population");
                    MUobj.Percentage = DBHelper.getString(sqlobj, "Perform");
                    MUobj.Meet = DBHelper.getString(sqlobj, "meet");
                    list.Add(MUobj);
                }
            }

            return new MUReportList
            {
                MUReport_List = list
            };

        }




        [OperationContract]
        public MUReportDetailList GetMUReportDetailList(long physicanid
                                     , string Q1_start
                                     , string Q1_end
                                     , string Q2_start
                                     , string Q2_end
                                     , string Q3_start
                                     , string Q3_end
                                     , string Q4_start
                                     , string Q4_end
                                     , int measure)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("@physicanid",physicanid),
                new SqlParameter("@Q1_start",Q1_start),
                new SqlParameter("@Q1_end",Q1_end),
                new SqlParameter("@Q2_start",Q2_start),
                new SqlParameter("@Q2_end",Q2_end),
                new SqlParameter("@Q3_start",Q3_start),
                new SqlParameter("@Q3_end",Q3_end),
                new SqlParameter("@Q4_start",Q4_start),
                new SqlParameter("@Q4_end",Q4_end),
                new SqlParameter("@measure",measure)
            };

            var list = new List<IGNITE_MODEL.SuperUser.MUReportDetails>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_GetMUreport_Popup", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var MUobj = new IGNITE_MODEL.SuperUser.MUReportDetails();
                    MUobj.PatientName = DBHelper.getString(sqlobj, "patient_Name");
                    MUobj.DataType = DBHelper.getString(sqlobj, "InformationType");
                    MUobj.DataValue = DBHelper.getString(sqlobj, "DataValue");
                    MUobj.DateOfActivity = DBHelper.getString(sqlobj, "Audit_Date");
                    MUobj.TimeOfActivity = DBHelper.getString(sqlobj, "Audit_Time");
                    MUobj.IpAddress = "";//DBHelper.getString(sqlobj, "meet");
                    list.Add(MUobj);
                }
            }

            return new MUReportDetailList
            {
                MUReportDetail_List = list
            };

        }

    }
}