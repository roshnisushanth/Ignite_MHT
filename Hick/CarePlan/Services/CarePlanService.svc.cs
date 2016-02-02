using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Newtonsoft;

namespace Hick.CarePlan.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CarePlanService
    {
        public void AddCarePlan(long userId, long screenId, string data)
        {
            try
            {

                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {

                    conn.ConnectionString = constr;
                    conn.Open();
                    StringBuilder sbu = new StringBuilder();

                    using (SqlCommand command = new SqlCommand("CarePlan_AddCarePlan", conn))
                    {
                        command.Parameters.Add("@UserId", SqlDbType.BigInt).Value = userId;
                        command.Parameters.Add("@ScreenId", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                        command.Parameters.Add("@Data", SqlDbType.VarChar).Value = data;
                        SqlDataReader reader = command.ExecuteReader();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void EditCarePlan(int id, string data)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {

                    conn.ConnectionString = constr;
                    conn.Open();
                    StringBuilder sbu = new StringBuilder();

                    using (SqlCommand command = new SqlCommand("CarePlan_UpdateCarePlan", conn))
                    {
                        command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
                        command.Parameters.Add("@Data", SqlDbType.VarChar).Value = data;
                        SqlDataReader reader = command.ExecuteReader();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public string GetCarePlansForUser(long userId)
        {
            string jsondata = "";
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {

                    conn.ConnectionString = constr;
                    conn.Open();
                    StringBuilder sbu = new StringBuilder();

                    using (SqlCommand command = new SqlCommand("CarePlan_GetPlansForUser", conn))
                    {
                        command.Parameters.Add("@UserId", SqlDbType.BigInt).Value = userId;
                        SqlDataReader reader = command.ExecuteReader();
                        jsondata= WriteReaderToJSON(reader);
                    }
                }
            }
            catch (Exception)
            {
            }
            return jsondata;
        }


        private string WriteReaderToJSON(IDataReader reader)
        {
            StringBuilder sb = new StringBuilder();
            if (reader == null || reader.FieldCount == 0)
            {
                sb.Append("null");
                return "";
            }

            int rowCount = 0;

            sb.Append(@"{""Rows"":[");

            while (reader.Read())
            {
                sb.Append("{");

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    sb.Append("\"" + reader.GetName(i) + "\":");
                    sb.Append("\"" + reader[i] + "\"");

                    sb.Append(i == reader.FieldCount-1 ? "" : ",");

                }

                sb.Append("},");

                rowCount++;
            }

            if (rowCount > 0)
            {
                int index = sb.ToString().LastIndexOf(",");
                sb.Remove(index, 1);
            }

            sb.Append("]}");

            return sb.ToString();

        }
    }
}
