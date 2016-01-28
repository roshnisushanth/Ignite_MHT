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
using IGNITE_MODEL.Encounters;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Data.SqlTypes;
using Newtonsoft.Json.Linq;
using System.ServiceModel.Web;
using Dal.Encryption;

namespace Hick.Encounters
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EncounterService
    {

        EncryptDecryptUtil ecd = new EncryptDecryptUtil();
        [OperationContract]
        public long AddSummary(string PeerId, string UserId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("UserId",Convert.ToInt64(UserId)),
                new SqlParameter("PeerId",Convert.ToInt64(PeerId))
            };

            var newId = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_AddSummary", sqlParms.ToArray());

            return (long)newId;
        }

        [OperationContract]
        public SummaryWrapper GetAssessmentSummaries(long userId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("UserId",userId)
            };

            var list = new List<IGNITE_MODEL.Encounters.AssessmentSummary>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetAllAssessmentSummaries", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var robj = new IGNITE_MODEL.Encounters.AssessmentSummary();
                    robj.AssessmentId = DBHelper.getInt64(sqlobj, "AssessmentId");
                    robj.AssesmentBy = DBHelper.getString(sqlobj, "AssessmentBy");
                    robj.Date = DBHelper.getDateTime(sqlobj, "AssessmentDate");
                    list.Add(robj);
                }
            }

            return new SummaryWrapper
            {
                Rows = list
            };
        }

        [OperationContract]
        public MomOB GetMomOB(long assessmentId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",assessmentId)
            };
            var robj = new MomOB();
            var list = new List<MomOB>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetMomOB", sqlParms))
            {
                while (sqlobj.Read())
                {
                    robj.Id = DBHelper.getInt64(sqlobj, "Id");
                    robj.AssessmentId = DBHelper.getInt64(sqlobj, "AssessmentId");
                    robj.Address1 = DBHelper.getString(sqlobj, "Address1");
                    robj.Address2 = DBHelper.getString(sqlobj, "Address2");
                    robj.Name = DBHelper.getString(sqlobj, "Name");
                    robj.Phone = DBHelper.getString(sqlobj, "Phone");
                    robj.City = DBHelper.getString(sqlobj, "City");
                    robj.State = DBHelper.getString(sqlobj, "State");
                    robj.ZipCode = DBHelper.getString(sqlobj, "ZipCode");
                }
            }
            return robj;
        }

        [OperationContract]
        public long AddMomOB(long assessmentId, string Name, string Address1, string Address2, string Phone, string City, string State, string ZipCode)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",assessmentId),
                new SqlParameter("Name",Name),
                new SqlParameter("Address1",Address1),
                new SqlParameter("Address2",Address2),
                new SqlParameter("Phone",Phone),
                new SqlParameter("City",City),
                new SqlParameter("State",State),
                new SqlParameter("ZipCode",ZipCode)
            };

            object id = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_AddMomOB", sqlParms.ToArray());
            return Convert.ToInt64(id);
        }

        [OperationContract]
        public object EditMomOB(long momOBId, string Name, string Address1, string Address2, string Phone, string City, string State, string ZipCode)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("Id",momOBId),
                new SqlParameter("Name",Name),
                new SqlParameter("Address1",Address1),
                new SqlParameter("Address2",Address2),
                new SqlParameter("Phone",Phone),
                new SqlParameter("City",City),
                new SqlParameter("State",State),
                new SqlParameter("ZipCode",ZipCode)
            };

            return SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_EditMomOB", sqlParms.ToArray());
        }

        [OperationContract]
        public BabyPCPhy GetBabyPCP(long assessmentId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",assessmentId)
            };
            var robj = new BabyPCPhy();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetBabyPCP", sqlParms))
            {
                while (sqlobj.Read())
                {

                    robj.Id = DBHelper.getInt64(sqlobj, "Id");
                    robj.AssessmentId = DBHelper.getInt64(sqlobj, "AssessmentId");
                    robj.Address1 = DBHelper.getString(sqlobj, "Address1");
                    robj.Address2 = DBHelper.getString(sqlobj, "Address2");
                    robj.Name = DBHelper.getString(sqlobj, "Name");
                    robj.Phone = DBHelper.getString(sqlobj, "Phone");
                    robj.City = DBHelper.getString(sqlobj, "City");
                    robj.State = DBHelper.getString(sqlobj, "State");
                    robj.ZipCode = DBHelper.getString(sqlobj, "ZipCode");

                }
            }
            return robj;
        }

        [OperationContract]
        public void AddBabyPCP(long assessmentId, string Name, string Address1, string Address2, string Phone, string City, string State, string ZipCode)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",assessmentId),
                new SqlParameter("Name",Name),
                new SqlParameter("Address1",Address1),
                new SqlParameter("Address2",Address2),
                new SqlParameter("Phone",Phone),
                new SqlParameter("City",City),
                new SqlParameter("State",State),
                new SqlParameter("ZipCode",ZipCode)
            };

            SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_AddBabyPCP", sqlParms.ToArray());
        }

        [OperationContract]
        public void EditBabyPCP(long babyPCPId, string Name, string Address1, string Address2, string Phone, string City, string State, string ZipCode)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("Id",babyPCPId),
                new SqlParameter("Name",Name),
                new SqlParameter("Address1",Address1),
                new SqlParameter("Address2",Address2),
                new SqlParameter("Phone",Phone),
                new SqlParameter("City",City),
                new SqlParameter("State",State),
                new SqlParameter("ZipCode",ZipCode)
            };

            SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_EditBabyPCP", sqlParms.ToArray());
        }

        [OperationContract]
        public cWrapper GetConditions(long assessmentId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",assessmentId)
            };

            var list = new List<Condition>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetAllConditions", sqlParms))
            {
                while (sqlobj.Read())
                {
                    Condition robj = GetConditionData(sqlobj);
                    list.Add(robj);
                }
            }

            return new cWrapper
            {
                Rows = list
            };
        }

        private static Condition GetConditionData(SqlDataReader sqlobj)
        {
            var robj = new Condition();
            robj.Id = DBHelper.getInt64(sqlobj, "Id");
            robj.Description = DBHelper.getString(sqlobj, "Description");
            robj.AssessmentId = DBHelper.getInt64(sqlobj, "AssessmentId");
            robj.ICDCode = DBHelper.getString(sqlobj, "ICDCode");
            var aDate = DBHelper.getDateTime(sqlobj, "ActiveDate");
            robj.ActiveDate = aDate;
            robj.ActiveDateString = aDate == SqlDateTime.MinValue ? string.Empty : aDate.ToString(Utility.GlobalDateFormat);
            robj.InActiveDate = DBHelper.getDateTime(sqlobj, "InActiveDate");
            return robj;
        }

        [OperationContract]
        public Condition GetCondition(long conditionId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("ConditionId",conditionId)
            };

            var robj = new Condition();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetCondition", sqlParms))
            {
                while (sqlobj.Read())
                {
                    robj = GetConditionData(sqlobj);
                }
            }

            return robj;
        }

        [OperationContract]
        public Condition AddCondition(string Id, string ICDCode, string ActiveDate, string IsActive, string InActiveDate)
        {
            SqlParameter[] sqlParms = CreateConditionParams(Id, ICDCode, ActiveDate, IsActive, InActiveDate);

            var robj = new Condition();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_AddCondition", sqlParms.ToArray()))
            {
                while (sqlobj.Read())
                {
                    robj = GetConditionData(sqlobj);
                }
            }
            return robj;

        }

        private static SqlParameter[] CreateConditionParams(string Id, string ICDCode, string ActiveDate, string IsActive, string InActiveDate)
        {
            return new SqlParameter[]{
                new SqlParameter("Id",Convert.ToInt64(Id)),
                new SqlParameter("CodeId",ICDCode),
                new SqlParameter("ActiveDate",string.IsNullOrWhiteSpace( ActiveDate) ? SqlDateTime.MinValue : Convert.ToDateTime(ActiveDate) ),
                new SqlParameter("IsActive",Convert.ToBoolean(IsActive)),
                new SqlParameter("InActiveDate",string.IsNullOrWhiteSpace(InActiveDate) ? SqlDateTime.MinValue : Convert.ToDateTime(InActiveDate) )
            };
        }

        [OperationContract]
        public Condition EditCondition(string Id, string ICDCode, string ActiveDate, string IsActive, string InActiveDate)
        {
            SqlParameter[] sqlParms = CreateConditionParams(Id, ICDCode, ActiveDate, IsActive, InActiveDate);
            var robj = new Condition();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_EditCondition", sqlParms.ToArray()))
            {
                while (sqlobj.Read())
                {
                    robj = GetConditionData(sqlobj);
                }
            }
            return robj;
        }

        [OperationContract]
        public void DeleteCondition(string Id)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("Id",Convert.ToInt64(Id))
            };

            SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_DeleteCondition", sqlParms.ToArray());
        }

        [OperationContract]
        public mWrapper GetMedications(long assessmentId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",assessmentId)
            };

            var list = new List<Medication>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetAllMedications", sqlParms))
            {
                while (sqlobj.Read())
                {
                    Medication robj = GetMedicationData(sqlobj);
                    list.Add(robj);
                }
            }

            return new mWrapper
            {
                Rows = list
            };
        }

        [OperationContract]
        public Medication GetMedication(long medicationId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("MedicationId",medicationId)
            };

            var robj = new Medication();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetMedication", sqlParms))
            {
                while (sqlobj.Read())
                {
                    robj = GetMedicationData(sqlobj);
                }
            }
            return robj;
        }

        [OperationContract]
        public Medication AddMedication(string id, string medication, string isActive, string dosage, string type, string startDate, string stopDate)
        {
            List<SqlParameter> sqlParms = new List<SqlParameter>();

            CreateMedicationParams(id, medication, isActive, dosage, type, startDate, stopDate, sqlParms);

            //SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_AddMedication", sqlParms.ToArray());
            var robj = new Medication();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_AddMedication", sqlParms.ToArray()))
            {
                while (sqlobj.Read())
                {
                    robj = GetMedicationData(sqlobj);
                }
            }
            return robj;
        }

        private static Medication GetMedicationData(SqlDataReader sqlobj)
        {
            var robj = new Medication();
            robj.Id = DBHelper.getInt64(sqlobj, "Id");
            robj.AssessmentId = DBHelper.getInt64(sqlobj, "AssessmentId");
            robj.Medicine = DBHelper.getString(sqlobj, "Medication");
            robj.IsActive = DBHelper.getBool(sqlobj, "IsActive");
            robj.Dosage = DBHelper.getString(sqlobj, "Dosage");
            robj.Type = DBHelper.getString(sqlobj, "Type");
            var sDate = DBHelper.getDateTime(sqlobj, "StartDate");
            robj.StartDate = sDate;
            robj.StopDate = DBHelper.getDateTime(sqlobj, "StopDate");
            robj.StartDateString = sDate == SqlDateTime.MinValue ? string.Empty : sDate.ToString(Utility.GlobalDateFormat);
            return robj;
        }

        private static void CreateMedicationParams(string id, string medication, string isActive, string dosage, string type, string startDate, string stopDate, List<SqlParameter> sqlParms)
        {
            sqlParms.Add(new SqlParameter("Id", Convert.ToInt64(id)));
            sqlParms.Add(new SqlParameter("Medication", medication));
            sqlParms.Add(new SqlParameter("Dosage", dosage));
            sqlParms.Add(new SqlParameter("IsActive", Convert.ToBoolean(isActive)));
            sqlParms.Add(new SqlParameter("Type", type));
            sqlParms.Add(new SqlParameter("StartDate", string.IsNullOrWhiteSpace(startDate) ? SqlDateTime.MinValue : Convert.ToDateTime(startDate)));
            sqlParms.Add(new SqlParameter("StopDate", string.IsNullOrWhiteSpace(stopDate) ? SqlDateTime.MinValue : Convert.ToDateTime(stopDate)));
        }

        [OperationContract]
        public Medication EditMedication(string id, string medication, string isActive, string dosage, string type, string startDate, string stopDate)
        {
            List<SqlParameter> sqlParms = new List<SqlParameter>();

            CreateMedicationParams(id, medication, isActive, dosage, type, startDate, stopDate, sqlParms);

            var robj = new Medication();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_EditMedication", sqlParms.ToArray()))
            {
                while (sqlobj.Read())
                {
                    robj = GetMedicationData(sqlobj);
                }
            }
            return robj;
        }

        [OperationContract]
        public void DeleteMedication(string Id)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("Id",Convert.ToInt64(Id))
            };

            SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_DeleteMedication", sqlParms.ToArray());
        }

        [OperationContract]
        public List<FeedingMaster> GetFeedingMaster()
        {
            SqlParameter[] sqlParms = new SqlParameter[] { };

            var list = new List<FeedingMaster>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetFeedingMaster", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var robj = new FeedingMaster();
                    robj.Id = DBHelper.getInt64(sqlobj, "Id");
                    robj.Description = DBHelper.getString(sqlobj, "Description");
                    list.Add(robj);
                }
            }
            return list;
        }

        [OperationContract]
        public List<MasterICD9Codes> GetMasterICD9CodesAutoComplete(string pre)
        {

            SqlParameter[] sqlParms = new SqlParameter[] {
                 new SqlParameter("pre",pre),
            };

            var list = new List<MasterICD9Codes>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetMasterICD9CodesAutoComplete", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var robj = new MasterICD9Codes();
                    robj.Id = DBHelper.getInt64(sqlobj, "Id");
                    robj.Description = DBHelper.getString(sqlobj, "Description");
                    robj.CodeId = DBHelper.getString(sqlobj, "CodeId");
                    robj.Status = DBHelper.getString(sqlobj, "Status");
                    list.Add(robj);
                }
            }

            return list;
        }

        [OperationContract]
        public List<StatesMaster> GeStatesMaster()
        {
            SqlParameter[] sqlParms = new SqlParameter[] { };

            var list = new List<StatesMaster>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetStatesMaster", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var robj = new StatesMaster();
                    robj.StateId = DBHelper.getInt(sqlobj, "StateId");
                    robj.StateRepresentation = DBHelper.getString(sqlobj, "StateRepresentation");
                    robj.State = DBHelper.getString(sqlobj, "State");
                    list.Add(robj);
                }
            }
            return list;
        }

        [OperationContract]
        public List<FeedingChild> GetFeedingBabies(long assessmentId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",assessmentId)
            };

            var list = new List<FeedingChild>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetBreastFeedingBabyList", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var robj = new FeedingChild();
                    robj.Id = DBHelper.getInt64(sqlobj, "Id");
                    robj.Description = DBHelper.getString(sqlobj, "Description");
                    robj.Status = DBHelper.getInt16(sqlobj, "StsVal");
                    robj.StatusStr = DBHelper.getString(sqlobj, "Status");
                    robj.FeedingId = DBHelper.getInt64(sqlobj, "FeedingId");
                    list.Add(robj);
                }
            }
            return list;
        }

        //[OperationContract]
        public void EditFeedingBabies(string data)
        {
            JArray bArr = JArray.Parse(data);

            foreach (var baby in bArr)
            {
                SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("FeedingId",baby.Value<long>("FeedingId")),
                new SqlParameter("Status",baby.Value<int>("Status")),
                new SqlParameter("AssessmentId",baby.Value<long>("AssessmentId"))
            };

                SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_EditFeedingBaby", sqlParms);
            }
        }

        private bool FeedingExists(long AssessmentId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",AssessmentId)
            };
            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_ExistsFeeding", sqlParms);
            if ((int)obj > 0)
            {
                return true;
            }
            return false;
        }

        private bool FeedingExistsMother(long AssessmentId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",AssessmentId)
            };
            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_ExistsMotherFeeding", sqlParms);
            if ((int)obj > 0)
            {
                return true;
            }
            return false;
        }

        [OperationContract]
        public void AddFeedingObservation(long AssessmentId, bool IsObserved, string data)
        {
            long? oId = FeedingObservationMainExists(AssessmentId);
            long? observationId = null;
            if (!oId.HasValue)
            {
                observationId = AddFeedingParentObservation(AssessmentId, IsObserved);
            }
            else
            {
                observationId = oId;
                EditFeedingParentObservation(oId.Value, IsObserved);
            }

            if (IsObserved)
            {
                AddEditObservationChild(IsObserved, data, observationId);
            }
        }

        private void AddEditObservationChild(bool isObserved, string data, long? observationId)
        {
            JArray bArr = JArray.Parse(data);
            if (FeedingExistsChildObservation(observationId.Value))
            {
                EditFeedingChildObservation(data, observationId.Value);
            }
            else
            {
                AddFeedingChildObservation(observationId.Value, data);
            }
        }

        private bool FeedingExistsChildObservation(long ObservationParent)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("ObservationParent",ObservationParent)
            };
            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_ExistsChildObservation", sqlParms);
            if ((int)obj > 0)
            {
                return true;
            }
            return false;
        }

        private void EditFeedingChildObservation(string data, long ObservationParent)
        {
            JArray oArr = JArray.Parse(data);
            foreach (var baby in oArr)
            {
                SqlParameter[] sqlParms = new SqlParameter[]{
                         new SqlParameter("ObservationId",baby.Value<int>("ObservationId")),
                        new SqlParameter("Status",baby.Value<int>("Status")),
                         new SqlParameter("Notes",baby.Value<string>("Notes")),
                         new SqlParameter("ObservationParent",ObservationParent)
                    };

                SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_EditObservationsChild", sqlParms);
            }
        }

        private void AddFeedingChildObservation(long ObservationParent, string data)
        {
            JArray oArr = JArray.Parse(data);
            foreach (var baby in oArr)
            {
                SqlParameter[] sqlParms = new SqlParameter[]{
                         new SqlParameter("ObservationId",baby.Value<long>("ObservationId")),
                        new SqlParameter("Status",baby.Value<int>("Status")),
                         new SqlParameter("Notes",baby.Value<string>("Notes")),
                         new SqlParameter("ObservationParent",ObservationParent)
                    };

                SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_AddObservationsChild", sqlParms);
            }
        }

        private long AddFeedingParentObservation(long AssessmentId, bool IsObserved)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                         new SqlParameter("AssessmentId",AssessmentId),
                         new SqlParameter("IsObserved",IsObserved)
                    };

            var oid = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_AddObservationFeedingParent", sqlParms);
            return (long)oid;
        }

        private void EditFeedingParentObservation(long ObservationId, bool IsObserved)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                         new SqlParameter("ObservationId",ObservationId),
                         new SqlParameter("IsObserved",IsObserved)
                    };

            SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_EditObservationsParent", sqlParms);
        }


        private long? FeedingObservationMainExists(long assessmentId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                         new SqlParameter("AssessmentId",assessmentId)
                    };

            var oid = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_ExistsObservationFeeding", sqlParms);
            if (oid != null)
            {
                return (long)oid;
            }
            else
            {
                return null;
            }
        }

        [OperationContract]
        public void AddFeedingBabies(string data)
        {
            JArray bArr = JArray.Parse(data);
            if (FeedingExists(bArr[0].Value<long>("AssessmentId")))
            {
                EditFeedingBabies(data);
            }
            else
            {
                foreach (var baby in bArr)
                {
                    SqlParameter[] sqlParms = new SqlParameter[]{
                         new SqlParameter("AssessmentId",baby.Value<long>("AssessmentId")),
                        new SqlParameter("FeedingId",baby.Value<long>("FeedingId")),
                         new SqlParameter("Status",baby.Value<int>("Status"))
                    };

                    SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_AddFeedingBaby", sqlParms);
                }
            }
        }

        [OperationContract]
        public void AddFeedingMothers(string data)
        {
            JArray bArr = JArray.Parse(data);
            if (FeedingExistsMother(bArr[0].Value<long>("AssessmentId")))
            {
                EditFeedingMothers(data);
            }
            else
            {
                foreach (var baby in bArr)
                {
                    SqlParameter[] sqlParms = new SqlParameter[]{
                         new SqlParameter("AssessmentId",baby.Value<long>("AssessmentId")),
                        new SqlParameter("FeedingId",baby.Value<long>("FeedingId")),
                         new SqlParameter("Status",baby.Value<int>("Status")),
                         new SqlParameter("Notes",baby.Value<string>("Notes"))
                    };

                    SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_AddFeedingMothers", sqlParms);
                }
            }
        }
        public void EditFeedingMothers(string data)
        {
            JArray bArr = JArray.Parse(data);

            foreach (var baby in bArr)
            {
                SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",baby.Value<long>("AssessmentId")),
                new SqlParameter("FeedingId",baby.Value<long>("FeedingId")),
                new SqlParameter("Status",baby.Value<int>("Status")),
                new SqlParameter("Notes",baby.Value<string>("Notes"))
            };

                SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_EditFeedingMother", sqlParms);
            }
        }

       
        private DataTable GetDataTable(string json)
        {
            var result = new DataTable();
            var jArray = JArray.Parse(json);
            //Initialize the columns, If you know the row type, replace this   
            foreach (var row in jArray)
            {
                foreach (var jToken in row)
                {
                    var jproperty = jToken as JProperty;
                    if (jproperty == null) continue;
                    if (result.Columns[jproperty.Name] == null)
                        result.Columns.Add(jproperty.Name, typeof(string));
                }
            }
            foreach (var row in jArray)
            {
                var datarow = result.NewRow();
                foreach (var jToken in row)
                {
                    var jProperty = jToken as JProperty;
                    if (jProperty == null) continue;
                    datarow[jProperty.Name] = jProperty.Value.ToString();
                }
                result.Rows.Add(datarow);
            }

            return result;
        }

        [OperationContract]
        public List<FeedingMother> GetFeedingMothers(long assessmentId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",assessmentId)
            };

            var list = new List<FeedingMother>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetBreastFeedingMotherList", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var robj = new FeedingMother();
                    robj.Id = DBHelper.getInt64(sqlobj, "Id");
                    robj.Description = DBHelper.getString(sqlobj, "Description");
                    robj.Notes = DBHelper.getString(sqlobj, "Notes");
                    robj.Status = DBHelper.getInt16(sqlobj, "StsVal");
                    robj.StatusStr = DBHelper.getString(sqlobj, "Status");
                    robj.FeedingId = DBHelper.getInt64(sqlobj, "FeedingId");
                    list.Add(robj);
                }
            }
            return list;
        }

        [OperationContract]
        public FeedingObservations GetFeedingObservations(long assessmentId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",assessmentId)
            };

            //var list = new List<FeedingMother>();
            var robj = new FeedingObservations();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetBreastFeedingObservations", sqlParms))
            {
                while (sqlobj.Read())
                {
                    robj.Id = DBHelper.getInt64(sqlobj, "Id");
                    robj.IsObserved = DBHelper.getBool(sqlobj, "IsObserved");

                }
                if (sqlobj.NextResult())
                {
                    while (sqlobj.Read())
                    {
                        var obcld = new FeedingOvbservationChild();
                        obcld.Id = DBHelper.getInt64(sqlobj, "Id");
                        obcld.FeedingId = DBHelper.getInt64(sqlobj, "FeedingId");
                        obcld.Status = DBHelper.getInt16(sqlobj, "StsVal");
                        obcld.StatusStr = DBHelper.getString(sqlobj, "Status");
                        obcld.Notes = DBHelper.getString(sqlobj, "Notes");
                        obcld.Description = DBHelper.getString(sqlobj, "Description");
                        obcld.ObservationParentId = DBHelper.getInt64(sqlobj, "ObservationParentId");
                        robj.ChildObservations.Add(obcld);
                    }
                }
            }
            return robj;
        }

        [OperationContract]
        public List<Complaints> GetChiefComplaints(long assessmentId, bool IsChild)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",assessmentId),
                new SqlParameter("IsChild",IsChild)
            };
            //Encounters_GetChiefComplaintMothers
            var list = new List<Complaints>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetChiefComplaints", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var robj = new Complaints();
                    robj.Id = DBHelper.getInt64(sqlobj, "Id");
                    robj.ComplaintId = DBHelper.getInt64(sqlobj, "ComplaintId");
                    robj.Description = DBHelper.getString(sqlobj, "Description");
                    robj.DiagnosisCode = DBHelper.getString(sqlobj, "DiagnosisCode");
                    robj.IsChild = DBHelper.getBool(sqlobj, "IsChild");

                    list.Add(robj);
                }
            }
            return list;
        }

        [OperationContract]
        public Assessment GetAssessment(long assessmentId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",assessmentId)
            };

            var robj = new Assessment();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetAssessment", sqlParms))
            {
                while (sqlobj.Read())
                {

                    robj.Id = DBHelper.getInt64(sqlobj, "Id");
                    robj.AssessmentId = DBHelper.getInt64(sqlobj, "AssessmentId");
                    robj.Date = DBHelper.getDateTime(sqlobj, "Date");
                    robj.Time = DBHelper.getString(sqlobj, "Time");
                    robj.Notes = DBHelper.getString(sqlobj, "Notes");
                }
            }
            return robj;
        }

        [OperationContract]
        public PresentIllness GetPresentIllness(long assessmentId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",assessmentId)
            };

            var robj = new PresentIllness();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetPresentIllness", sqlParms))
            {
                while (sqlobj.Read())
                {

                    robj.Id = DBHelper.getInt64(sqlobj, "Id");
                    robj.AssessmentId = DBHelper.getInt64(sqlobj, "AssessmentId");
                    robj.Date = DBHelper.getDateTime(sqlobj, "Date");
                    robj.Time = DBHelper.getString(sqlobj, "Time");
                    robj.Notes = DBHelper.getString(sqlobj, "Notes");
                }
            }
            return robj;
        }

        [OperationContract]
        public CoordinationOfCare GetCoordicationOfCare(long assessmentId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",assessmentId)
            };

            var robj = new CoordinationOfCare();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetCoordicationOfCare", sqlParms))
            {
                while (sqlobj.Read())
                {

                    robj.Id = DBHelper.getInt64(sqlobj, "Id");
                    robj.AssessmentId = DBHelper.getInt64(sqlobj, "AssessmentId");
                    robj.Date = DBHelper.getDateTime(sqlobj, "Date");
                    robj.Time = DBHelper.getString(sqlobj, "Time");
                    robj.Notes = DBHelper.getString(sqlobj, "Notes");
                }
            }
            return robj;
        }

        [OperationContract]
        public Plan GetPlan(long assessmentId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",assessmentId)
            };

            var robj = new Plan();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetPlan", sqlParms))
            {
                while (sqlobj.Read())
                {
                    robj.Id = DBHelper.getInt64(sqlobj, "Id");
                    robj.AssessmentId = DBHelper.getInt64(sqlobj, "AssessmentId");
                    robj.Date = DBHelper.getDateTime(sqlobj, "Date");
                    robj.Time = DBHelper.getString(sqlobj, "Time");
                    robj.Notes = DBHelper.getString(sqlobj, "Notes");
                }
            }
            return robj;
        }

        private static SqlParameter[] CommonProperties(string Id, string Date, string Time, string Notes, bool IsAdd)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter(IsAdd ? "AssessmentId" : "Id", Convert.ToInt64(Id)));
            param.Add(new SqlParameter("Date", Convert.ToDateTime(Date)));
            param.Add(new SqlParameter("Time", Time));
            param.Add(new SqlParameter("Notes", Notes));
            return param.ToArray();
        }

        [OperationContract]
        public long AddPlan(string Id, string Date, string Time, string Notes)
        {
            SqlParameter[] sqlParms = CommonProperties(Id, Date, Time, Notes, true);

            return (long)SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_AddPlan", sqlParms.ToArray());
        }

        [OperationContract]
        public void EditPlan(string Id, string Date, string Time, string Notes)
        {
            SqlParameter[] sqlParms = CommonProperties(Id, Date, Time, Notes, false);

            SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_EditPlan", sqlParms.ToArray());
        }

        [OperationContract]
        public long AddAssessment(string Id, string Date, string Time, string Notes)
        {
            SqlParameter[] sqlParms = CommonProperties(Id, Date, Time, Notes, true);

            return (long)SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_AddAssessment", sqlParms.ToArray());
        }

        [OperationContract]
        public void EditAssessment(string Id, string Date, string Time, string Notes)
        {
            SqlParameter[] sqlParms = CommonProperties(Id, Date, Time, Notes, false);

            SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_EditAssessment", sqlParms.ToArray());
        }

        [OperationContract]
        public long AddCoordinationOfCare(string Id, string Date, string Time, string Notes)
        {
            SqlParameter[] sqlParms = CommonProperties(Id, Date, Time, Notes, true);

            return (long)SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_AddCoordinationOfCare", sqlParms.ToArray());
        }

        [OperationContract]
        public void EditCoordinationOfCare(string Id, string Date, string Time, string Notes)
        {
            SqlParameter[] sqlParms = CommonProperties(Id, Date, Time, Notes, false);

            SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_EditCoordinationOfCare", sqlParms.ToArray());
        }

        [OperationContract]
        public long AddHPI(string Id, string Date, string Time, string Notes)
        {
            SqlParameter[] sqlParms = CommonProperties(Id, Date, Time, Notes, true);

            return (long)SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_AddHPI", sqlParms.ToArray());
        }

        [OperationContract]
        public void EditHPI(string Id, string Date, string Time, string Notes)
        {
            SqlParameter[] sqlParms = CommonProperties(Id, Date, Time, Notes, false);

            SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_EditHPI", sqlParms.ToArray());
        }

        [OperationContract]
        public void AddChiefComplaints(string AssessmentId, string data)
        {
            PrepareInsertComplaints(AssessmentId);

            JArray bArr = JArray.Parse(data);
            foreach (var baby in bArr)
            {
                SqlParameter[] sqlParms = new SqlParameter[]{
                         new SqlParameter("AssessmentId",AssessmentId),
                        new SqlParameter("ComplaintId",baby.Value<long>("ComplaintId"))
                    };

                SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_AddChiefComplaints", sqlParms);
            }
        }

        private void PrepareInsertComplaints(string assessmentId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                         new SqlParameter("AssessmentId",assessmentId)
                    };

            SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_PrepareChiefComplaints", sqlParms);
        }

        [OperationContract]
        public EncounterAssessmentSummary GetCompleteAssessmentSummary(long AssessmentId, long UserId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("AssessmentId",AssessmentId),
                new SqlParameter("UserId",UserId)
            };

            var ea = new EncounterAssessmentSummary();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetCompleteSummary", sqlParms))
            {
                while (sqlobj.Read())
                {
                    ea.PatientName = DBHelper.getString(sqlobj, "PeerName");
                    ea.DOB = Convert.ToDateTime(ecd.DecryptData(DBHelper.getString(sqlobj, "DOB"), ecd.GetEncryptType()));
                    ea.Age = DBHelper.getInt(sqlobj, "Age");
                    ea.AssessmentDate = DBHelper.getDateTime(sqlobj, "AssessmentDate");
                    ea.AssessmentBy = DBHelper.getString(sqlobj, "AssessmentBy");
                }
                if (sqlobj.NextResult())
                {
                    while (sqlobj.Read())
                    {
                        ea.AssessmentDate = DBHelper.getDateTime(sqlobj, "AssessmentDate");
                        ea.AssessmentBy = DBHelper.getString(sqlobj, "AssessmentBy");
                        ea.AssessmentNotes = DBHelper.getString(sqlobj, "aNotes");
                        ea.HPINotes = DBHelper.getString(sqlobj, "piNotes");
                        ea.COCNotes = DBHelper.getString(sqlobj, "corNotes");
                    }
                }
                if (sqlobj.NextResult())
                {
                    //public List<FeedingMother> BreastFeedingBabies { get; set; }
                    while (sqlobj.Read())
                    {
                        var fb = new FeedingMother();
                        fb.Description = DBHelper.getString(sqlobj, "Description");
                        fb.StatusStr = DBHelper.getString(sqlobj, "Status");
                        ea.BreastFeedingBabies.Add(fb);
                    }

                }
                if (sqlobj.NextResult())
                {
                    //public List<FeedingMother> BreastFeedingMothers { get; set; }
                    while (sqlobj.Read())
                    {
                        var fm = new FeedingMother();
                        fm.Description = DBHelper.getString(sqlobj, "Description");
                        fm.StatusStr = DBHelper.getString(sqlobj, "Status");
                        ea.BreastFeedingMothers.Add(fm);
                    }

                }

                if (sqlobj.NextResult())
                {
                    //public List<FeedingOvbservationChild> Observations { get; set; }
                    while (sqlobj.Read())
                    {
                        var oc = new FeedingOvbservationChild();
                        oc.Description = DBHelper.getString(sqlobj, "Description");
                        oc.StatusStr = DBHelper.getString(sqlobj, "Status");
                        oc.Notes = DBHelper.getString(sqlobj, "Notes");
                        ea.Observations.Add(oc);
                    }
                }

                if (sqlobj.NextResult())
                {
                    //public List<ChiefComplaints> ChiefComplaintList { get; set; }
                    while (sqlobj.Read())
                    {
                        var cc = new EncounterAssessmentSummary.ChiefComplaints();
                        cc.Notes = DBHelper.getString(sqlobj, "Description");
                        ea.ChiefComplaintList.Add(cc);

                        var ccid = DBHelper.getInt64(sqlobj, "Id");
                        sqlParms = new SqlParameter[]{
                            new SqlParameter("ComplaintId", ccid)
                         };

                        using (SqlDataReader pObj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "Encounters_GetChiefComplaintPlans", sqlParms))
                        {
                            while (pObj.Read())
                            {
                                var cp = new EncounterAssessmentSummary.ChiefComplaints.ComplaintsPlan();
                                cp.Plan = DBHelper.getString(pObj, "Description");

                                cc.ComplaintsPlans.Add(cp);
                            }
                        }


                    }
                }
            }
            return ea;
        }
    }
}