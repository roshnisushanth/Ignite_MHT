using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Collections;
using HPFCONNECT_MODEL;
using HPFCONNECT_BLL;

namespace IgniteServices
{
    public class IgniteService : IIgniteService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public string DeleteMessage(Stream message)
        {
            try
            {

                MessageBox objInput = JSonHelper.JsonDeserialize<MessageBox>(new StreamReader(message).ReadToEnd());
                if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
                {
                    return "Invalid PIN";
                }
                if (objInput.Pin != null && objInput.Pin.Length > 0)
                {
                    MessageBox obj2 = new MessageBox();
                    List<MessageInfo> objcol = new List<MessageInfo>();
                    MessageInfo obj = new MessageInfo();
                    MessageBLL objbll = new MessageBLL();

                    obj.FromId = objInput.FromId;
                    obj.MessageComposeId = objInput.MessageComposeId;
                    obj.userType = objInput.userType;
                    obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();
                    int i = objbll.DeleteMessages(obj);
                    if (i > 0)
                        return "Delete sucessful";
                    else
                        return "Delete Unsucessful";
                }
                else
                {
                    return "Pin is a required field";
                }
            }
            catch (Exception ex)
            {
                return "Unable to delete. Please try again later";
            }
        }


        public string PermanentlyDeleteMessage(Stream message)
        {
            try
            {

                MessageBox objInput = JSonHelper.JsonDeserialize<MessageBox>(new StreamReader(message).ReadToEnd());
                if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
                {
                    return "Invalid PIN";
                }
                if (objInput.Pin != null && objInput.Pin.Length > 0)
                {
                    MessageBox obj2 = new MessageBox();
                    List<MessageInfo> objcol = new List<MessageInfo>();
                    MessageInfo obj = new MessageInfo();
                    MessageBLL objbll = new MessageBLL();

                    obj.MessageComposeId = objInput.MessageComposeId;
                    obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();
                    int i = objbll.DeleteMessages_DeletedItems(obj);
                    if (i > 0)
                        return "Delete sucessful";
                    else
                        return "Delete Unsucessful";
                }
                else
                {
                    return "Pin is a required field";
                }
            }
            catch (Exception ex)
            {
                return "Unable to delete. Please try again later";
            }
        }

        public string GetAppSetting(Stream message)
        {
            try
            {
                return System.Configuration.ConfigurationManager.AppSettings["BASE"].ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public UserDetails[] ValidateUserLogin(Stream message)
        {

            UserDetails objInput = JSonHelper.JsonDeserialize<UserDetails>(new StreamReader(message).ReadToEnd());
            if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
            {
                var objArray = new UserDetails[1];
                UserDetails objDetails = new UserDetails();
                objDetails.UserId = "";
                objDetails.MessengerMessage = "Invalid PIN";
                objArray[0] = objDetails;
                return objArray;

            }
            if (objInput.Pin != null && objInput.Pin.Length <= 0)
            {
                var objArray = new UserDetails[1];
                UserDetails objDetails = new UserDetails();
                objDetails.UserId = "";
                objDetails.MessengerMessage = "Pin is a required field";
                objArray[0] = objDetails;
                return objArray;
            }
            else
            {
                var objArray = new UserDetails[1];
                UserInfo oUserInfo = new UserInfo();
                oUserInfo.EmailId = objInput.UserName;
                oUserInfo.Password = objInput.Password;
                oUserInfo.pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();
                UserBLL oUser = new UserBLL();

                int exists = oUser.ValidateUserLogin(ref oUserInfo);

                if (exists == (int)HPFCONNECT.Constants.UserType.Patient)
                {
                    UserDetails objDetails = new UserDetails();
                    objDetails.UserId = "";
                    objDetails.Application = oUserInfo.pin;
                    objDetails.MessengerMessage = "User found";
                    objArray[0] = objDetails;
                    return objArray;
                }
                else if (exists == (int)HPFCONNECT.Constants.UserType.Physician)
                {
                    PhysicianInfo oPhysicianInfo = new PhysicianInfo();
                    PhysicianBLL oPhysician = new PhysicianBLL();
                    UserInfo oUserInfoID = new UserInfo();
                    UserBLL oUserId = new UserBLL();
                    oPhysicianInfo = oPhysician.getPhysicianDetailsByEmailId(oUserInfo.EmailId, oUserInfo.pin);
                    UserDetails objDetails = new UserDetails();
                    objDetails.UserId = "";
                    objDetails.MessengerMessage = "User found";
                    objDetails.PhysicianId = oPhysicianInfo.PhysicianId;
                    objDetails.IsAdmin = oPhysicianInfo.IsAdmin.ToString();
                    objDetails.IsSuperUser = oPhysicianInfo.IsSuperUser.ToString();
                    objDetails.IsStaff = oPhysicianInfo.IsStaff.ToString();
                    objDetails.UserId = oUserInfo.UserId.ToString();
                    objDetails.Application = oUserInfo.pin;
                    objDetails.UserType = exists;
                    objArray[0] = objDetails;
                    return objArray;
                }
                else if (exists == (int)HPFCONNECT.Constants.UserType.Staff)
                {
                    PhysicianInfo oPhysicianInfo = new PhysicianInfo();
                    PhysicianBLL oPhysician = new PhysicianBLL();
                    UserInfo oUserInfoID = new UserInfo();
                    UserBLL oUserId = new UserBLL();
                    oPhysicianInfo = oPhysician.getPhysicianDetailsByEmailId(oUserInfo.EmailId, oUserInfo.pin);
                    UserDetails objDetails = new UserDetails();
                    objDetails.UserId = "";
                    objDetails.MessengerMessage = "User found";
                    objDetails.PhysicianId = oPhysicianInfo.PhysicianId;
                    objDetails.IsAdmin = oPhysicianInfo.IsAdmin.ToString();
                    objDetails.IsSuperUser = oPhysicianInfo.IsSuperUser.ToString();
                    objDetails.IsStaff = oPhysicianInfo.IsStaff.ToString();
                    objDetails.UserId = oUserInfo.UserId.ToString();
                    objDetails.Application = oUserInfo.pin;
                    objDetails.UserType = exists;
                    objArray[0] = objDetails;
                    return objArray;
                }
                else if (exists == (int)HPFCONNECT.Constants.UserType.Admin)
                {
                    PhysicianInfo oPhysicianInfo = new PhysicianInfo();
                    PhysicianBLL oPhysician = new PhysicianBLL();
                    UserInfo oUserInfoID = new UserInfo();
                    UserBLL oUserId = new UserBLL();
                    oPhysicianInfo = oPhysician.getPhysicianDetailsByEmailId(oUserInfo.EmailId, oUserInfo.pin);
                    UserDetails objDetails = new UserDetails();
                    objDetails.UserId = "";
                    objDetails.MessengerMessage = "User found";
                    objDetails.PhysicianId = oPhysicianInfo.PhysicianId;
                    objDetails.IsAdmin = oPhysicianInfo.IsAdmin.ToString();
                    objDetails.IsSuperUser = oPhysicianInfo.IsSuperUser.ToString();
                    objDetails.IsStaff = oPhysicianInfo.IsStaff.ToString();
                    objDetails.UserId = oUserInfo.UserId.ToString();
                    objDetails.Application = oUserInfo.pin;
                    objDetails.UserType = exists;
                    objArray[0] = objDetails;
                    return objArray;
                }
                else if (exists == (int)HPFCONNECT.Constants.UserType.CaseManager)
                {
                    PhysicianInfo oPhysicianInfo = new PhysicianInfo();
                    PhysicianBLL oPhysician = new PhysicianBLL();
                    UserInfo oUserInfoID = new UserInfo();
                    UserBLL oUserId = new UserBLL();
                    oPhysicianInfo = oPhysician.getPhysicianDetailsByEmailId(oUserInfo.EmailId, oUserInfo.pin);
                    UserDetails objDetails = new UserDetails();
                    objDetails.UserId = "";
                    objDetails.MessengerMessage = "User found";
                    objDetails.PhysicianId = oPhysicianInfo.PhysicianId;
                    objDetails.IsAdmin = oPhysicianInfo.IsAdmin.ToString();
                    objDetails.IsSuperUser = oPhysicianInfo.IsSuperUser.ToString();
                    objDetails.IsStaff = oPhysicianInfo.IsStaff.ToString();
                    objDetails.UserId = oUserInfo.UserId.ToString();
                    objDetails.Application = oUserInfo.pin;
                    objDetails.UserType = exists;
                    objArray[0] = objDetails;
                    return objArray;
                }
                else if (exists == (int)HPFCONNECT.Constants.UserType.CareTransitionCoach)
                {
                    PhysicianInfo oPhysicianInfo = new PhysicianInfo();
                    PhysicianBLL oPhysician = new PhysicianBLL();
                    UserInfo oUserInfoID = new UserInfo();
                    UserBLL oUserId = new UserBLL();
                    oPhysicianInfo = oPhysician.getPhysicianDetailsByEmailId(oUserInfo.EmailId, oUserInfo.pin);
                    UserDetails objDetails = new UserDetails();
                    objDetails.UserId = "";
                    objDetails.MessengerMessage = "User found";
                    objDetails.PhysicianId = oPhysicianInfo.PhysicianId;
                    objDetails.IsAdmin = oPhysicianInfo.IsAdmin.ToString();
                    objDetails.IsSuperUser = oPhysicianInfo.IsSuperUser.ToString();
                    objDetails.IsStaff = oPhysicianInfo.IsStaff.ToString();
                    objDetails.UserId = oUserInfo.UserId.ToString();
                    objDetails.Application = oUserInfo.pin;
                    objDetails.UserType = exists;
                    objArray[0] = objDetails;
                    return objArray;
                }
                else if (exists == (int)HPFCONNECT.Constants.UserType.CareAdmin)
                {
                    CareAdminInfo oCareAdminInfo = new CareAdminInfo();
                    CareAdminBLL oCareAdminBLL = new CareAdminBLL();
                    UserInfo oUserInfoID = new UserInfo();
                    UserBLL oUserId = new UserBLL();
                    oCareAdminInfo = oCareAdminBLL.getCareAdminDetailsByEmailID(oUserInfo.EmailId, oUserInfo.pin);
                    UserDetails objDetails = new UserDetails();
                    objDetails.UserId = "";
                    objDetails.MessengerMessage = "User found";
                    objDetails.PhysicianId = oCareAdminInfo.CareManagerID;
                    objDetails.UserId = oUserInfo.UserId.ToString();
                    objDetails.Application = oUserInfo.pin;
                    objDetails.UserType = exists;
                    objArray[0] = objDetails;
                    return objArray;
                }
                else if (exists == (int)HPFCONNECT.Constants.UserType.CareCoordinator)
                {
                    CareAdminInfo oCareAdminInfo = new CareAdminInfo();
                    CareAdminBLL oCareAdminBLL = new CareAdminBLL();
                    UserInfo oUserInfoID = new UserInfo();
                    UserBLL oUserId = new UserBLL();
                    oCareAdminInfo = oCareAdminBLL.getCareAdminDetailsByEmailID(oUserInfo.EmailId, oUserInfo.pin);
                    UserDetails objDetails = new UserDetails();
                    objDetails.UserId = "";
                    objDetails.MessengerMessage = "User found";
                    objDetails.PhysicianId = oCareAdminInfo.CareManagerID;
                    objDetails.UserId = oUserInfo.UserId.ToString();
                    objDetails.Application = oUserInfo.pin;
                    objDetails.UserType = exists;
                    objArray[0] = objDetails;
                    return objArray;
                }
                else if (exists == (int)HPFCONNECT.Constants.UserType.Hospital)
                {
                    PhysicianInfo oPhysicianInfo = new PhysicianInfo();
                    PhysicianBLL oPhysician = new PhysicianBLL();
                    UserInfo oUserInfoID = new UserInfo();
                    UserBLL oUserId = new UserBLL();
                    oPhysicianInfo = oPhysician.getPhysicianDetailsByEmailId(oUserInfo.EmailId, oUserInfo.pin);
                    UserDetails objDetails = new UserDetails();
                    objDetails.UserId = "";
                    objDetails.MessengerMessage = "User found";
                    objDetails.PhysicianId = oPhysicianInfo.PhysicianId;
                    objDetails.IsAdmin = oPhysicianInfo.IsAdmin.ToString();
                    objDetails.IsSuperUser = oPhysicianInfo.IsSuperUser.ToString();
                    objDetails.IsStaff = oPhysicianInfo.IsStaff.ToString();
                    objDetails.UserId = oUserInfo.UserId.ToString();
                    objDetails.Application = oUserInfo.pin;
                    objDetails.UserType = exists;
                    objArray[0] = objDetails;
                    return objArray;
                }
                else if (exists == (int)HPFCONNECT.Constants.UserType.CareOperator)
                {
                    PhysicianInfo oPhysicianInfo = new PhysicianInfo();
                    PhysicianBLL oPhysician = new PhysicianBLL();
                    UserInfo oUserInfoID = new UserInfo();
                    UserBLL oUserId = new UserBLL();
                    oPhysicianInfo = oPhysician.getPhysicianDetailsByEmailId(oUserInfo.EmailId, oUserInfo.pin);
                    UserDetails objDetails = new UserDetails();
                    objDetails.UserId = "";
                    objDetails.MessengerMessage = "User found";
                    objDetails.PhysicianId = oPhysicianInfo.PhysicianId;
                    objDetails.IsAdmin = oPhysicianInfo.IsAdmin.ToString();
                    objDetails.IsSuperUser = oPhysicianInfo.IsSuperUser.ToString();
                    objDetails.IsStaff = oPhysicianInfo.IsStaff.ToString();
                    objDetails.UserId = oUserInfo.UserId.ToString();
                    objDetails.Application = oUserInfo.pin;
                    objDetails.UserType = exists;
                    objArray[0] = objDetails;
                    return objArray;
                }
                else if (exists == (int)HPFCONNECT.Constants.UserType.Billing)
                {
                    PhysicianInfo oPhysicianInfo = new PhysicianInfo();
                    PhysicianBLL oPhysician = new PhysicianBLL();
                    UserInfo oUserInfoID = new UserInfo();
                    UserBLL oUserId = new UserBLL();
                    oPhysicianInfo = oPhysician.getBillingDetailsByEmailId(oUserInfo.EmailId, oUserInfo.pin);
                    UserDetails objDetails = new UserDetails();
                    objDetails.UserId = "";
                    objDetails.MessengerMessage = "User found";
                    objDetails.PhysicianId = oPhysicianInfo.PhysicianId;
                    objDetails.IsAdmin = oPhysicianInfo.IsAdmin.ToString();
                    objDetails.IsSuperUser = oPhysicianInfo.IsSuperUser.ToString();
                    objDetails.IsStaff = oPhysicianInfo.IsStaff.ToString();
                    objDetails.UserId = oUserInfo.UserId.ToString();
                    objDetails.Application = oUserInfo.pin;
                    objDetails.UserType = exists;
                    objArray[0] = objDetails;
                    return objArray;
                }
                else if (exists == (int)HPFCONNECT.Constants.UserType.SystemAdmin)
                {
                    PhysicianInfo oPhysicianInfo = new PhysicianInfo();
                    PhysicianBLL oPhysician = new PhysicianBLL();
                    oPhysicianInfo = oPhysician.getPhysicianDetailsByEmailId(oUserInfo.EmailId, oUserInfo.pin);
                    UserDetails objDetails = new UserDetails();
                    objDetails.UserId = "";
                    objDetails.MessengerMessage = "User found";
                    objDetails.PhysicianId = oPhysicianInfo.PhysicianId;
                    objDetails.IsAdmin = oPhysicianInfo.IsAdmin.ToString();
                    objDetails.IsSuperUser = oPhysicianInfo.IsSuperUser.ToString();
                    objDetails.IsStaff = oPhysicianInfo.IsStaff.ToString();
                    objDetails.UserId = oPhysicianInfo.PhysicianId.ToString();
                    objDetails.Application = oUserInfo.pin;
                    objDetails.UserType = exists;
                    objArray[0] = objDetails;
                    return objArray;
                }
                UserDetails objDetails3 = new UserDetails();
                objDetails3.UserId = "";
                objDetails3.MessengerMessage = "User not found";
                objArray[0] = objDetails3;
                return objArray;
            }
        }

        public string UpdatePassword(Stream message)
        {
            try
            {
                UserDetails objInput = JSonHelper.JsonDeserialize<UserDetails>(new StreamReader(message).ReadToEnd());
                if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
                {
                    return "Invalid PIN";
                }
                if (objInput.Pin != null && objInput.Pin.Length <= 0)
                {
                    return "Pin is a required field";

                }
                else
                {
                    UserInfo oUserInfo = new UserInfo();
                    oUserInfo.UserId = Convert.ToInt32(objInput.UserId);
                    oUserInfo.Password = objInput.Password;
                    oUserInfo.UserpasswordChangedDate = DateTime.Now;
                    oUserInfo.pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();
                    UserBLL oUser = new UserBLL();
                    int i = oUser.UpdatechangepasswordDetails(oUserInfo);

                    return "Update sucessful";

                }
            }
            catch (Exception ex)
            {
                return "An error occured. Please try again later";
            }
        }

        public UserDetails[] ForgotPassword(Stream message)
        {
            UserDetails objInput = JSonHelper.JsonDeserialize<UserDetails>(new StreamReader(message).ReadToEnd());
            if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
            {
                var objArray = new UserDetails[1];
                UserDetails objDetails = new UserDetails();
                objDetails.UserId = "";
                objDetails.MessengerMessage = "Invalid PIN";
                objArray[0] = objDetails;
                return objArray;
            }
            if (objInput.Pin != null && objInput.Pin.Length <= 0)
            {
                var objArray = new UserDetails[1];
                UserDetails objDetails = new UserDetails();
                objDetails.UserId = "";
                objDetails.MessengerMessage = "Pin is a required field";
                objArray[0] = objDetails;
                return objArray;
            }
            else
            {
                var objArray = new UserDetails[1];
                UserInfo oUserInfo = new UserInfo();
                oUserInfo.EmailId = objInput.UserName;
                oUserInfo.pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();
                UserBLL oUser = new UserBLL();
                UserInfo ouserinfo = oUser.getUserDetailsByEmailId(oUserInfo.EmailId, oUserInfo.pin);
                if (ouserinfo != null && ouserinfo.UserId > 0)
                {
                    UserDetails objDetails = new UserDetails();
                    objDetails.UserId = ouserinfo.UserId.ToString();
                    objDetails.MessengerMessage = "";
                    objDetails.SecurityQuestion1 = ouserinfo.SecurityQuestion1;
                    objDetails.SecurityQuestion2 = ouserinfo.SecurityQuestion2;
                    objDetails.SecurityQuestion3 = ouserinfo.SecurityQuestion3;
                    objDetails.SecurityAnswer1 = ouserinfo.SecurityAnswer1;
                    objDetails.SecurityAnswer2 = ouserinfo.SecurityAnswer2;
                    objDetails.SecurityAnswer3 = ouserinfo.SecurityAnswer3;


                    objArray[0] = objDetails;
                    return objArray;
                }
                else
                {
                    objArray = new UserDetails[1];
                    UserDetails objDetails = new UserDetails();
                    objDetails.UserId = "";
                    objDetails.MessengerMessage = "User not found";
                    objArray[0] = objDetails;
                    return objArray;
                }
            }
        }



        public MessageBox[] SentMessages(Stream message)
        {
            try
            {
                MessageBox objInput = JSonHelper.JsonDeserialize<MessageBox>(new StreamReader(message).ReadToEnd());
                if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
                {
                    var objArray = new MessageBox[1];
                    List<MessageInfo> objcol = new List<MessageInfo>();
                    MessageInfo obj = new MessageInfo();
                    MessageBLL objbll = new MessageBLL();
                    MessageBox obj2 = new MessageBox();
                    obj2.ErrorMessage = "Invalid PIN";
                    obj2.MessengerMessage = "Invalid PIN";
                    objArray[0] = obj2;
                    return objArray;
                }
                if (objInput.Pin != null && objInput.Pin.Length <= 0)
                {
                    var objArray = new MessageBox[1];
                    List<MessageInfo> objcol = new List<MessageInfo>();
                    MessageInfo obj = new MessageInfo();
                    MessageBLL objbll = new MessageBLL();
                    MessageBox obj2 = new MessageBox();
                    obj2.ErrorMessage = "PIN is a required field";
                    obj2.MessengerMessage = "Pin is a required field";
                    objArray[0] = obj2;
                    return objArray;
                }
                else
                {
                    var objArray = new MessageBox[1];
                    List<MessageInfo> objcol = new List<MessageInfo>();
                    MessageInfo obj = new MessageInfo();
                    MessageBLL objbll = new MessageBLL();
                    MessageBox obj2 = new MessageBox();
                    obj2.ErrorMessage = "No messages found";
                    obj2.MessengerMessage = "No messages found";
                    obj.FromId = objInput.FromId;
                    obj.FromType = objInput.FromType;
                    obj.Getattachment = objInput.GetAttachment;
                    obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();
                    objcol = objbll.BindSentMessages(obj);
                    if (objcol.Count() > 0)
                    {
                        var objBox = new MessageBox[objcol.Count()];
                        for (int i = 0; i < objcol.Count(); i++)
                        {
                            MessageBox objR = new MessageBox();
                            objR.MessageComposeId = objcol[i].MessageComposeId;
                            objR.FromId = objcol[i].FromId;
                            objR.ToId = objcol[i].ToId;
                            objR.FromEmailId = objcol[i].FromEmailId;
                            objR.ToEmailId = objcol[i].ToEmailId;
                            objR.MessageId = objcol[i].MessageId;
                            objR.Subject = objcol[i].Subject;
                            objR.Message = objcol[i].Message;
                            if (objcol[i].Attachment != null && objcol[i].Attachment.Length > 0)
                            {
                                //byte[] filecontent = File.ReadAllBytes(objcol[i].Attachment);

                                string[] files = objcol[i].Attachment.Split(',');
                                string[] filextensions = new string[files.Count()];

                                for (int icnt = 0; icnt <= files.GetUpperBound(0); icnt++)
                                {
                                    string[] filecomps = files[icnt].Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                                    if (filecomps.GetUpperBound(0) >= 1)
                                    {
                                        filextensions[icnt] = filecomps[1];
                                    }
                                }

                                //FileInfo fi = new FileInfo(objcol[i].Attachment);
                                //objR.Attachment = Convert.ToBase64String(filecontent);
                                //string extension = fi.Name.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[1];
                                objR.AttachmentExtension = filextensions; //extension.Split(',');
                            }
                            objR.LocationCoordinates = objcol[i].LocationCoordinates;
                            objR.FromType = objcol[i].FromType;
                            objR.ToType = objcol[i].ToType;
                            objR.Status = objcol[i].Status;
                            objR.ProcessDate = objcol[i].ProcessDate;
                            objR.FromName = objcol[i].FromName;
                            objR.userType = objcol[i].userType;
                            objR.physicianId = objcol[i].physicianId;
                            objR.MessageRepliedTo = objcol[i].MessageRepliedTo;
                            objR.IsMessageRead = objcol[i].IsMessageRead;
                            objR.MessageSignatureId = objcol[i].MessageSignatureId;
                            objR.MessageSignatureName = objcol[i].MessageSignatureName;
                            objR.MessageSignatureOffcName = objcol[i].MessageSignatureOffcName;

                            objBox[i] = objR;
                        }
                        return objBox;
                    }
                    objArray[0] = obj2;
                    return objArray;
                }
            }
            catch (Exception ex)
            {
                var objArray = new MessageBox[1];
                List<MessageInfo> objcol = new List<MessageInfo>();
                MessageInfo obj = new MessageInfo();
                MessageBLL objbll = new MessageBLL();
                MessageBox obj2 = new MessageBox();
                obj2.ErrorMessage = ex.Message;
                obj2.MessengerMessage = ex.Message;
                objArray[0] = obj2;
                return objArray;
            }
        }

        public MessageBox[] GetAttachment(Stream message)
        {
            MessageBox objInput = JSonHelper.JsonDeserialize<MessageBox>(new StreamReader(message).ReadToEnd());
            if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
            {
                var objArray = new MessageBox[1];
                MessageBox obj2 = new MessageBox();
                obj2.ErrorMessage = "Invalid PIN";
                obj2.MessengerMessage = "Invalid PIN";
                objArray[0] = obj2;
                return objArray;
            }
            if (objInput.Pin != null && objInput.Pin.Length > 0)
            {
                var objArray = new MessageBox[1];
                MessageBox obj2 = new MessageBox();
                obj2.ErrorMessage = "No messages found";
                obj2.MessengerMessage = "No messages found";
                List<MessageInfo> objcol = new List<MessageInfo>();
                MessageInfo obj = new MessageInfo();
                MessageBLL objbll = new MessageBLL();
                obj.MessageId = objInput.MessageId;
                obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();
                objcol = objbll.GetAttachmentByMesageId(obj);
                if (objcol.Count() > 0)
                {
                    var objBox = new MessageBox[objcol.Count()];
                    for (int i = 0; i < objcol.Count(); i++)
                    {
                        MessageBox objR = new MessageBox();
                        if (objcol[i].Attachment != null && objcol[i].Attachment.Length > 0)
                        {
                            string[] filenames = objcol[i].Attachment.Split(',');
                            string[] filecont = new string[1];
                            string[] fileext = new string[1];
                            for (int icnt = 0; icnt <= filenames.GetUpperBound(0); icnt++)
                            {
                                if (icnt == objInput.AttachmentIndex)
                                {
                                    byte[] filecontent = File.ReadAllBytes(filenames[icnt]);
                                    FileInfo fi = new FileInfo(filenames[icnt]);
                                    filecont[0] = Convert.ToBase64String(filecontent);
                                    fileext[0] = fi.Name.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[1];
                                    objR.Attachment = filecont;
                                    objR.AttachmentExtension = fileext;
                                    break;
                                }

                            }

                        }
                        objBox[i] = objR;
                    }
                    return objBox;
                }
                objArray[0] = obj2;
                return objArray;
            }
            else
            {
                var objArray = new MessageBox[1];
                MessageBox obj2 = new MessageBox();
                obj2.ErrorMessage = "PIN is a required field";
                obj2.MessengerMessage = "Pin is a required field";
                objArray[0] = obj2;
                return objArray;
            }
        }

        public string UpdateMessage(Stream message)
        {
            try
            {
                MessageBox objInput = JSonHelper.JsonDeserialize<MessageBox>(new StreamReader(message).ReadToEnd());
                if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
                {
                    return "Invalid PIN";
                }
                if (objInput.Pin != null && objInput.Pin.Length > 0)
                {
                    MessageInfo obj = new MessageInfo();
                    MessageBLL objbll = new MessageBLL();

                    obj.MessageId = objInput.MessageId;
                    obj.IsMessageRead = objInput.IsMessageRead;

                    obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();



                    int i = objbll.updatemessage(obj);

                    return "Updated successfully";

                }
                else
                {
                    return "Pin is a required field";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public MessageBox[] MyMessages(Stream message)
        {
            MessageBox objInput = JSonHelper.JsonDeserialize<MessageBox>(new StreamReader(message).ReadToEnd());
            if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
            {
                var objArray = new MessageBox[1];
                MessageBox obj2 = new MessageBox();
                obj2.ErrorMessage = "Invalid PIN";
                obj2.MessengerMessage = "Invalid PIN";
                objArray[0] = obj2;
                return objArray;
            }
            if (objInput.Pin != null && objInput.Pin.Length > 0)
            {
                var objArray = new MessageBox[1];
                MessageBox obj2 = new MessageBox();
                obj2.ErrorMessage = "No messages found";
                obj2.MessengerMessage = "No messages found";
                List<MessageInfo> objcol = new List<MessageInfo>();
                MessageInfo obj = new MessageInfo();
                MessageBLL objbll = new MessageBLL();
                obj.ToId = objInput.ToId;
                obj.ToType = objInput.userType;
                obj.Getattachment = objInput.GetAttachment;
                obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();
                objcol = objbll.BindInboxMessages(obj);
                if (objcol.Count() > 0)
                {
                    var objBox = new MessageBox[objcol.Count()];
                    for (int i = 0; i < objcol.Count(); i++)
                    {
                        MessageBox objR = new MessageBox();
                        objR.MessageComposeId = objcol[i].MessageComposeId;
                        objR.FromId = objcol[i].FromId;
                        objR.ToId = objcol[i].ToId;
                        objR.FromEmailId = objcol[i].FromEmailId;
                        objR.ToEmailId = objcol[i].ToEmailId;
                        objR.MessageId = objcol[i].MessageId;
                        objR.Subject = objcol[i].Subject;
                        objR.Message = objcol[i].Message;
                        if (objcol[i].Attachment != null && objcol[i].Attachment.Length > 0)
                        {
                            ////byte[] filecontent = File.ReadAllBytes(objcol[i].Attachment);
                            //FileInfo fi = new FileInfo(objcol[i].Attachment);
                            ////objR.Attachment = Convert.ToBase64String(filecontent);
                            //string extension = fi.Name.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[1];
                            //objR.AttachmentExtension = extension.Split(',');


                            string[] files = objcol[i].Attachment.Split(',');
                            string[] filextensions = new string[files.Count()];

                            for (int icnt = 0; icnt <= files.GetUpperBound(0); icnt++)
                            {
                                string[] filecomps = files[icnt].Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                                if (filecomps.GetUpperBound(0) >= 1)
                                {
                                    filextensions[icnt] = filecomps[1];
                                }
                            }


                            objR.AttachmentExtension = filextensions;
                        }
                        objR.LocationCoordinates = objcol[i].LocationCoordinates;
                        objR.FromType = objcol[i].FromType;
                        objR.ToType = objcol[i].ToType;
                        objR.Status = objcol[i].Status;
                        objR.ProcessDate = objcol[i].ProcessDate;
                        objR.FromName = objcol[i].FromName;
                        objR.userType = objcol[i].userType;
                        objR.physicianId = objcol[i].physicianId;
                        objR.MessageRepliedTo = objcol[i].MessageRepliedTo;
                        objR.IsMessageRead = objcol[i].IsMessageRead;
                        objR.MessageSignatureId = objcol[i].MessageSignatureId;
                        objR.MessageSignatureName = objcol[i].MessageSignatureName;
                        objR.MessageSignatureOffcName = objcol[i].MessageSignatureOffcName;
                        objBox[i] = objR;
                    }
                    return objBox;
                }
                objArray[0] = obj2;
                return objArray;
            }
            else
            {
                var objArray = new MessageBox[1];
                MessageBox obj2 = new MessageBox();
                obj2.ErrorMessage = "PIN is a required field";
                obj2.MessengerMessage = "Pin is a required field";
                objArray[0] = obj2;
                return objArray;
            }
        }
        public MessageBox[] DeletedMessages(Stream message)
        {
            MessageBox objInput = JSonHelper.JsonDeserialize<MessageBox>(new StreamReader(message).ReadToEnd());
            if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
            {
                var objArray = new MessageBox[1];
                MessageBox obj2 = new MessageBox();
                obj2.ErrorMessage = "Invalid PIN";
                obj2.MessengerMessage = "Invalid PIN";
                objArray[0] = obj2;
                return objArray;
            }
            if (objInput.Pin != null && objInput.Pin.Length > 0)
            {
                var objArray = new MessageBox[1];
                MessageBox obj2 = new MessageBox();
                obj2.ErrorMessage = "No messages found";
                obj2.MessengerMessage = "No messages found";
                List<MessageInfo> objcol = new List<MessageInfo>();
                MessageInfo obj = new MessageInfo();
                MessageBLL objbll = new MessageBLL();
                obj.ToId = objInput.ToId;
                obj.ToType = objInput.ToType;
                obj.Getattachment = objInput.GetAttachment;
                obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();
                objcol = objbll.BindDeleteMessages(obj);
                if (objcol.Count() > 0)
                {
                    var objBox = new MessageBox[objcol.Count()];
                    for (int i = 0; i < objcol.Count(); i++)
                    {
                        MessageBox objR = new MessageBox();
                        objR.MessageComposeId = objcol[i].MessageComposeId;
                        objR.MessageId = objcol[i].MessageId;
                        objR.FromId = objcol[i].FromId;
                        objR.ToId = objcol[i].ToId;
                        objR.FromEmailId = objcol[i].FromEmailId;
                        objR.ToEmailId = objcol[i].ToEmailId;
                        objR.MessageId = objcol[i].MessageId;
                        objR.Subject = objcol[i].Subject;
                        objR.Message = objcol[i].Message;
                        if (objcol[i].Attachment != null && objcol[i].Attachment.Length > 0)
                        {
                            ////byte[] filecontent = File.ReadAllBytes(objcol[i].Attachment);
                            //FileInfo fi = new FileInfo(objcol[i].Attachment);
                            ////objR.Attachment = Convert.ToBase64String(filecontent);
                            //string extension = fi.Name.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[1];
                            //objR.AttachmentExtension = extension.Split(',');

                            string[] files = objcol[i].Attachment.Split(',');
                            string[] filextensions = new string[files.Count()];

                            for (int icnt = 0; icnt <= files.GetUpperBound(0); icnt++)
                            {
                                string[] filecomps = files[icnt].Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                                if (filecomps.GetUpperBound(0) >= 1)
                                {
                                    filextensions[icnt] = filecomps[1];
                                }
                            }


                            objR.AttachmentExtension = filextensions;
                        }
                        objR.LocationCoordinates = objcol[i].LocationCoordinates;
                        objR.FromType = objcol[i].FromType;
                        objR.ToType = objcol[i].ToType;
                        objR.Status = objcol[i].Status;
                        objR.ProcessDate = objcol[i].ProcessDate;
                        objR.FromName = objcol[i].FromName;
                        objR.userType = objcol[i].userType;
                        objR.physicianId = objcol[i].physicianId;
                        objR.MessageRepliedTo = objcol[i].MessageRepliedTo;
                        objR.IsMessageRead = objcol[i].IsMessageRead;
                        objR.MessageSignatureId = objcol[i].MessageSignatureId;
                        objR.MessageSignatureName = objcol[i].MessageSignatureName;
                        objR.MessageSignatureOffcName = objcol[i].MessageSignatureOffcName;
                        objBox[i] = objR;
                    }
                    return objBox;
                }
                objArray[0] = obj2;
                return objArray;
            }
            else
            {
                var objArray = new MessageBox[1];
                MessageBox obj2 = new MessageBox();
                obj2.ErrorMessage = "No messages found";
                obj2.MessengerMessage = "No messages found";
                objArray[0] = obj2;
                return objArray;
            }
        }
        public string ComposeReply(Stream message)
        {
            try
            {
                ComposeMessage objInput = JSonHelper.JsonDeserialize<ComposeMessage>(new StreamReader(message).ReadToEnd());
                if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
                {

                    return "Invalid PIN";
                }
                if (objInput.Pin != null && objInput.Pin.Length > 0)
                {
                    MessageInfo obj = new MessageInfo();
                    MessageBLL objbll = new MessageBLL();
                    string filepath = "";
                    if (objInput.Attachment != null && objInput.Attachment.GetUpperBound(0) >= 0)
                    {
                        for (int ictr = 0; ictr <= objInput.Attachment.GetUpperBound(0); ictr++)
                        {
                            if (filepath.Length <= 0)
                            {
                                filepath = System.Configuration.ConfigurationManager.AppSettings["messageattachmentfolder"].ToString() + "\\" + Guid.NewGuid().ToString() + "." + objInput.Attachmentextension[ictr];

                                File.WriteAllBytes(filepath, Convert.FromBase64String(objInput.Attachment[ictr]));
                            }
                            else
                            {
                                string filepath1 = System.Configuration.ConfigurationManager.AppSettings["messageattachmentfolder"].ToString() + "\\" + Guid.NewGuid().ToString() + "." + objInput.Attachmentextension[ictr];

                                File.WriteAllBytes(filepath1, Convert.FromBase64String(objInput.Attachment[ictr]));

                                filepath = filepath + "," + filepath1;
                            }
                        }
                    }
                    obj.MessageSignatureId = objInput.MessageSignatureId;
                    obj.FromId = objInput.FromId;
                    obj.FromType = objInput.FromType;

                    obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();

                    PhysicianInfo objinfo = new PhysicianInfo();
                    PhysicianBLL objphy = new PhysicianBLL();
                    int PhysicianId = objInput.PhysicianId;
                    objinfo = objphy.GetPhysicianInfoByPatientId(PhysicianId, obj.Pin);
                    if (objinfo.UserType == 24)
                    {
                        obj.ToId = objInput.ToId;//Convert.ToInt32(dd_To.SelectedValue.ToString());
                        obj.ToType = objInput.ToType;
                    }
                    else if (objinfo.UserType == 23)
                    {
                        obj.ToId = objInput.ToId;//Convert.ToInt32(dd_To.SelectedValue.ToString());
                        obj.ToType = 23;
                    }
                    else
                    {
                        obj.ToId = objInput.ToId;//Convert.ToInt32(dd_To.SelectedValue.ToString());
                        obj.ToType = objInput.ToType;
                    }

                    obj.ToEmailId = objInput.ToEmailId;
                    obj.Subject = objInput.Subject;
                    obj.Message = objInput.Message;
                    if (objInput.Status != null && objInput.Status.Length > 0)
                    {
                        obj.Status = objInput.Status;  // R for reply message "O";
                        if (obj.Status == "R")
                        {
                            obj.MessageRepliedTo = objInput.MessageRepliedTo;
                        }
                    }
                    else
                    {
                        obj.Status = "R";
                    }
                    obj.LocationCoordinates = objInput.LocationCoordinates;
                    if (objInput.MessageId != 0)
                        obj.MessageId = objInput.MessageId;
                    else
                        obj.MessageId = 0;
                    if (filepath != null && filepath.Length > 0)
                        obj.Attachment = filepath;
                    int i = objbll.insertmessage(obj);
                    if (i > 0)
                        return "message sent successfully";
                    else
                        return "message not sent";
                }
                else
                {
                    return "Pin is a required field";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        public ToFields[] GetToField(Stream message)
        {

            ToFields objInput = JSonHelper.JsonDeserialize<ToFields>(new StreamReader(message).ReadToEnd());
            if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
            {
                var objArray1 = new ToFields[1];
                ToFields otoField = new ToFields();
                otoField.ToId = "0";
                otoField.Name = "";
                otoField.UserType = "0";
                otoField.MessengerMessage = "Invalid PIN";
                objArray1[0] = otoField;
                return objArray1;
            }
            if (objInput.Pin != null && objInput.Pin.Length > 0)
            {
                List<MessageInfo> objcol = new List<MessageInfo>();
                MessageInfo obj = new MessageInfo();
                MessageBLL objbll = new MessageBLL();
                obj.ToId = Convert.ToInt32(objInput.ToId);
                obj.userType = Convert.ToInt32(objInput.UserType);
                obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();
                objcol = objbll.GetToFieldAutopopup(obj, objInput.Name);
                var objArray = new ToFields[objcol.Count];
                var autoPopUpdata = new List<string>(objcol.Count);
                if (objcol.Count > 0)
                {

                    for (var i = 0; i < objcol.Count; i++)
                    {
                        //var elementAtOrDefault = objcol.ElementAtOrDefault(i);
                        //if (elementAtOrDefault != null)
                        //    autoPopUpdata.Add(string.Format("{0}-{1}-{2}", elementAtOrDefault.FromName,
                        //                                    elementAtOrDefault.ToId,
                        //                                    elementAtOrDefault.userType));

                        ToFields otoField = new ToFields();
                        MessageInfo msginfo = objcol[i];
                        otoField.ToId = msginfo.ToId.ToString();
                        otoField.Name = msginfo.FromName;
                        otoField.ToEmail = msginfo.ToEmailId;
                        otoField.UserType = msginfo.ToType.ToString();
                        objArray[i] = otoField;
                    }
                }
                return objArray;
            }
            else
            {
                var objArray1 = new ToFields[1];
                ToFields otoField = new ToFields();
                otoField.ToId = "0";
                otoField.Name = "";
                otoField.UserType = "0";
                otoField.MessengerMessage = "Pin is a required field";
                objArray1[0] = otoField;
                return objArray1;
            }
        }

        public PatientDetails[] GetPatientLookup(Stream message)
        {
            PatientDetails objInput = JSonHelper.JsonDeserialize<PatientDetails>(new StreamReader(message).ReadToEnd());
            if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
            {
                PatientDetails patdet = new PatientDetails();
                patdet.PatientId = 0;
                patdet.FirstName = "";
                patdet.LastName = "";
                patdet.DOB = "";
                patdet.Phone = "";
                patdet.MessengerMessage = "Invalid PIN";
                var colpatdets = new PatientDetails[1];
                colpatdets[0] = patdet;
                return colpatdets;
            }
            if (objInput.Pin != null && objInput.Pin.Length > 0)
            {
                if (objInput.PhysicianId != null && objInput.PhysicianId.Length > 0)
                {
                    IList<PatientInfo> objcol = new List<PatientInfo>();
                    PatientInfo obj = new PatientInfo();
                    Patient objbll = new Patient();



                    obj.Lastname = objInput.LastName;
                    obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();
                    obj.Physician = objInput.PhysicianId.ToString();
                    if (objInput.DOB == null || objInput.DOB.ToString().Length <= 0)
                    {
                        obj.DateOfBirth = DateTime.MinValue;
                    }




                    objcol = objbll.SearchPatientsDetailsByCriteria(obj);
                    var colPatDetails = new PatientDetails[objcol.Count];
                    var autoPopUpdata = new List<string>(objcol.Count);
                    if (objcol.Count > 0)
                    {
                        for (var i = 0; i < objcol.Count; i++)
                        {
                            PatientDetails patdet = new PatientDetails();
                            PatientInfo patinfo = objcol[i];
                            patdet.PatientId = patinfo.PatientId;
                            patdet.FirstName = patinfo.FirstName;
                            patdet.LastName = patinfo.Lastname;
                            patdet.DOB = patinfo.DateOfBirth.ToString("MM/dd/yyyy");
                            if (patinfo.PhoneNumber != null)
                            {
                                patdet.Phone = patinfo.PhoneNumber;
                            }
                            colPatDetails[i] = patdet;

                            //var elementAtOrDefault = objcol.ElementAtOrDefault(i);
                            //if (elementAtOrDefault != null)
                            //    autoPopUpdata.Add(string.Format("{0}-{1}-{2}-{3}-{4}", elementAtOrDefault.PatientId,
                            //                                    elementAtOrDefault.FirstName,
                            //                                    elementAtOrDefault.Lastname,
                            //                                    elementAtOrDefault.DateOfBirth,
                            //                                    elementAtOrDefault.PhoneNumber));
                        }
                    }
                    return colPatDetails;
                }
                else
                {
                    PatientDetails patdet = new PatientDetails();
                    patdet.PatientId = 0;
                    patdet.FirstName = "";
                    patdet.LastName = "";
                    patdet.DOB = "";
                    patdet.Phone = "";
                    patdet.MessengerMessage = "Physician id is a required field";
                    var colpatdets = new PatientDetails[1];
                    colpatdets[0] = patdet;
                    return colpatdets;
                }
            }
            else
            {
                PatientDetails patdet = new PatientDetails();
                patdet.PatientId = 0;
                patdet.FirstName = "";
                patdet.LastName = "";
                patdet.DOB = "";
                patdet.Phone = "";
                patdet.MessengerMessage = "Pin is a required field";
                var colpatdets = new PatientDetails[1];
                colpatdets[0] = patdet;
                return colpatdets;
            }
        }

        public PatientDetails[] GetPatientDetails(Stream message)
        {
            try
            {
                PatientDetails objInput = JSonHelper.JsonDeserialize<PatientDetails>(new StreamReader(message).ReadToEnd());
                if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
                {
                    PatientDetails patdet = new PatientDetails();
                    patdet.PatientId = 0;
                    patdet.FirstName = "";
                    patdet.LastName = "";
                    patdet.DOB = "";
                    patdet.Phone = "";
                    patdet.MessengerMessage = "Invalid PIN";
                    var colpatdets = new PatientDetails[1];
                    colpatdets[0] = patdet;
                    return colpatdets;
                }
                if (objInput.Pin != null && objInput.Pin.Length > 0)
                {
                    if (objInput.PhysicianId != null && objInput.PhysicianId.ToString().Length > 0)
                    {
                        IList<PatientInfo> objcol = new List<PatientInfo>();
                        PatientInfo obj = new PatientInfo();
                        Patient objbll = new Patient();

                        obj.Physician = objInput.PhysicianId;
                        if (objInput.FirstName != null)
                            obj.FirstName = objInput.FirstName;
                        if (objInput.LastName != null)
                            obj.Lastname = objInput.LastName;

                        obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();
                        if (objInput.DOB != null && objInput.DOB.Length > 0)
                        {
                            obj.DateOfBirth = Convert.ToDateTime(objInput.DOB);
                        }
                        else
                        {
                            obj.DateOfBirth = DateTime.MinValue;
                        }

                        objcol = objbll.SearchPatientsDetailsByCriteria(obj);
                        var colPatDetails = new PatientDetails[objcol.Count];
                        var autoPopUpdata = new List<string>(objcol.Count);
                        if (objcol.Count > 0)
                        {
                            for (var i = 0; i < objcol.Count; i++)
                            {
                                PatientDetails patdet = new PatientDetails();
                                PatientInfo patinfo = objcol[i];
                                patdet.PatientId = patinfo.PatientId;
                                patdet.FirstName = patinfo.FirstName;
                                patdet.LastName = patinfo.Lastname;
                                patdet.DOB = patinfo.DateOfBirth_String;
                                patdet.Phone = patinfo.PhoneNumber;
                                colPatDetails[i] = patdet;

                                //var elementAtOrDefault = objcol.ElementAtOrDefault(i);
                                //if (elementAtOrDefault != null)
                                //    autoPopUpdata.Add(string.Format("{0}-{1}-{2}-{3}-{4}", elementAtOrDefault.PatientId,
                                //                                    elementAtOrDefault.FirstName,
                                //                                    elementAtOrDefault.Lastname,
                                //                                    elementAtOrDefault.DateOfBirth,
                                //                                    elementAtOrDefault.PhoneNumber));
                            }
                        }
                        return colPatDetails;
                    }
                    else
                    {
                        PatientDetails patdet = new PatientDetails();
                        patdet.PatientId = 0;
                        patdet.FirstName = "";
                        patdet.LastName = "";
                        patdet.DOB = "";
                        patdet.Phone = "";
                        patdet.MessengerMessage = "Physician id is a required field";
                        var colpatdets = new PatientDetails[1];
                        colpatdets[0] = patdet;
                        return colpatdets;
                    }
                }
                else
                {
                    PatientDetails patdet = new PatientDetails();
                    patdet.PatientId = 0;
                    patdet.FirstName = "";
                    patdet.LastName = "";
                    patdet.DOB = "";
                    patdet.Phone = "";
                    patdet.MessengerMessage = "Pin is a required field";
                    var colpatdets = new PatientDetails[1];
                    colpatdets[0] = patdet;
                    return colpatdets;
                }
            }
            catch (Exception ex)
            {
                PatientDetails patdet = new PatientDetails();
                patdet.PatientId = 0;
                patdet.FirstName = "";
                patdet.LastName = "";
                patdet.DOB = "";
                patdet.Phone = "";
                patdet.MessengerMessage = ex.Message;
                var colpatdets = new PatientDetails[1];
                colpatdets[0] = patdet;
                return colpatdets;
            }
        }

        public EmergencyContact[] GetEmergencyContact(Stream message)
        {
            EmergencyContact objInput = JSonHelper.JsonDeserialize<EmergencyContact>(new StreamReader(message).ReadToEnd());
            if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
            {
                var colemergencies = new EmergencyContact[1];
                EmergencyContact emercont = new EmergencyContact();
                emercont.PrimaryEmergencyFirstName = "";
                emercont.PrimaryEmergencyLastName = "";
                emercont.PrimaryEmergencyRelationship = "";
                emercont.PrimaryEmergencyAddress1 = "";
                emercont.PrimaryEmergencyAddress2 = "";
                emercont.PrimaryEmergencyCity = "";
                emercont.PrimaryEmergencyState = "";
                emercont.PrimaryEmergencyHomePhone = "";
                emercont.PrimaryEmergencyWorkPhone = "";
                emercont.PrimaryEmergencyCellPhone = "";

                emercont.SecondaryEmergencyFirstName = "";
                emercont.SecondaryEmergencyLastName = "";
                emercont.SecondaryEmergencyRelationship = "";
                emercont.SecondaryEmergencyAddress1 = "";
                emercont.SecondaryEmergencyAddress2 = "";
                emercont.SecondaryEmergencyCity = "";
                emercont.SecondaryEmergencyState = "";
                emercont.SecondaryEmergencyHomePhone = "";
                emercont.SecondaryEmergencyWorkPhone = "";
                emercont.SecondaryEmergencyCellPhone = "";
                emercont.MessengerMessage = "Invalid PIN";
                colemergencies[0] = emercont;

                return colemergencies;
            }
            if (objInput.Pin != null && objInput.Pin.Length > 0)
            {
                if (objInput.PatientID > 0)
                {
                    IList<GHPEmergencyContactsInfo> objcol = new List<GHPEmergencyContactsInfo>();
                    GHPEmergencyContactsInfo obj = new GHPEmergencyContactsInfo();
                    GHPEmergencyContactsBLL objbll = new GHPEmergencyContactsBLL();
                    obj.PatientId = objInput.PatientID;
                    obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();

                    objcol = objbll.GetGHPEmergencyContactsInfo(obj);
                    var colemercontact = new EmergencyContact[objcol.Count];
                    var autoPopUpdata = new List<string>(objcol.Count);
                    if (objcol.Count > 0)
                    {
                        for (var i = 0; i < objcol.Count; i++)
                        {
                            GHPEmergencyContactsInfo emerinfo = objcol[i];
                            EmergencyContact emercont = new EmergencyContact();
                            emercont.PrimaryEmergencyFirstName = emerinfo.PrimaryEmergencyFirstName;
                            emercont.PrimaryEmergencyLastName = emerinfo.PrimaryEmergencyLastName;
                            emercont.PrimaryEmergencyRelationship = emerinfo.PrimaryEmergencyRelationship;
                            emercont.PrimaryEmergencyAddress1 = emerinfo.PrimaryEmergencyAddress1;
                            emercont.PrimaryEmergencyAddress2 = emerinfo.PrimaryEmergencyAddress2;
                            emercont.PrimaryEmergencyCity = emerinfo.PrimaryEmergencyCity;
                            emercont.PrimaryEmergencyState = emerinfo.PrimaryEmergencyState;
                            emercont.PrimaryEmergencyHomePhone = emerinfo.PrimaryEmergencyHomePhone;
                            emercont.PrimaryEmergencyWorkPhone = emerinfo.PrimaryEmergencyWorkPhone;
                            emercont.PrimaryEmergencyCellPhone = emerinfo.PrimaryEmergencyCellPhone;

                            emercont.SecondaryEmergencyFirstName = emerinfo.SecondaryEmergencyFirstName;
                            emercont.SecondaryEmergencyLastName = emerinfo.SecondaryEmergencyLastName;
                            emercont.SecondaryEmergencyRelationship = emerinfo.SecondaryEmergencyRelationship;
                            emercont.SecondaryEmergencyAddress1 = emerinfo.SecondaryEmergencyAddress1;
                            emercont.SecondaryEmergencyAddress2 = emerinfo.SecondaryEmergencyAddress2;
                            emercont.SecondaryEmergencyCity = emerinfo.SecondaryEmergencyCity;
                            emercont.SecondaryEmergencyState = emerinfo.SecondaryEmergencyState;
                            emercont.SecondaryEmergencyHomePhone = emerinfo.SecondaryEmergencyHomePhone;
                            emercont.SecondaryEmergencyWorkPhone = emerinfo.SecondaryEmergencyWorkPhone;
                            emercont.SecondaryEmergencyCellPhone = emerinfo.SecondaryEmergencyCellPhone;

                            colemercontact[i] = emercont;
                            //var elementAtOrDefault = objcol.ElementAtOrDefault(i);
                            //if (elementAtOrDefault != null)
                            //    autoPopUpdata.Add(string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}-{11}-{12}-{13}-{14}-{15}-{16}-{17}-{18}-{19}"
                            //                                    , elementAtOrDefault.PrimaryEmergencyFirstName,
                            //                                    elementAtOrDefault.PrimaryEmergencyLastName,
                            //                                    elementAtOrDefault.PrimaryEmergencyRelationship,
                            //                                    elementAtOrDefault.PrimaryEmergencyAddress1,
                            //                                    elementAtOrDefault.PrimaryEmergencyAddress2,
                            //                                    elementAtOrDefault.PrimaryEmergencyCity,
                            //                                    elementAtOrDefault.PrimaryEmergencyState,
                            //                                    elementAtOrDefault.PrimaryEmergencyCellPhone,
                            //                                    elementAtOrDefault.PrimaryEmergencyHomePhone,
                            //                                    elementAtOrDefault.PrimaryEmergencyWorkPhone,
                            //                                    elementAtOrDefault.SecondaryEmergencyFirstName,
                            //                                    elementAtOrDefault.SecondaryEmergencyLastName,
                            //                                    elementAtOrDefault.SecondaryEmergencyRelationship,
                            //                                    elementAtOrDefault.SecondaryEmergencyAddress1,
                            //                                    elementAtOrDefault.SecondaryEmergencyAddress2,
                            //                                    elementAtOrDefault.SecondaryEmergencyCity,
                            //                                    elementAtOrDefault.SecondaryEmergencyState,
                            //                                    elementAtOrDefault.SecondaryEmergencyCellPhone,
                            //                                    elementAtOrDefault.SecondaryEmergencyHomePhone,
                            //                                    elementAtOrDefault.SecondaryEmergencyWorkPhone
                            //                                    ));
                        }
                    }
                    return colemercontact;
                }
                else
                {
                    var colemergencies = new EmergencyContact[1];
                    EmergencyContact emercont = new EmergencyContact();
                    emercont.PrimaryEmergencyFirstName = "";
                    emercont.PrimaryEmergencyLastName = "";
                    emercont.PrimaryEmergencyRelationship = "";
                    emercont.PrimaryEmergencyAddress1 = "";
                    emercont.PrimaryEmergencyAddress2 = "";
                    emercont.PrimaryEmergencyCity = "";
                    emercont.PrimaryEmergencyState = "";
                    emercont.PrimaryEmergencyHomePhone = "";
                    emercont.PrimaryEmergencyWorkPhone = "";
                    emercont.PrimaryEmergencyCellPhone = "";

                    emercont.SecondaryEmergencyFirstName = "";
                    emercont.SecondaryEmergencyLastName = "";
                    emercont.SecondaryEmergencyRelationship = "";
                    emercont.SecondaryEmergencyAddress1 = "";
                    emercont.SecondaryEmergencyAddress2 = "";
                    emercont.SecondaryEmergencyCity = "";
                    emercont.SecondaryEmergencyState = "";
                    emercont.SecondaryEmergencyHomePhone = "";
                    emercont.SecondaryEmergencyWorkPhone = "";
                    emercont.SecondaryEmergencyCellPhone = "";
                    emercont.MessengerMessage = "Patient id is a required field";
                    colemergencies[0] = emercont;

                    return colemergencies;
                }
            }
            else
            {
                var colemergencies = new EmergencyContact[1];
                EmergencyContact emercont = new EmergencyContact();
                emercont.PrimaryEmergencyFirstName = "";
                emercont.PrimaryEmergencyLastName = "";
                emercont.PrimaryEmergencyRelationship = "";
                emercont.PrimaryEmergencyAddress1 = "";
                emercont.PrimaryEmergencyAddress2 = "";
                emercont.PrimaryEmergencyCity = "";
                emercont.PrimaryEmergencyState = "";
                emercont.PrimaryEmergencyHomePhone = "";
                emercont.PrimaryEmergencyWorkPhone = "";
                emercont.PrimaryEmergencyCellPhone = "";

                emercont.SecondaryEmergencyFirstName = "";
                emercont.SecondaryEmergencyLastName = "";
                emercont.SecondaryEmergencyRelationship = "";
                emercont.SecondaryEmergencyAddress1 = "";
                emercont.SecondaryEmergencyAddress2 = "";
                emercont.SecondaryEmergencyCity = "";
                emercont.SecondaryEmergencyState = "";
                emercont.SecondaryEmergencyHomePhone = "";
                emercont.SecondaryEmergencyWorkPhone = "";
                emercont.SecondaryEmergencyCellPhone = "";
                emercont.MessengerMessage = "Pin is a required field";
                colemergencies[0] = emercont;

                return colemergencies;
            }
        }
        public string UpdateMedications(Stream message)
        {
            Medication objInput = JSonHelper.JsonDeserialize<Medication>(new StreamReader(message).ReadToEnd());
            if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
            {
                return "Invalid PIN";
            }
            if (objInput.Pin != null && objInput.Pin.Length > 0)
            {
                if (objInput.MedicationID > 0)
                {
                    MedicationInfo obj = new MedicationInfo();
                    MedicationBLL objbll = new MedicationBLL();
                    obj.PatientId = objInput.PatientID;
                    obj.Description = objInput.Description;
                    obj.Dosage = objInput.Dosage;
                    obj.Units = objInput.Units;
                    obj.Date = objInput.Date;
                    obj.Reason = objInput.Reason;
                    obj.MedicationId = objInput.MedicationID;
                    obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();

                    DataTransactionReturn ret = objbll.UpdateMedicationDetails(obj);
                    return ret.MedicationId.ToString();

                }
                else
                {
                    return "Medication ID is required field";
                }
            }
            else
            {
                return "PIN is a required field";
            }
        }

        public string UpdateMedicalHistory(Stream message)
        {
            Conditions objInput = JSonHelper.JsonDeserialize<Conditions>(new StreamReader(message).ReadToEnd());
            if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
            {
                return "Invalid PIN";
            }
            if (objInput.Pin != null && objInput.Pin.Length > 0)
            {
                if (objInput.PatientID > 0 && objInput.Condition.Length > 0)
                {
                    IList<GHPMedicalHistoryInfo> objcol = new List<GHPMedicalHistoryInfo>();
                    GHPMedicalHistoryInfo obj = new GHPMedicalHistoryInfo();
                    GHPMedicalHistoryBLL objbll = new GHPMedicalHistoryBLL();
                    obj.MedicalHistoryId = objInput.ConditionID;
                    obj.PatientId = objInput.PatientID;
                    obj.Condition = objInput.Condition;
                    obj.ConditionCheck = objInput.ConditionCheck;
                    obj.DateofOnset = objInput.DateOfOnset;
                    obj.ICDCode = objInput.ICDCode;
                    obj.ActionItem = "INSERT";
                    DataTransactionReturn ret = objbll.InsertGHPMedicalHistoryInfo(obj);

                    return ret.MedicalHistoryId.ToString();
                }
                else
                {

                    return "PatientID and Condition is required field";
                }
            }
            else
            {
                return "PIN is a required field";
            }
        }


        public Conditions[] GetMedicalHistory(Stream message)
        {
            Conditions objInput = JSonHelper.JsonDeserialize<Conditions>(new StreamReader(message).ReadToEnd());
            if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
            {
                var colconds = new Conditions[1];
                Conditions cond = new Conditions();
                cond.Condition = "";
                cond.ConditionCheck = "";
                cond.DateOfOnset = "";
                cond.ICDCode = "";
                cond.MessengerMessage = "Invalid PIN";

                colconds[0] = cond;
                return colconds;
            }
            if (objInput.Pin != null && objInput.Pin.Length > 0)
            {
                if (objInput.PatientID > 0)
                {
                    IList<GHPMedicalHistoryInfo> objcol = new List<GHPMedicalHistoryInfo>();
                    GHPMedicalHistoryInfo obj = new GHPMedicalHistoryInfo();
                    GHPMedicalHistoryBLL objbll = new GHPMedicalHistoryBLL();
                    obj.PatientId = objInput.PatientID;
                    obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();

                    objcol = objbll.GetGHPMedicalHistoryInfo(obj);

                    var colConditions = new Conditions[objcol.Count];

                    var autoPopUpdata = new List<string>(objcol.Count);
                    if (objcol.Count > 0)
                    {
                        for (var i = 0; i < objcol.Count; i++)
                        {

                            GHPMedicalHistoryInfo medinfo = objcol[i];
                            Conditions cond = new Conditions();
                            cond.ConditionID = medinfo.MedicalHistoryId;
                            cond.Condition = medinfo.Condition;
                            cond.ConditionCheck = medinfo.ConditionCheck;
                            cond.DateOfOnset = medinfo.DateofOnset;
                            cond.ICDCode = medinfo.ICDCode;

                            colConditions[i] = cond;

                            //var elementAtOrDefault = objcol.ElementAtOrDefault(i);
                            //if (elementAtOrDefault != null)
                            //    autoPopUpdata.Add(string.Format("{0}-{1}-{2}-{3}"
                            //                                    , elementAtOrDefault.Condition,
                            //                                    elementAtOrDefault.ConditionCheck,
                            //                                    elementAtOrDefault.DateofOnset,
                            //                                    elementAtOrDefault.ICDCode
                            //                                    ));
                        }
                    }
                    return colConditions;
                }
                else
                {
                    var colconds = new Conditions[1];
                    Conditions cond = new Conditions();
                    cond.Condition = "";
                    cond.ConditionCheck = "";
                    cond.DateOfOnset = "";
                    cond.ICDCode = "";
                    cond.MessengerMessage = "Patient id is a required field";

                    colconds[0] = cond;
                    return colconds;
                }
            }
            else
            {
                var colconds = new Conditions[1];
                Conditions cond = new Conditions();
                cond.Condition = "";
                cond.ConditionCheck = "";
                cond.DateOfOnset = "";
                cond.ICDCode = "";
                cond.MessengerMessage = "Pin is a required field";

                colconds[0] = cond;
                return colconds;

            }
        }

        public Medication[] GetMedications(Stream message)
        {
            Medication objInput = JSonHelper.JsonDeserialize<Medication>(new StreamReader(message).ReadToEnd());
            if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
            {
                var colmed = new Medication[1];
                Medication med = new Medication();
                med.Description = "";
                med.Dosage = "";
                med.Units = "";
                med.Date = "";
                med.Reason = "";
                med.MessengerMessage = "Invalid PIN";
                colmed[0] = med;

                return colmed;
            }
            if (objInput.Pin != null && objInput.Pin.Length > 0)
            {
                if (objInput.PatientID > 0)
                {
                    IList<MedicationInfo> objcol = new List<MedicationInfo>();
                    MedicationInfo obj = new MedicationInfo();
                    MedicationBLL objbll = new MedicationBLL();
                    obj.PatientId = objInput.PatientID;
                    obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();

                    objcol = objbll.GetMedicationDetails(obj);

                    var colmed = new Medication[objcol.Count];

                    var autoPopUpdata = new List<string>(objcol.Count);
                    if (objcol.Count > 0)
                    {
                        for (var i = 0; i < objcol.Count; i++)
                        {
                            MedicationInfo medinfo = objcol[i];
                            Medication med = new Medication();
                            med.Description = medinfo.Description;
                            med.Dosage = medinfo.Dosage;
                            med.Units = medinfo.Units;
                            med.Date = medinfo.Date;
                            med.Reason = medinfo.Reason;

                            colmed[i] = med;


                            //var elementAtOrDefault = objcol.ElementAtOrDefault(i);
                            //if (elementAtOrDefault != null)
                            //    autoPopUpdata.Add(string.Format("{0}-{1}-{2}-{3}-{4}"
                            //                                    , elementAtOrDefault.Description,
                            //                                    elementAtOrDefault.Dosage,
                            //                                    elementAtOrDefault.Units,
                            //                                    elementAtOrDefault.Date,
                            //                                    elementAtOrDefault.Reason
                            //                                    ));
                        }
                    }
                    return colmed;
                }
                else
                {
                    var colmed = new Medication[1];
                    Medication med = new Medication();
                    med.Description = "";
                    med.Dosage = "";
                    med.Units = "";
                    med.Date = "";
                    med.Reason = "";
                    med.MessengerMessage = "Patient id is a required field";
                    colmed[0] = med;

                    return colmed;
                }
            }
            else
            {
                var colmed = new Medication[1];
                Medication med = new Medication();
                med.Description = "";
                med.Dosage = "";
                med.Units = "";
                med.Date = "";
                med.Reason = "";
                med.MessengerMessage = "Pin is a required field";
                colmed[0] = med;

                return colmed;
            }
        }


        public string UpdateAllergies(Stream message)
        {
            Allergies objInput = JSonHelper.JsonDeserialize<Allergies>(new StreamReader(message).ReadToEnd());
            if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
            {
                return "Invalid PIN";

            }
            if (objInput.Pin != null && objInput.Pin.Length > 0)
            {
                if (objInput.PatientID > 0 && objInput.AllergyType.Length > 0)
                {
                    GHPAllergiesInfo obj = new GHPAllergiesInfo();
                    GHPAllergiesBLL objbll = new GHPAllergiesBLL();
                    obj.AllergiesId = objInput.AllergyID;
                    obj.PatientId = objInput.PatientID;
                    obj.AllergyType = objInput.AllergyType;
                    obj.Reaction = objInput.Reaction;
                    obj.DateLastOccured = objInput.DateLastOccured;
                    obj.Treatment = objInput.Treatment;
                    obj.IsMedicationAllergy = objInput.IsMedicationAllergy;
                    obj.ActionItem = "INSERT";
                    obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();
                    DataTransactionReturn ret = objbll.InsertGHPAllergiesInfo(obj);
                    return ret.AllergiesId.ToString();

                }
                else
                {


                    return "PatientID and AllergyType are required field";
                }
            }
            else
            {
                return "Pin is required field";

            }
        }

        public Allergies[] GetAllergies(Stream message)
        {
            try
            {
                Allergies objInput = JSonHelper.JsonDeserialize<Allergies>(new StreamReader(message).ReadToEnd());
                if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
                {
                    var colAllergies = new Allergies[1];
                    Allergies allergy = new Allergies();
                    allergy.AllergyType = "";
                    allergy.Reaction = "";
                    allergy.DateLastOccured = "";
                    allergy.Treatment = "";
                    allergy.IsMedicationAllergy = "";
                    allergy.MessengerMessage = "Invalid PIN";
                    colAllergies[0] = allergy;

                    return colAllergies;
                }
                if (objInput.Pin != null && objInput.Pin.Length > 0)
                {
                    if (objInput.PatientID > 0)
                    {
                        IList<GHPAllergiesInfo> objcol = new List<GHPAllergiesInfo>();
                        GHPAllergiesInfo obj = new GHPAllergiesInfo();
                        GHPAllergiesBLL objbll = new GHPAllergiesBLL();
                        obj.PatientId = objInput.PatientID;
                        obj.Pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();

                        objcol = objbll.GetGHPAllergiesInfo(obj);
                        var colAllergies = new Allergies[objcol.Count];
                        //var autoPopUpdata = new List<string>(objcol.Count);
                        if (objcol.Count > 0)
                        {
                            for (var i = 0; i < objcol.Count; i++)
                            {
                                GHPAllergiesInfo allergyinfo = objcol[i];
                                Allergies allergy = new Allergies();
                                allergy.AllergyID = allergyinfo.AllergiesId;
                                allergy.AllergyType = allergyinfo.AllergyType;
                                allergy.Reaction = allergyinfo.Reaction;
                                allergy.DateLastOccured = allergyinfo.DateLastOccured;
                                allergy.Treatment = allergyinfo.Treatment;
                                allergy.IsMedicationAllergy = allergyinfo.IsMedicationAllergy;

                                colAllergies[i] = allergy;

                                //var elementAtOrDefault = objcol.ElementAtOrDefault(i);
                                //if (elementAtOrDefault != null)
                                //    autoPopUpdata.Add(string.Format("{0}-{1}-{2}-{3}-{4}"
                                //                                    , elementAtOrDefault.AllergyType,
                                //                                    elementAtOrDefault.Reaction,
                                //                                    elementAtOrDefault.DateLastOccured,
                                //                                    elementAtOrDefault.Treatment,
                                //                                    elementAtOrDefault.IsMedicationAllergy
                                //                                    ));
                            }
                        }
                        return colAllergies;
                    }
                    else
                    {
                        var colAllergies = new Allergies[1];
                        Allergies allergy = new Allergies();
                        allergy.AllergyType = "";
                        allergy.Reaction = "";
                        allergy.DateLastOccured = "";
                        allergy.Treatment = "";
                        allergy.IsMedicationAllergy = "";
                        allergy.MessengerMessage = "Patient id is required field";
                        colAllergies[0] = allergy;

                        return colAllergies;
                    }
                }
                else
                {
                    var colAllergies = new Allergies[1];
                    Allergies allergy = new Allergies();
                    allergy.AllergyType = "";
                    allergy.Reaction = "";
                    allergy.DateLastOccured = "";
                    allergy.Treatment = "";
                    allergy.IsMedicationAllergy = "";
                    allergy.MessengerMessage = "Pin is required field";
                    colAllergies[0] = allergy;

                    return colAllergies;

                }
            }
            catch (Exception ex)
            {
                var colAllergies = new Allergies[1];
                Allergies allergy = new Allergies();
                allergy.AllergyType = "";
                allergy.Reaction = "";
                allergy.DateLastOccured = "";
                allergy.Treatment = "";
                allergy.IsMedicationAllergy = "";
                allergy.MessengerMessage = ex.StackTrace;
                colAllergies[0] = allergy;

                return colAllergies;
            }
        }

        public string Enroll(Stream message)
        {
            try
            {
                Enrollment objInput = JSonHelper.JsonDeserialize<Enrollment>(new StreamReader(message).ReadToEnd());
                if (System.Configuration.ConfigurationManager.AppSettings[objInput.Pin] == null)
                {


                    return "Invalid PIN";
                }
                if (objInput.Pin != null && objInput.Pin.Length > 0)
                {
                    UserBLL oUser1 = new UserBLL();
                    UserInfo _UserInfo1 = new UserInfo();
                    System.Data.DataSet ds = new System.Data.DataSet();
                    _UserInfo1.PracticeTin = objInput.PracticeTin;
                    _UserInfo1.PracticeName = objInput.PracticeName;
                    _UserInfo1.pin = System.Configuration.ConfigurationManager.AppSettings[objInput.Pin].ToString();
                    ds = oUser1.getpracticename1(objInput.PracticeTin);
                    int iPracticeID = 0;
                    int iLocationID = 0;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        iPracticeID = Convert.ToInt32(ds.Tables[0].Rows[0]["ProviderId"].ToString());

                        System.Data.DataSet dslocation = oUser1.getLocationDetails(objInput.PracticeTin);
                        iLocationID = Convert.ToInt32(dslocation.Tables[0].Rows[0]["LocationId"].ToString());
                    }
                    else
                    {
                        int i = oUser1.insertPractice(_UserInfo1);
                        if (i > 0)
                        {
                            ds = oUser1.getpracticename1(objInput.PracticeTin);

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                iPracticeID = Convert.ToInt32(ds.Tables[0].Rows[0]["ProviderId"].ToString());
                                System.Data.DataSet dslocation = oUser1.getLocationDetails(objInput.PracticeTin);
                                if (dslocation.Tables[0].Rows.Count <= 0)
                                {

                                    _UserInfo1.LocationName = objInput.LocationName;
                                    _UserInfo1.OrganisationName = objInput.OrganizationType;
                                    _UserInfo1.practicephone = objInput.PracticePhone;
                                    _UserInfo1.PracticeAddress = objInput.Address;
                                    _UserInfo1.practiceFax = objInput.PracticeFax;
                                    _UserInfo1.PracticeCity = objInput.City;
                                    _UserInfo1.PracticeState = objInput.State;
                                    _UserInfo1.PracticeZip = objInput.State;


                                    _UserInfo1.ProviderId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProviderId"].ToString());
                                    int j = oUser1.insertLocationDetails(_UserInfo1);

                                    dslocation = oUser1.getLocationDetails(objInput.PracticeTin);
                                    iLocationID = Convert.ToInt32(dslocation.Tables[0].Rows[0]["LocationId"].ToString());
                                }
                                else
                                {
                                    iLocationID = Convert.ToInt32(ds.Tables[0].Rows[0]["LocationId"].ToString());
                                }

                            }
                        }
                    }

                    if (iPracticeID > 0 && iLocationID > 0)
                    {
                        _UserInfo1.PhysicianId = 0;
                        _UserInfo1.ProviderId = iPracticeID;
                        _UserInfo1.PhyOfficeName = objInput.PracticeName;
                        _UserInfo1.fname = objInput.FirstName;
                        _UserInfo1.lname = objInput.LastName;
                        _UserInfo1.UserType = 2;
                        _UserInfo1.UserRole = 2;
                        _UserInfo1.EmailId = objInput.EmailAddress;
                        _UserInfo1.Password = objInput.Password;
                        _UserInfo1.EmailAddress = objInput.EmailAddress;
                        _UserInfo1.DirectEmail = "";
                        _UserInfo1.practicephone = objInput.PracticePhone;
                        _UserInfo1.LocationIds = iLocationID.ToString();
                        _UserInfo1.SecurityQuestion1 = objInput.SecurityQuestion1;
                        _UserInfo1.SecurityQuestion2 = objInput.SecurityQuestion2;
                        _UserInfo1.SecurityQuestion3 = objInput.SecurityQuestion3;
                        _UserInfo1.SecurityAnswer1 = objInput.SecurityAnswer1;
                        _UserInfo1.SecurityAnswer2 = objInput.SecurityAnswer2;
                        _UserInfo1.SecurityAnswer3 = objInput.SecurityAnswer3;
                        UserInfo info1 = oUser1.getUserDetailsByEmailId(objInput.EmailAddress, _UserInfo1.pin);
                        if (info1.UserId > 0)
                        {
                            return "User already exists";
                        }
                        else
                        {
                            int success = oUser1.insertuserfromEnrollment(_UserInfo1);
                            return "Enrollment Sucessful";
                        }

                    }
                    return "Unable to enroll. please try again later";
                }
                else
                {
                    return "Pin is a required field";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}