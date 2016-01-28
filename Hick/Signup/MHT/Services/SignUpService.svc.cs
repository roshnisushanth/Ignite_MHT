using Hick.Models;
using IGNITE.DBUtility;
using IGNITE_MODEL;
using IGNITE_MODEL.SignUp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Hick.SignUp
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SignUpService
    {
        [OperationContract]
        public List<Provider> Getproviders()
        {
            SqlParameter[] sqlParms = new SqlParameter[] { };

            var list = new List<Provider>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(Utility.DBBridgeConnectionString, CommandType.StoredProcedure, "SignUp_GetProviders", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var robj = new Provider();
                    robj.Id = DBHelper.getInt(sqlobj, "ProviderID");
                    robj.Name = DBHelper.getString(sqlobj, "ProviderName");
                    list.Add(robj);
                }
            }
            return list;
        }

        [OperationContract]
        public Wrapper GetPhysicianByProvider(string providerId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("ProviderId",Convert.ToInt64(providerId))
            };

            var list = new List<Physician>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(Utility.DBBridgeConnectionString, CommandType.StoredProcedure, "SignUp_GetPhysiciansByProvider", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var robj = new Physician();
                    robj.Id = DBHelper.getInt(sqlobj, "PhysicianId");
                    robj.Name = DBHelper.getString(sqlobj, "Name");
                    list.Add(robj);
                }
            }

            return new Wrapper
            {
                Rows = list
            };

        }

        [OperationContract]
        public Contact GetBridgePatient(string firstName, string lastName, DateTime dob, long physicianId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("FirstName",firstName),
                new SqlParameter("LastName",lastName),
                new SqlParameter("DOB",dob),
                new SqlParameter("PhysicianId",physicianId)
            };
            var robj = new Contact();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(Utility.DBBridgeConnectionString, CommandType.StoredProcedure, "SignUp_GetPatientDetails", sqlParms))
            {
                while (sqlobj.Read())
                {
                    robj.Id = DBHelper.getInt64(sqlobj, "Id");
                    robj.FirstName = DBHelper.getString(sqlobj, "FirstName");
                    robj.LastName = DBHelper.getString(sqlobj, "LastName");
                    robj.DOB = DBHelper.getDateTime(sqlobj, "DOB");
                    robj.ReferenceId = DBHelper.getInt64(sqlobj, "ReferenceId");
                    robj.ProviderId = DBHelper.getInt64(sqlobj, "ProviderID");
                    robj.PhoneNumber = DBHelper.getString(sqlobj, "PhoneNumber");
                    robj.Email = DBHelper.getString(sqlobj, "Email");
                }
            }
            return robj;
        }

        [OperationContract]
        public bool CreateIgniteContact(Contact contact)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("FirstName",contact.FirstName),
                new SqlParameter("UserName",(Convert.ToString(contact.FirstName).Substring(0, 1) + Convert.ToString(contact.LastName)).ToLower().Trim()),
                new SqlParameter("LastName",contact.LastName),
                new SqlParameter("DOB",contact.DOB),
                new SqlParameter("ProviderId",contact.ProviderId),
                new SqlParameter("PhysicianId",contact.PhysicianId),
                new SqlParameter("ReferenceId",contact.ReferenceId),
                new SqlParameter("PhoneNumber",contact.PhoneNumber),
                new SqlParameter("EmailId",contact.Email),
                new SqlParameter("Password",contact.Password)
            };

            int ret = (int)SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "SignUp_CreateNewContact", sqlParms.ToArray());

            if (ret > 0)
            {
                return true;
            }
            return false;
        }

    }
}