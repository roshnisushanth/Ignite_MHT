
using System;
using System.Collections.Generic;
using IGNITE_DAL.Interfaces;
using IGNITE_MODEL;
using System.Data.SqlClient;
using IGNITE.DBUtility;
using System.Data;

namespace IGNITE_DAL.DataObjects
{
    public class AuditManager : IAuditManager
    {
        public AuditWrapper GetAllAudits(long currentuserid, string usertype, int PageIndex, int PageSize)
        {

            SqlParameter[] sqlParms = new SqlParameter[]{new SqlParameter("UserId",currentuserid)};

            List<IGNITE_MODEL.Audit> auditList = new List<IGNITE_MODEL.Audit>();

            using (SqlDataReader sqlGetAudits = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_GetAllAudits", sqlParms))
            {
                while (sqlGetAudits.Read())
                {
                    var audit = new IGNITE_MODEL.Audit();
                    audit.ActionType = GetActionTypeString(DBHelper.getInt16(sqlGetAudits, "Type"));
                    //audit.Email = DBHelper.getString(sqlGetAudits, "Username");

                    audit.Email = DBHelper.getString(sqlGetAudits, "Email");
                    audit.Date = DBHelper.getDateTime(sqlGetAudits, "ActionDate").ToString("MM/dd/yyyy HH:mm:ss");
                    audit.InformationType = DBHelper.getString(sqlGetAudits, "InformationType");
                    //audit.NewValue = DBHelper.getString(sqlGetAudits, "NewValue");
                    //audit.OldValue = DBHelper.getString(sqlGetAudits, "OldValue");
                    auditList.Add(audit);
                }

                return new AuditWrapper()
                {
                    AuditColl = auditList
                };
            }
        }

        private string GetActionTypeString(int v)
        {
            switch (v)
            {
                case 0:
                    return "View";
                case 1:
                    return "Download";
                case 2:
                    return "Transmit";
                default:
                    return "View";
            }
        }
    }
}
