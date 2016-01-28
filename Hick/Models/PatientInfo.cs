using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hick.Models
{
    public class PatientInfo
    {
        public long PatientId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DOB { get; set; }
        private string _Phone;
        public string Phone
        {
            get
            {
                return String.IsNullOrEmpty(_Phone) ? "NA" : _Phone;

            }
            set
            {
                _Phone = value;
            }
        }

        private string _SSN;
        public string SSN
        {
            get
            {
                return String.IsNullOrEmpty(_SSN) ? "NA" : _SSN;

            }
            set
            {
                _SSN = value;
            }
        }
        private string _RiskProfile;
        public string RiskProfile
        {
            get
            {
                return String.IsNullOrEmpty(_RiskProfile) ? "NA" : _RiskProfile;

            }
            set
            {
                _RiskProfile = value;
            }
        }

    }
    public class Conditons
    {
        public long PatientID { get; set; }
        public long ConditionID { get; set; }
        public string Condition { get; set; }
        public string ConditionCheck { get; set; }
        public string ConditionStatus { get; set; }
        public string ConditionName { get; set; }
        public string DateOfOnset { get; set; }
        public string History { get; set; }
        public string ICDCode { get; set; }
        public string OtherInfo { get; set; }
        public string Pin { get; set; }
    }
    public class Medications
    {
        public long PatientID { get; set; }
        public long MedicationID { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Dosage { get; set; }
        public string Pin { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string Units { get; set; }
        public string HowTaken { get; set; }
        public string DateStopped { get; set; }
        public string DosageUnits { get; set; }
    }

    public class Allergy
    {
        public long PatientID { get; set; }
        public long AllergyID { get; set; }
        public string AllergyType { get; set; }
        public string Reaction { get; set; }
        public string DateLastOccured { get; set; }
        public string Treatment { get; set; }
        public string IsMedicationAllergy { get; set; }
        public string Pin { get; set; }
        public string DateObserved { get; set; }
    }

    public class FamilyHistory
    {
        public long PatientID { get; set; }
        public long FamilyHistoryId { get; set; }
        public long UserId { get; set; }
        public string Mother { get; set; }
        public string Father { get; set; }
        public string Siblings { get; set; }
        public string Pin { get; set; }
        public int NoOfSibling { get; set; }
        public string Children { get; set; }
        public int NoOfChildren { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class LifeStyle
    {
        public long PatientID { get; set; }
        public long LifestyleId { get; set; }
        public long UserId { get; set; }
        public string Alcoholic { get; set; }
        public string Smoke { get; set; }
        public string Exercise { get; set; }
        public string DrinksPerWeek { get; set; }
        public string HowlongYearsDrinking { get; set; }
        public string PacksPerDay { get; set; }
        public string HowlongYearsSmoking { get; set; }
        public string TypeOfExercise { get; set; }
        public string ExerciseDaysPerWeek { get; set; }
        public string OtherInfo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Pin { get; set; }
    }

    public class Immunizations
    {
        public long PatientID { get; set; }
        public long ImmunizationID { get; set; }
        public string ImmunizationType { get; set; }
        public DateTime AdministrationDate { get; set; }
        public string Notes { get; set; }
        public string AdverseEvent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Pin { get; set; }
    }

    public class PatientDetails
    {

        public int PatientId { get; set; }
        public long PatId { get; set; }

        public string PhysicianId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DOB { get; set; }

        public string Phone { get; set; }

        public string EmailId { get; set; }

        public string Gender { get; set; }

        public string Age { get; set; }

        public string Height { get; set; }

        public string Weight { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string BP { get; set; }

        public string OrginalBP { get; set; }

        public string BMI { get; set; }
        public string ICDCode { get; set; }

        public string MiddleName { get; set; }

        public string MaidenName { get; set; }

        public string BloodType { get; set; }

        public string Last4SSN { get; set; }

        public string HICN { get; set; }

        public string Race { get; set; }

        public string PhoneNumber { get; set; }

        public string WorkPhone { get; set; }

        public string CellPhone { get; set; }

        public string Email { get; set; }

        public string Zipcode { get; set; }
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string AlterAddress1 { get; set; }

        public string AlterAddress2 { get; set; }

        public string EyeColor { get; set; }

        public string HairColor { get; set; }

        public string Birthmark { get; set; }

        public string PrimaryFinancialClass { get; set; }

        public string PrimaryInsuranceCompany { get; set; }

        public string SecondaryFinancialClass { get; set; }

        public string SecondryCompanyName { get; set; }

        public string PrimaryPolicyNumber { get; set; }

        public string SecondaryPolicyNumber { get; set; }

        public string SecondaryGroupNumber { get; set; }


        public string PrimaryHealthInsurance { get; set; }

        public string SecondaryHealthInsurance { get; set; }

        public string OtherInfo { get; set; }


        public string Ins1GroupNo { get; set; }

        public string Ins2GroupNo { get; set; }

        public string ManagedBP { get; set; }

        public string OriginalPolicyNumber { get; set; }

        public string ManagedPolicyNumber { get; set; }

        public string PrimarySpecify { get; set; }

        public string SecondarySpecify { get; set; }

        public string SelfPay { get; set; }

        public string Pin { get; set; }

        public string MessengerMessage { get; set; }

        public string ReferredByPhyscianOfficeName { get; set; }

        public string AssignedPhyscianFirstName { get; set; }

        public string AssignedPhyscianLastName { get; set; }

        public string ReferredPhyscianFirstName { get; set; }

        public string ReferredPhyscianLastName { get; set; }

        public string PhyOfficeName { get; set; }

        public string CreatedDate { get; set; }

        public string ApptDateTimePref1 { get; set; }

        public string CpdCode { get; set; }

        public string AppSuite { get; set; }

        public string ApptDateTimePref2 { get; set; }

        public string ApptDateTimePref3 { get; set; }

        public string AddServiceComments { get; set; }

        public string ClinicalDocPath { get; set; }

        public string Problems { get; set; }

        public string Medications { get; set; }

        public string ReferralTransmissionMeth { get; set; }

        public string RefMethodNotes { get; set; }

        public string AddAppNotes { get; set; }

        public string ApptTime_Pref2 { get; set; }

        public string ApptTime_Pref1 { get; set; }

        public string ApptTime_Pref3 { get; set; }

        public int Status { get; set; }

        public string AssignedPhysician { get; set; }
    }
    //public class PatientDetails
    //{
    //    public int PatientId { get; set; }
    //    public string LastName { get; set; }
    //    public string FirstName { get; set; }
    //    public string DOB { get; set; }
    //    public string MiddleName { get; set; }
    //    public string MaidenName { get; set; }
    //    public string Last4SSN { get; set; }
    //    public string Gender { get; set; }
    //    public string HomePhone { get; set; }
    //    public string WorkPhone { get; set; }
    //    public string CellPhone { get; set; }
    //    public string HICN { get; set; }
    //    public string Email { get; set; }
    //    public string Height { get; set; }
    //    public string Weight { get; set; }
    //    public string Age { get; set; }
    //    public string Race { get; set; }
    //    public string Maritalstatus { get; set; }
    //    public string Address1 { get; set; }
    //    public string Address2 { get; set; }
    //    public string City { get; set; }
    //    public string State { get; set; }
    //    public string Zip { get; set; }
    //    public string AlterAddress1 { get; set; }
    //    public string AlterAddress2 { get; set; }
    //    public string AlterCity { get; set; }
    //    public string AlterState { get; set; }
    //    public string AlterZip { get; set; }
    //    public string EyeColor { get; set; }
    //    public string HairColor { get; set; }
    //    public string BloodType { get; set; }
    //    public string Birthmark { get; set; }
    //    public string ReligiousPreferences { get; set; }
    //    public string Specialconditions { get; set; }
    //    public string PrimaryFinancialClass { get; set; }
    //    public string PrimaryCompanyName { get; set; }
    //    public string OrginalPolicyNumber { get; set; }
    //    public string PrimaryGroupNumber { get; set; }
    //    public string PrimaryHealthInsurance { get; set; }
    //    public string SecondaryHealthInsurance { get; set; }
    //    public string SecondaryFinancialClass { get; set; }
    //    public string SecondryCompanyName { get; set; }
    //    public string SecondaryPolicyNumber { get; set; }
    //    public string SecondaryGroupNumber { get; set; }
    //    public string BP { get; set; }
    //    public string BMI { get; set; }
    //    public string OrginalBP { get; set; }
    //    public string Pin { get; set; }


    //}

    public class EmergencyContact
    {
        public int PatientID { get; set; }
        public string Pin { get; set; }
        public string PrimaryEmergencyFirstName { get; set; }
        public string PrimaryEmergencyLastName { get; set; }
        public string PrimaryEmergencyRelationship { get; set; }
        public string PrimaryEmergencyAddress1 { get; set; }
        public string PrimaryEmergencyAddress2 { get; set; }
        public string PrimaryEmergencyCity { get; set; }
        public string PrimaryEmergencyState { get; set; }
        public string PrimaryEmergencyCellPhone { get; set; }
        public string PrimaryEmergencyHomePhone { get; set; }
        public string PrimaryEmergencyWorkPhone { get; set; }
        public string SecondaryEmergencyFirstName { get; set; }
        public string SecondaryEmergencyLastName { get; set; }

        public string SecondaryEmergencyRelationship { get; set; }

        public string SecondaryEmergencyAddress1 { get; set; }

        public string SecondaryEmergencyAddress2 { get; set; }

        public string SecondaryEmergencyCity { get; set; }

        public string SecondaryEmergencyState { get; set; }

        public string SecondaryEmergencyCellPhone { get; set; }

        public string SecondaryEmergencyHomePhone { get; set; }

        public string SecondaryEmergencyWorkPhone { get; set; }
        public string PrimaryEmergencyEmailID { get; set; }
        public string SecondaryEmergencyEmailID { get; set; }
    }

    public class CreatePatient
    {
        public int PatientID { get; set; }
        public int PhyscianID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DOB { get; set; }
        public string MiddleName { get; set; }
        public string MaidenName { get; set; }
        public string Last4SSN { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string Maritalstatus { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string AlterAddress1 { get; set; }
        public string AlterAddress2 { get; set; }
        public string AlterCity { get; set; }
        public string AlterState { get; set; }
        public string AlterZip { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public string BloodType { get; set; }
        public string Birthmark { get; set; }
        public string ReligiousPreferences { get; set; }
        public string Specialconditions { get; set; }
        public string FinancialClass { get; set; }
        public string CompanyName { get; set; }
        public string PolicyNumber { get; set; }
        public string GroupNumber { get; set; }
        public string SecondaryFinancialClass { get; set; }
        public string SecondryCompanyName { get; set; }
        public string SecondaryPolicyNumber { get; set; }
        public string SecondaryGroupNumber { get; set; }
    }

    public class HealthProvider
    {
        public int UserId { get; set; }
        public int HealthcareProvidersId { get; set; }
        public string ProviderName { get; set; }
        public string pcp { get; set; }
        public string Speciality { get; set; }
        public string Phone { get; set; }
        public string EmergencyPhone { get; set; }
        public string Email { get; set; }
        public string OtherInfo { get; set; }
        public string UserType { get; set; }
        public string PracticeName { get; set; }
        public string Pin { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class PatientClinicalQuesRespInformation
    {

        public int PatientID { get; set; }

        public int UserId { get; set; }

        public int RespId { get; set; }

        public string QCode { get; set; }

        public string QType { get; set; }

        public string QTypeDesc { get; set; }

        public string FactorDesc { get; set; }

        public string FactorText { get; set; }

        public string FactorValue { get; set; }

        public string OtherInfo { get; set; }

        public string UserType { get; set; }

        public string ActionItem { get; set; }

        public string Pin { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }

    public class Activities
    {


        public int PatientID { get; set; }

        public int UserId { get; set; }

        public int ActivitiesId { get; set; }

        public string Bathing { get; set; }

        public string Dressing { get; set; }

        public string Eating { get; set; }

        public string UsingtheBathroom { get; set; }

        public string Walking { get; set; }

        public string PreparingMeals { get; set; }

        public string TakingMedications { get; set; }

        public string OtherInfo { get; set; }

        public string UserType { get; set; }

        public string Pin { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }

    public class PreventiveInfo
    {
        public int PatientID { get; set; }

        public int UserId { get; set; }

        public int PreventiveId { get; set; }

        public string BoneMassMeasurement { get; set; }

        public string ColorectalScreening { get; set; }

        public string MammographyScreening { get; set; }

        public string PapTest { get; set; }

        public string ProstateCancer { get; set; }

        public string Pneumonia { get; set; }

        public string FluShot { get; set; }

        public string HepatitisB { get; set; }

        public string OtherInfo { get; set; }

        public string UserType { get; set; }

        public string Pin { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
    public class FamilyHistoryDetailInfo
    {

        public string Pin { get; set; }

        public int FamilyHistoryId { get; set; }

        public int UserId { get; set; }

        public int PatientID { get; set; }

        public string Note { get; set; }

        public string SlNo { get; set; }

        public string MedicalHistoryOf { get; set; }

        public string Gender { get; set; }

        public string LivingOrDeceased { get; set; }

        public string AgeAtDeath { get; set; }


        public string CauseOfDeath { get; set; }

        public int AgeOfLiving { get; set; }

        public string MajorHealthProblem { get; set; }


        public string HealthProblemCancer { get; set; }

        public string HealthProblemDiabetes { get; set; }

        public string HealthProblemStroke { get; set; }


        public string HealthProblemHeartAttack { get; set; }

        public string HealthProblemHypertension { get; set; }

        public string HealthProblemHyperlipidemia { get; set; }



        public string HealthProblemNone { get; set; }

        public string HealthProblemOther { get; set; }




        public int PatientFamilyHistoryID { get; set; }

        public string ConditionName { get; set; }

        public string OnsetDate { get; set; }


        public string Relationship { get; set; }

        public string ConditionCheck { get; set; }



        public string CreatedDate { get; set; }

        public string ModifiedDate { get; set; }

        public string MessengerMessage { get; set; }
    }

    public class HealthLogInfo
    {


        public int PatientID { get; set; }

        public int UserId { get; set; }

        public int HealthLogId { get; set; }

        public int MedicationId { get; set; }

        public int DoctorVisitId { get; set; }

        public int HospitalizationsId { get; set; }

        public string DiagnosesCode { get; set; }

        public DateTime DiagnosedDate { get; set; }

        public string Doctor { get; set; }

        public string NatureofHealthProblems { get; set; }

        public string Diagnoses { get; set; }

        public int AgeatOnset { get; set; }

        public string ConditionStatus { get; set; }

        public string Remarks { get; set; }

        public DateTime MedicationsDate { get; set; }

        public string MedicationDosage { get; set; }

        public string Frequency { get; set; }

        public string HospitalizationType { get; set; }

        public string HospitalName { get; set; }

        public DateTime AdmitDate { get; set; }

        public DateTime DischargeDate { get; set; }

        public string HospitalDoctor { get; set; }

        public string Reason { get; set; }

        public string Diagnosis { get; set; }

        public string Complications { get; set; }

        public DateTime Visitdate { get; set; }

        public string DoctorName { get; set; }

        public string VisitReason { get; set; }

        public string VisitDiagnosis { get; set; }

        public string HCC { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int UserType { get; set; }

        public string Pin { get; set; }

        public string MessengerMessage { get; set; }

        public DateTime DosFrom { get; set; }

        public DateTime DosThrough { get; set; }

        public string POT { get; set; }

        public string RiskFactor { get; set; }
    }

    public class Surgeries
    {


        public int PatientID { get; set; }

        public int UserId { get; set; }

        public int SurgeriesId { get; set; }

        public string Date { get; set; }

        public string Doctor { get; set; }

        public string Hospital { get; set; }

        public string SurgeriesProcedure { get; set; }

        public string Description { get; set; }

        public string Results { get; set; }

        public string Comments { get; set; }

        public string OtherInfo { get; set; }

        public string UserType { get; set; }

        public string Pin { get; set; }

        public string MessengerMessage { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }

    public class LabImaging
    {


        public int PatientID { get; set; }

        public int UserId { get; set; }

        public int LabImagingId { get; set; }

        public string Date { get; set; }

        public string TestType { get; set; }

        public string RequestingDoctor { get; set; }

        public string AdministeredBy { get; set; }

        public string Reason { get; set; }

        public string Results { get; set; }

        public string OtherInfo { get; set; }

        public string UserType { get; set; }

        public string Pin { get; set; }

        public string MessengerMessage { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }

    public class MedicalDevices
    {
        public int PatientID { get; set; }

        public int UserId { get; set; }

        public int MedicalDevicesId { get; set; }

        public string DeviceType { get; set; }

        public string Doctor { get; set; }

        public string Hospital { get; set; }

        public string Date { get; set; }

        public string Reason { get; set; }

        public string OtherInfo { get; set; }

        public string UserType { get; set; }

        public string Pin { get; set; }

        public string MessengerMessage { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }


    public class PhysicalTherapy
    {
        
        public int PatientID { get; set; }
        
        public int UserId { get; set; }
        
        public int PhysicalTherapyId { get; set; }
        
        public string TherapyType { get; set; }
        
        public string StartDate { get; set; }
        
        public string StopDate { get; set; }
        
        public string Frequency { get; set; }
        
        public string Therapist { get; set; }
        
        public string OtherInfo { get; set; }
        
        public string UserType { get; set; }
        
        public string Pin { get; set; }
        
        public string MessengerMessage { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime ModifiedDate { get; set; }
    }

    public class ICDconditions
    {
        //conditions
        public int COPD { get; set; }

        public int CAD { get; set; }

        public int HTN { get; set; }

        public int IVD { get; set; }

        public int CHF { get; set; }

        public int DM { get; set; }

        public string Pin { get; set; }
    }

    public class CCMConditions
    {
        //conditions
        public int Current { get; set; }

        public int Dropped { get; set; }

        public int Deceased { get; set; }

        public int InActive { get; set; }

        

        public string Pin { get; set; }
    }
    public class Referral
    {

        public int PatientID { get; set; }

        public int PhysicianID { get; set; }

        public int ReferralID { get; set; }

        public int ReferralPhysicianID { get; set; }

        public string Servicesrequred { get; set; }

        public DateTime CreatedDate { get; set; }

        public string PatientStatus { get; set; }

        public string Service { get; set; }

        public int ServiceID { get; set; }

        public string OtherService { get; set; }

        public int Status { get; set; }

        public DateTime AppDatePref1 { get; set; }

        public string ApptDateTimePref1 { get; set; }

        public string Pin { get; set; }

        public string MessengerMessage { get; set; }

        public string AssignedPhysician { get; set; }

        public string ReferredBy { get; set; }

        public string StatusText { get; set; }


    }
    public class PhysicianDetails
    {

        public int PatientID { get; set; }

        public int PhysicianID { get; set; }


        public int ReferralID { get; set; }

        public int FProviderId { get; set; }

        public int UserType { get; set; }

        public int ProviderId { get; set; }

        public int OrgTypeId { get; set; }

        public string OrgTypeDescription { get; set; }

        public string NPI { get; set; }

        public string PhyOfficeName { get; set; }

        public string Pin { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string UserRole { get; set; }

        public string ResourceName { get; set; }

        public string ContactPerson { get; set; }

        public string ContactOfficePhNo { get; set; }

        public string EmailId { get; set; }

        public string ContactPhNo { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zipcode { get; set; }

        public string FrindlyName { get; set; }

        public string TempLogin { get; set; }

        public string TempPassword { get; set; }

        public string TempPasswordSalt { get; set; }

        public string SecurityQuestion { get; set; }

        public string SecurityAnswer { get; set; }

        public bool IsApproved { get; set; }

        public bool IsLockedAccount { get; set; }

        public string FaxNo { get; set; }

        public string MessengerMessage { get; set; }

    }
    public class VitalSignsInfo
    {

        public int PatientID { get; set; }

        public int UserId { get; set; }

        public int VitalSignsID { get; set; }

        public string Height { get; set; }

        public string HeightUnits { get; set; }

        public string Weight { get; set; }

        public string WeightUnits { get; set; }

        public string Temperature { get; set; }

        public string TemperatureUnit { get; set; }

        public string Pulse { get; set; }

        public string Respiration { get; set; }

        public string BloodPressure { get; set; }

        public string Status { get; set; }

        public string FromDate { get; set; }


        public string ToDate { get; set; }


        public string Pin { get; set; }

        public string MessengerMessage { get; set; }

        public string CreatedDate { get; set; }

    }
    public class ICDCodes
    {



        public string ICD { get; set; }


        public string Pin { get; set; }
    }
}