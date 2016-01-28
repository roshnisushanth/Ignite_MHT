using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace IgniteServices
{
    [ServiceContract]
    public interface IIgniteService
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getappsettings",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        string GetAppSetting(Stream message);

        // TODO: Add your service operations here
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "validateuserlogin",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        UserDetails[] ValidateUserLogin(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "forgotpassword",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        UserDetails[] ForgotPassword(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "updatepassword",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        string UpdatePassword(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "deletemessage",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        string DeleteMessage(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "permenantlydeletemessage",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        string PermanentlyDeleteMessage(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "patientdetails",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        PatientDetails[] GetPatientDetails(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "patientlookup",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        PatientDetails[] GetPatientLookup(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "allergies",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        Allergies[] GetAllergies(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "conditions",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        Conditions[] GetMedicalHistory(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "updateconditions",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        string UpdateMedicalHistory(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "updatemedications",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        string UpdateMedications(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "updateallergies",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        string UpdateAllergies(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "medications",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        Medication[] GetMedications(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "emergencycontact",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        EmergencyContact[] GetEmergencyContact(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "mymessages",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        MessageBox[] MyMessages(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getattachment",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        MessageBox[] GetAttachment(Stream message);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "sentmessages",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare)]
        MessageBox[] SentMessages(Stream message);
        [WebInvoke(Method = "POST", UriTemplate = "deletemessages",
              RequestFormat = WebMessageFormat.Json,
              ResponseFormat = WebMessageFormat.Json,
              BodyStyle = WebMessageBodyStyle.Bare)]
        MessageBox[] DeletedMessages(Stream message);
        [WebInvoke(Method = "POST", UriTemplate = "composereply",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare)]
        string ComposeReply(Stream message);

        [WebInvoke(Method = "POST", UriTemplate = "updatemessage",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare)]
        string UpdateMessage(Stream message);
        [WebInvoke(Method = "POST", UriTemplate = "gettofield",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare)]
        ToFields[] GetToField(Stream message);

        [WebInvoke(Method = "POST", UriTemplate = "enroll",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare)]
        string Enroll(Stream message);
    }

    [DataContract]
    public class ToFields
    {
        [DataMember]
        public string Pin { get; set; }
        [DataMember]
        public string ToId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string FromEmail { get; set; }
        [DataMember]
        public string ToEmail { get; set; }
        [DataMember]
        public string UserType { get; set; }
        [DataMember]
        public string MessengerMessage { get; set; }
    }
    [DataContract]
    public class ComposeMessage
    {
        [DataMember]
        public string Pin { get; set; }
        [DataMember]
        public int FromId { get; set; }
        [DataMember]
        public int FromType { get; set; }
        [DataMember]
        public int MessageSignatureId { get; set; }
        [DataMember]
        public int UserType { get; set; }
        [DataMember]
        public int ToId { get; set; }
        [DataMember]
        public int ToType { get; set; }
        [DataMember]
        public string ToEmailId { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public int MessageRepliedTo { get; set; }
        [DataMember]
        public int MessageId { get; set; }
        [DataMember]
        public string[] Attachment { get; set; }
        [DataMember]
        public string[] Attachmentextension { get; set; }
        [DataMember]
        public string LocationCoordinates { get; set; }
        [DataMember]
        public int PhysicianId { get; set; }
        [DataMember]
        public string MessengerMessage { get; set; }
    }

    [DataContract]
    public class Enrollment
    {
        [DataMember]
        public string PracticeTin { get; set; }
        [DataMember]
        public string PracticeName { get; set; }
        [DataMember]
        public string LocationName { get; set; }
        [DataMember]
        public string Pin { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string OrganizationType { get; set; }
        [DataMember]
        public string PracticePhone { get; set; }
        [DataMember]
        public string PracticeFax { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Address2 { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Zip { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string SecurityQuestion1 { get; set; }
        [DataMember]
        public string SecurityAnswer1 { get; set; }
        [DataMember]
        public string SecurityQuestion2 { get; set; }
        [DataMember]
        public string SecurityAnswer2 { get; set; }
        [DataMember]
        public string SecurityQuestion3 { get; set; }
        [DataMember]
        public string SecurityAnswer3 { get; set; }
        [DataMember]
        public string MessengerMessage { get; set; }
    }

    [DataContract]
    public class MessageBox
    {
        [DataMember]
        public int MessageComposeId { get; set; }
        [DataMember]
        public int FromId { get; set; }
        [DataMember]
        public int ToId { get; set; }
        [DataMember]
        public string FromEmailId { get; set; }
        [DataMember]
        public string ToEmailId { get; set; }
        [DataMember]
        public int MessageId { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string[] Attachment { get; set; }
        [DataMember]
        public string[] AttachmentExtension { get; set; }
        [DataMember]
        public int FromType { get; set; }
        [DataMember]
        public int ToType { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public DateTime ProcessDate { get; set; }
        [DataMember]
        public string FromName { get; set; }
        [DataMember]
        public int userType { get; set; }
        [DataMember]
        public int physicianId { get; set; }
        [DataMember]
        public int MessageRepliedTo { get; set; }
        [DataMember]
        public bool IsMessageRead { get; set; }
        [DataMember]
        public int MessageSignatureId { get; set; }
        [DataMember]
        public string MessageSignatureName { get; set; }
        [DataMember]
        public string MessageSignatureOffcName { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string LocationCoordinates { get; set; }
        [DataMember]
        public string Pin { get; set; }
        [DataMember]
        public string MessengerMessage { get; set; }
        [DataMember]
        public string GetAttachment { get; set; }
        [DataMember]
        public int AttachmentIndex { get; set; }
    }

    [DataContract]
    public class PatientDetails
    {
        [DataMember]
        public int PatientId { get; set; }
        [DataMember]
        public string PhysicianId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string DOB { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Pin { get; set; }
        [DataMember]
        public string MessengerMessage { get; set; }
    }

    [DataContract]
    public class Allergies
    {
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public int AllergyID { get; set; }
        [DataMember]
        public string AllergyType { get; set; }
        [DataMember]
        public string Reaction { get; set; }
        [DataMember]
        public string DateLastOccured { get; set; }
        [DataMember]
        public string Treatment { get; set; }
        [DataMember]
        public string IsMedicationAllergy { get; set; }
        [DataMember]
        public string Pin { get; set; }
        [DataMember]
        public string MessengerMessage { get; set; }
    }

    [DataContract]
    public class Conditions
    {
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public int ConditionID { get; set; }
        [DataMember]
        public string Condition { get; set; }
        [DataMember]
        public string ConditionCheck { get; set; }
        [DataMember]
        public string DateOfOnset { get; set; }
        [DataMember]
        public string History { get; set; }
        [DataMember]
        public string ICDCode { get; set; }
        [DataMember]
        public string Pin { get; set; }
        [DataMember]
        public string MessengerMessage { get; set; }
    }

    [DataContract]
    public class Medication
    {
        [DataMember]
        public string Pin { get; set; }
        [DataMember]
        public int MedicationID { get; set; }
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Dosage { get; set; }
        [DataMember]
        public string Units { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        public string MessengerMessage { get; set; }
    }

    [DataContract]
    public class EmergencyContact
    {
        [DataMember]
        public string Pin { get; set; }
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public string PrimaryEmergencyFirstName { get; set; }
        [DataMember]
        public string PrimaryEmergencyLastName { get; set; }
        [DataMember]
        public string PrimaryEmergencyRelationship { get; set; }
        [DataMember]
        public string PrimaryEmergencyAddress1 { get; set; }
        [DataMember]
        public string PrimaryEmergencyAddress2 { get; set; }
        [DataMember]
        public string PrimaryEmergencyCity { get; set; }
        [DataMember]
        public string PrimaryEmergencyState { get; set; }
        [DataMember]
        public string PrimaryEmergencyCellPhone { get; set; }
        [DataMember]
        public string PrimaryEmergencyHomePhone { get; set; }
        [DataMember]
        public string PrimaryEmergencyWorkPhone { get; set; }

        [DataMember]
        public string SecondaryEmergencyFirstName { get; set; }
        [DataMember]
        public string SecondaryEmergencyLastName { get; set; }
        [DataMember]
        public string SecondaryEmergencyRelationship { get; set; }
        [DataMember]
        public string SecondaryEmergencyAddress1 { get; set; }
        [DataMember]
        public string SecondaryEmergencyAddress2 { get; set; }
        [DataMember]
        public string SecondaryEmergencyCity { get; set; }
        [DataMember]
        public string SecondaryEmergencyState { get; set; }
        [DataMember]
        public string SecondaryEmergencyCellPhone { get; set; }
        [DataMember]
        public string SecondaryEmergencyHomePhone { get; set; }
        [DataMember]
        public string SecondaryEmergencyWorkPhone { get; set; }
        [DataMember]
        public string MessengerMessage { get; set; }
    }

    [DataContract]
    public class UserDetails
    {
        [DataMember]
        public int PhysicianId { get; set; }
        [DataMember]
        public string Pin { get; set; }
        [DataMember]
        public string IsSuperUser { get; set; }
        [DataMember]
        public string IsAdmin { get; set; }
        [DataMember]
        public string IsStaff { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public int UserType { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string MessengerMessage { get; set; }
        [DataMember]
        public string SecurityQuestion1 { get; set; }
        [DataMember]
        public string SecurityQuestion2 { get; set; }
        [DataMember]
        public string SecurityQuestion3 { get; set; }
        [DataMember]
        public string SecurityAnswer1 { get; set; }
        [DataMember]
        public string SecurityAnswer2 { get; set; }
        [DataMember]
        public string SecurityAnswer3 { get; set; }
        [DataMember]
        public string Application { get; set; }
    }




    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }

    }
}