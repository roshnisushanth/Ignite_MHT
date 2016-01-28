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
using System.Data;

namespace Hick
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HickUser" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HickUser.svc or HickUser.svc.cs at the Solution Explorer and start debugging.
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class HickUser
    {
        [OperationContract]
        public string TestService()
        {
            return "Hello User";
        }

        [OperationContract]
        public void AddUser(long userid, string username, string firstname, string lastname, string password, string image, string phoneNumber, string dateofbirth, string usertype)
        {
            try
            {

                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();
                    bool isuserexist = false;
                    //var selcmd = "SELECT * FROM Hick_Users WHERE ID=" + userid + "";

                    using (SqlCommand selcommand = new SqlCommand("sp_hick_FetchUserByID", conn))
                    {
                        selcommand.CommandType = CommandType.StoredProcedure;
                        selcommand.Parameters.AddWithValue("@UserId",userid);
                        using (SqlDataReader reader = selcommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                isuserexist = true;
                            }
                        }
                    }
                    if (!isuserexist)
                    {
                        //var cmd = "INSERT INTO Hick_Users (ID,Username,Firstname,Lastname,Status,Password,Image,phone_number,dateofbirth,user_type) VALUES(@id,@username,@firstname,@lastname,@status,@password,@image,@phonenumber,@dob,@usertype)";
                        using (SqlCommand command = new SqlCommand("sp_hick_InsertHickUser", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@Id", userid);
                            command.Parameters.AddWithValue("@UserName", username);
                            command.Parameters.AddWithValue("@FirstName", firstname);
                            command.Parameters.AddWithValue("@LastName", lastname);
                            command.Parameters.AddWithValue("@Status", 0);
                            //command.Parameters.AddWithValue("@statusmessage", statusmessage);
                            command.Parameters.AddWithValue("@Password", password);
                            command.Parameters.AddWithValue("@image", image);
                            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                            command.Parameters.AddWithValue("@DOB", dateofbirth);
                            command.Parameters.AddWithValue("@UserType", usertype);
                            command.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        [OperationContract]
        public void EditUser(long userid, string username, string firstname, string lastname, string password, string image, string phoneNumber, string dateofbirth)
        {
            try
            {

                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();

                    //var cmd = "UPDATE Hick_Users SET Username=@username,Firstname=@firstname,Lastname=@lastname,Status=@status,Password=@password,Image=@image,phone_number=@phonenumber,dateofbirth=@dob WHERE ID=@id";
                    using (SqlCommand command = new SqlCommand("sp_hick_UpdateHickUser", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Id", userid);
                        command.Parameters.AddWithValue("@UserName", username);
                        command.Parameters.AddWithValue("@FirstName", firstname);
                        command.Parameters.AddWithValue("@LastName", lastname);
                        command.Parameters.AddWithValue("@Status", 0);
                        //command.Parameters.AddWithValue("@statusmessage", statusmessage);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@image", image);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@DOB", dateofbirth);
                        command.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception)
            {
                throw;
            }
        }

        [OperationContract]
        public string AuthenticateBridgeUser(string username)
        {
            string guid = "0";
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                long userid = 0;
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();
                    bool isuserexist = false;
                    //var selcmd = "SELECT top 1 * FROM Hick_Users WHERE Lower(Username) like '" + username.ToLower() + "'";

                    using (SqlCommand selcommand = new SqlCommand("sp_hick_FetchHickUserLike", conn))
                    {
                        selcommand.CommandType = CommandType.StoredProcedure;
                        selcommand.Parameters.AddWithValue("@UserName", username.ToLower());

                        using (SqlDataReader reader = selcommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    isuserexist = true;
                                    userid = Convert.ToInt32(reader["ID"]);

                                }

                            }
                        }
                    }
                    if (isuserexist)
                    {
                        guid = Utility.EncryptText(userid.ToString()) + "-" + Guid.NewGuid();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return guid;
        }

        [OperationContract]
        public int AddPhysicianMapping(long physicianid, long patientid)
        {
            int result=0;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {
                    bool isuserexist = false;
                    conn.ConnectionString = constr;
                    conn.Open();

                    //var selectcmd = "Select * from hick_physician_mapping where physicianId=" + physicianid + " and patientId=" + patientid + "";
                    using (SqlCommand cmd = new SqlCommand("sp_hick_FetchPhysicianMapping", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PhysicianId",physicianid);
                        cmd.Parameters.AddWithValue("@PatientId",patientid);

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.HasRows)
                            {
                                isuserexist = true;
                            }
                        }
                    }

                    if (isuserexist == false)
                    {
                        //var addcmd = "Insert into hick_physician_mapping(physicianId,patientId)values(@physicianId,@patientId)";
                        using (SqlCommand cmd = new SqlCommand("sp_hick_InsertPhysicianMapping", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            
                            cmd.Parameters.AddWithValue("@PhysicianId", physicianid);
                            cmd.Parameters.AddWithValue("@PatientId", patientid);
                            result = cmd.ExecuteNonQuery();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [OperationContract]
        public int AddPhysicianPractice(long physicianid, long practiceid)
        {
            int result = 0;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {
                    bool isuserexist = false;
                    conn.ConnectionString = constr;
                    conn.Open();

                    //var selectcmd = "Select * from hick_physician_practice where physicianId=" + physicianid + " and practiceId=" + practiceid + "";
                    using (SqlCommand cmd = new SqlCommand("sp_hick_FetchPhysicianPractice", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PhysicianId",physicianid);
                        cmd.Parameters.AddWithValue("@PracticeId",practiceid);

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.HasRows)
                            {
                                isuserexist = true;
                            }
                        }
                    }

                    if (isuserexist == false)
                    {
                        //var addcmd = "Insert into hick_physician_practice(physicianId,practiceId)values(@physicianId,@practiceId)";
                        using (SqlCommand cmd = new SqlCommand("sp_hick_InsertPhysicianPractice", conn))
                        {
                            cmd.Parameters.AddWithValue("@physicianId", physicianid);
                            cmd.Parameters.AddWithValue("@practiceId", practiceid);
                            result = cmd.ExecuteNonQuery();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
