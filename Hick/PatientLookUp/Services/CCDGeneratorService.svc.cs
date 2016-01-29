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
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;
using CCD;
namespace Hick.CCDGenerator
{
    public class CCDGeneratorService
    {

        private static string Serialize<T>(T value)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(value.GetType());
            StringWriter textWriter = new StringWriter();


            xmlSerializer.Serialize(textWriter, value);
            return textWriter.ToString();
        }
        string BMI = "0";

        public string GenerateCCD(int patientid, string firstName,string lastName)
        {
            List<PatientDetails> dicpatientdetails = null;
            if (patientid != 0)
            {

                
                var uri = Utility.GetServiceUrl("getpatientdetailsbypatientid");
                IgJObject postData = new IgJObject();
                postData.Add("PatientId", patientid);
                dicpatientdetails = Utility.PostRequest<PatientDetails>(uri, postData.ToString(Formatting.None));

                if(dicpatientdetails[0].BMI.ToString().Length>0)
                {
                    BMI = dicpatientdetails[0].BMI;
                }
              

                POCD_MT000040ClinicalDocument ccd = new POCD_MT000040ClinicalDocument();
        
      
                ccd.id = new II();
                ccd.id.extension = ConfigurationManager.AppSettings["id_extension"].ToString();
                ccd.id.root = ConfigurationManager.AppSettings["id_root"].ToString();
            

                ccd.code = new CE();
                ccd.code.code = "11488-4";
                ccd.code.codeSystem = ConfigurationManager.AppSettings["code_codesystem"].ToString();
                ccd.code.displayName = "Consultation note";

                ccd.title = new ST();
                ccd.title.Text = new string[1];
                ccd.title.Text[0] = "Good Health Clinic Consultation Note";
                ccd.effectiveTime = new TS();
                ccd.effectiveTime.value = DateTime.UtcNow.ToString();

                ccd.confidentialityCode = new CE();
                ccd.confidentialityCode.code = "N";
                ccd.confidentialityCode.codeSystem = ConfigurationManager.AppSettings["confidentialityCode_codesystem"].ToString();

                ccd.setId = new II();
                ccd.setId.extension = "BB35";
                ccd.setId.root= ConfigurationManager.AppSettings["id_root"].ToString();

                ccd.versionNumber = new INT();
                ccd.versionNumber.value = "2";

                ccd.legalAuthenticator = new POCD_MT000040LegalAuthenticator();
                ccd.legalAuthenticator.time = new TS();
                ccd.legalAuthenticator.time.value = DateTime.UtcNow.ToString();
                ccd.legalAuthenticator.signatureCode = new CS();
                ccd.legalAuthenticator.signatureCode.code = "s";
                ccd.legalAuthenticator.assignedEntity = new POCD_MT000040AssignedEntity();
                ccd.legalAuthenticator.assignedEntity.id = new II[1];
                ccd.legalAuthenticator.assignedEntity.id[0] = new II();
                ccd.legalAuthenticator.assignedEntity.id[0].extension = "KP00017";
                ccd.legalAuthenticator.assignedEntity.id[0].root= ConfigurationManager.AppSettings["id_root"].ToString();  
                ccd.legalAuthenticator.assignedEntity.assignedPerson = new POCD_MT000040Person();
                ccd.legalAuthenticator.assignedEntity.assignedPerson.name = new PN[1];
                ccd.legalAuthenticator.assignedEntity.assignedPerson.name[0] = new PN();
                CCD.engiven given = new engiven();
                given.Text = new string[1];
                given.Text[0] = firstName;

                CCD.enfamily family = new enfamily();
                family.Text = new string[1];
                family.Text[0] = lastName;

                CCD.ensuffix suffix = new ensuffix();
                suffix.Text = new string[1];
                suffix.Text[0] = "";
                ccd.legalAuthenticator.assignedEntity.assignedPerson.name[0].Items = new ENXP[3];
                ccd.legalAuthenticator.assignedEntity.assignedPerson.name[0].Items[0] = given;
                ccd.legalAuthenticator.assignedEntity.assignedPerson.name[0].Items[1] = family;
                ccd.legalAuthenticator.assignedEntity.assignedPerson.name[0].Items[2] = suffix;
                ccd.legalAuthenticator.assignedEntity.representedOrganization = new POCD_MT000040Organization();
                ccd.legalAuthenticator.assignedEntity.representedOrganization.id = new II[1];
                ccd.legalAuthenticator.assignedEntity.representedOrganization.id[0] = new II();
                ccd.legalAuthenticator.assignedEntity.representedOrganization.id[0].extension = "M345";
                ccd.legalAuthenticator.assignedEntity.representedOrganization.id[0].root = ConfigurationManager.AppSettings["id_root"].ToString();

                ccd.author = new POCD_MT000040Author[1];
                ccd.author[0] = new POCD_MT000040Author();
                ccd.author[0].time = new TS();
                ccd.author[0].time.value = "20000407";
                ccd.author[0].assignedAuthor = new POCD_MT000040AssignedAuthor();
                ccd.author[0].assignedAuthor.id = new II[1];
                ccd.author[0].assignedAuthor.id[0] = new II();
                ccd.author[0].assignedAuthor.id[0].extension = "KP00017";
                ccd.author[0].assignedAuthor.id[0].root= ConfigurationManager.AppSettings["id_root"].ToString();
                ccd.author[0].assignedAuthor.assignedPerson = new POCD_MT000040Person();
                ccd.author[0].assignedAuthor.assignedPerson.name= new PN[1];
                ccd.author[0].assignedAuthor.assignedPerson.name[0] = new PN();
                ccd.author[0].assignedAuthor.assignedPerson.name[0].Items = new ENXP[3];
                ccd.author[0].assignedAuthor.assignedPerson.name[0].Items[0] =given;
                ccd.author[0].assignedAuthor.assignedPerson.name[0].Items[1] = family;
                ccd.author[0].assignedAuthor.assignedPerson.name[0].Items[2] = suffix;
                ccd.author[0].assignedAuthor.representedOrganization = new POCD_MT000040Organization();
                ccd.author[0].assignedAuthor.representedOrganization.id = new II[1];
                ccd.author[0].assignedAuthor.representedOrganization.id[0] = new II();
                ccd.author[0].assignedAuthor.representedOrganization.id[0].extension = "M345";
                ccd.author[0].assignedAuthor.representedOrganization.id[0].root = ConfigurationManager.AppSettings["id_root"].ToString();

                ccd.custodian = new POCD_MT000040Custodian();
                ccd.custodian.assignedCustodian = new POCD_MT000040AssignedCustodian();
                ccd.custodian.assignedCustodian.representedCustodianOrganization = new POCD_MT000040CustodianOrganization();
                ccd.custodian.assignedCustodian.representedCustodianOrganization.id = new II[1];
                ccd.custodian.assignedCustodian.representedCustodianOrganization.id[0] = new II();
                ccd.custodian.assignedCustodian.representedCustodianOrganization.id[0].extension = "M345";
                ccd.custodian.assignedCustodian.representedCustodianOrganization.id[0].root= ConfigurationManager.AppSettings["id_root"].ToString();
               ccd.custodian.assignedCustodian.representedCustodianOrganization.name = new ON();
                ccd.custodian.assignedCustodian.representedCustodianOrganization.name.Text = new string[1];
                ccd.custodian.assignedCustodian.representedCustodianOrganization.name.Text[0] = "Good Health Clinic";




                ccd.recordTarget = new POCD_MT000040RecordTarget();
                ccd.recordTarget.patientRole = new POCD_MT000040PatientRole();

                ccd.recordTarget.patientRole.id = new II[1];
                ccd.recordTarget.patientRole.id[0] = new II();
                ccd.recordTarget.patientRole.id[0].extension = "12345";
                ccd.recordTarget.patientRole.id[0].root = ConfigurationManager.AppSettings["id_root"].ToString();

                ccd.recordTarget.patientRole.patient = new POCD_MT000040Patient();
                ccd.recordTarget.patientRole.patient.name = new PN[1];
                ccd.recordTarget.patientRole.patient.name[0] = new PN();

                CCD.engiven pgiven = new engiven();
                given.Text = new string[1];
                given.Text[0] = firstName;

                CCD.enfamily pfamily = new enfamily();
                family.Text = new string[1];
                family.Text[0] = lastName;

                CCD.ensuffix psuffix = new ensuffix();
                suffix.Text = new string[1];
                suffix.Text[0] = "";
                ccd.recordTarget.patientRole.patient.name[0].Items = new ENXP[3];
                ccd.recordTarget.patientRole.patient.name[0].Items[0] = pgiven;
                ccd.recordTarget.patientRole.patient.name[0].Items[1] = pfamily;
                ccd.recordTarget.patientRole.patient.name[0].Items[2] = psuffix;

                ccd.recordTarget.patientRole.patient.administrativeGenderCode = new CE();
                ccd.recordTarget.patientRole.patient.administrativeGenderCode.code = "M";
                ccd.recordTarget.patientRole.patient.administrativeGenderCode.codeSystem = "2.16.840.1.113883.5.1";

                ccd.recordTarget.patientRole.patient.birthTime = new TS();
                ccd.recordTarget.patientRole.patient.birthTime.value = dicpatientdetails[0].DOB.ToString();

                ccd.recordTarget.patientRole.providerOrganization = new POCD_MT000040Organization();
                ccd.recordTarget.patientRole.providerOrganization.id = new II[1];
                ccd.recordTarget.patientRole.providerOrganization.id[0] = new II();
                ccd.recordTarget.patientRole.providerOrganization.id[0].extension = "M345";
                ccd.recordTarget.patientRole.providerOrganization.id[0].root = ConfigurationManager.AppSettings["id_root"].ToString();

                POCD_MT000040RelatedDocument relateddocument = new POCD_MT000040RelatedDocument();
                relateddocument.typeCode = 0;

                relateddocument.parentDocument = new POCD_MT000040ParentDocument();
                relateddocument.parentDocument.id = new II[1];
                relateddocument.parentDocument.id[0] = new II();
                relateddocument.parentDocument.id[0].extension = "a123";
                relateddocument.parentDocument.id[0].root = ConfigurationManager.AppSettings["id_root"].ToString();
                relateddocument.parentDocument.setId = new II();
                relateddocument.parentDocument.setId.extension = "BB35";
                relateddocument.parentDocument.setId.root = ConfigurationManager.AppSettings["id_root"].ToString();
                relateddocument.parentDocument.versionNumber = new INT();
             
                relateddocument.parentDocument.versionNumber.value = "1";


                ccd.componentOf = new POCD_MT000040Component1();
                ccd.componentOf.currentEncounter = new CurrentEncounter();
                ccd.componentOf.currentEncounter.id = new II[1];
                ccd.componentOf.currentEncounter.id[0] = new II();
                ccd.componentOf.currentEncounter.id[0].extension = "KPENC1332";
                ccd.componentOf.currentEncounter.id[0].root= ConfigurationManager.AppSettings["id_root"].ToString();
                ccd.componentOf.currentEncounter.effectiveTime = new TS();
                ccd.componentOf.currentEncounter.effectiveTime.value = DateTime.UtcNow.ToString();
                ccd.componentOf.currentEncounter.location = new POCD_MT000040Place();
                ccd.componentOf.currentEncounter.location.healthCareFacility = new POCD_MT000040HealthCareFacility();
                ccd.componentOf.currentEncounter.location.healthCareFacility.classCode = RoleClassServiceDeliveryLocation.DSDLOC;
                ccd.componentOf.currentEncounter.location.healthCareFacility.code = new CE();
                ccd.componentOf.currentEncounter.location.healthCareFacility.code.code = "GIM";
                ccd.componentOf.currentEncounter.location.healthCareFacility.code.code = "2.16.840.1.113883.5.10588";
                ccd.componentOf.currentEncounter.location.healthCareFacility.code.displayName = "General internal medicine clinic";
                ccd.componentOf.currentEncounter.encounterPerformer = new EncounterPerformer();
                ccd.componentOf.currentEncounter.encounterPerformer.typeCode = ActRelationshipHasComponent.CON;
                ccd.componentOf.currentEncounter.encounterPerformer.time = new IVL_TS();
                ccd.componentOf.currentEncounter.encounterPerformer.time.value = "20000407";
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity = new POCD_MT000040AssignedEntity();
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.id = new II[1];
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.id[0] = new II();
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.id[0].extension = "KP00017";
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.id[0].root= ConfigurationManager.AppSettings["id_root"].ToString();
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.assignedPerson = new POCD_MT000040Person();
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.assignedPerson.name = new PN[1];
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.assignedPerson.name[0] = new PN();
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.assignedPerson.name[0].Items = new ENXP[3];
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.assignedPerson.name[0].Items[0] = given;
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.assignedPerson.name[0].Items[1] = family;
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.assignedPerson.name[0].Items[2] = suffix;
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.representedOrganization = new POCD_MT000040Organization();
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.representedOrganization.id = new II[1];
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.representedOrganization.id[0] = new II();
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.representedOrganization.id[0].extension = "M345";
                ccd.componentOf.currentEncounter.encounterPerformer.assignedEntity.representedOrganization.id[0].root = ConfigurationManager.AppSettings["id_root"].ToString();
               ccd.component = new POCD_MT000040Component2();

                POCD_MT000040StructuredBody body = new POCD_MT000040StructuredBody();
                body.component = new POCD_MT000040Component3[6];

                AddMedicationt(ref body, patientid);
                AddAllergies(ref body, patientid); 
                AddFamilyHistory(ref body, patientid);
                AddSocialHistory(ref body, patientid);
                AddViatlHistory(ref body, patientid);
                AddLabResult(ref body, patientid);
                ccd.component.Item = body;
                return Serialize<POCD_MT000040ClinicalDocument>(ccd);
            }


            return "";


        }

        private void AddMedicationt(ref POCD_MT000040StructuredBody body, int patientid)
        {
            List<Medications> objColl = null;
            if (patientid != 0)
            {
                var uri = Utility.GetServiceUrl("medications");
                IgJObject postData = new IgJObject();
                postData.Add("PatientID", patientid);
                objColl = Utility.PostRequest<Medications>(uri, postData.ToString(Formatting.None));
                if (objColl.Count > 0)
                {
                    body.component[0] = new POCD_MT000040Component3();
                    body.component[0].section = new POCD_MT000040Section();
                    body.component[0].section.code = new CE();
                    body.component[0].section.code.code = "10160-0";
                    body.component[0].section.code.codeSystem = ConfigurationManager.AppSettings["code_codesystem"].ToString();
                    body.component[0].section.code.codeSystemName = "LOINC";
                    body.component[0].section.title = new ST();
                    body.component[0].section.title.Text = new string[1];
                    body.component[0].section.title.Text[0] = "Medications";


                    body.component[0].section.text = new StrucDocText();
                    body.component[0].section.text.list = new StrucDocList();
                
                    if (objColl.Count() > 0)
                    {
                        int Icnt = 0;
                        body.component[0].section.text.list.item = new StrucDocItem[objColl.Count()];
                        while (Icnt < objColl.Count())
                        {


                            body.component[0].section.text.list.item[Icnt] = new StrucDocItem();
                            body.component[0].section.text.list.item[Icnt].Text = new string[1];
                            body.component[0].section.text.list.item[Icnt].Text[0] = objColl[Icnt].Description + " " + objColl[Icnt].Dosage + "" + objColl[Icnt].DosageUnits;
                            Icnt++;
                        }
                        Icnt = 0;
                        body.component[0].section.entry = new POCD_MT000040Entry[objColl.Count()];
                        while (Icnt < objColl.Count())
                        {

                            body.component[0].section.entry[Icnt] = new POCD_MT000040Entry();
                            body.component[0].section.entry[Icnt].SubstanceAdministration = new POCD_MT000040SubstanceAdministration();
                            body.component[0].section.entry[Icnt].SubstanceAdministration.text = new ED();
                            body.component[0].section.entry[Icnt].SubstanceAdministration.text.Text = new string[3];
                            body.component[0].section.entry[Icnt].SubstanceAdministration.text.Text[0] = objColl[Icnt].Description;
                            body.component[0].section.entry[Icnt].SubstanceAdministration.text.Text[1] = objColl[Icnt].Dosage + " " + objColl[Icnt].DosageUnits;

                            body.component[0].section.entry[Icnt].SubstanceAdministration.effectiveTime = new SXCM_TS[1];
                            body.component[0].section.entry[Icnt].SubstanceAdministration.effectiveTime[0] = new SXCM_TS();
                            body.component[0].section.entry[Icnt].SubstanceAdministration.effectiveTime[0].value = "PIVL_TS";
                            body.component[0].section.entry[Icnt].SubstanceAdministration.effectiveTime[0].institutionSpecified = true;
                            body.component[0].section.entry[Icnt].SubstanceAdministration.effectiveTime[0].period = new PPD_PQ();
                            body.component[0].section.entry[Icnt].SubstanceAdministration.effectiveTime[0].period.value = objColl[Icnt].HowTaken;
                            body.component[0].section.entry[Icnt].SubstanceAdministration.effectiveTime[0].period.value = objColl[Icnt].Units;
                            body.component[0].section.entry[Icnt].SubstanceAdministration.routeCode = new CE();
                            body.component[0].section.entry[Icnt].SubstanceAdministration.routeCode.code = "PO";
                            body.component[0].section.entry[Icnt].SubstanceAdministration.routeCode.codeSystem = "2.16.840.1.113883.5.112";
                            body.component[0].section.entry[Icnt].SubstanceAdministration.routeCode.displayName = "RouteOfAdministration";
                            body.component[0].section.entry[Icnt].SubstanceAdministration.doseQuantity = new IVL_PQ();
                            body.component[0].section.entry[Icnt].SubstanceAdministration.doseQuantity.ItemsElementName = new ItemsChoiceType[1];
                            body.component[0].section.entry[Icnt].SubstanceAdministration.doseQuantity.ItemsElementName[0] = ItemsChoiceType.center;
                            body.component[0].section.entry[Icnt].SubstanceAdministration.doseQuantity.Items = new PQ[1];
                            body.component[0].section.entry[Icnt].SubstanceAdministration.doseQuantity.Items[0] = new PQ();
                            body.component[0].section.entry[Icnt].SubstanceAdministration.doseQuantity.Items[0].value = objColl[Icnt].Dosage;
                            body.component[0].section.entry[Icnt].SubstanceAdministration.doseQuantity.Items[0].unit = objColl[Icnt].DosageUnits;
                            body.component[0].section.entry[Icnt].SubstanceAdministration.consumable = new POCD_MT000040Consumable();
                            body.component[0].section.entry[Icnt].SubstanceAdministration.consumable.manufacturedProduct = new POCD_MT000040ManufacturedProduct();
                            body.component[0].section.entry[Icnt].SubstanceAdministration.consumable.manufacturedProduct.manufacturedLabeledDrug = new POCD_MT000040LabeledDrug();
                            body.component[0].section.entry[Icnt].SubstanceAdministration.consumable.manufacturedProduct.manufacturedLabeledDrug.code = new CE();
                            body.component[0].section.entry[Icnt].SubstanceAdministration.consumable.manufacturedProduct.manufacturedLabeledDrug.code.code = "66493003";
                            body.component[0].section.entry[Icnt].SubstanceAdministration.consumable.manufacturedProduct.manufacturedLabeledDrug.code.displayName =ConfigurationManager.AppSettings["body_Code_codesystem"];
                            body.component[0].section.entry[Icnt].SubstanceAdministration.consumable.manufacturedProduct.manufacturedLabeledDrug.code.codeSystemName = "SNOMED CT";
                            body.component[0].section.entry[Icnt].SubstanceAdministration.consumable.manufacturedProduct.manufacturedLabeledDrug.code.displayName = "";
                            Icnt++;
                        }


                    }
                }
            }
        }
        private void AddAllergies(ref POCD_MT000040StructuredBody body, int patientid)
        {
            List<Allergy> objColl = null;
            if (patientid != 0)
            {
                var uri = Utility.GetServiceUrl("allergies");
                IgJObject postData = new IgJObject();
                postData.Add("PatientID", patientid);
                objColl = Utility.PostRequest<Allergy>(uri, postData.ToString(Formatting.None));
            }

            body.component[1] = new POCD_MT000040Component3();
            body.component[1].section = new POCD_MT000040Section();
            body.component[1].section.code = new CE();
            body.component[1].section.code.code = "10155-0";
            body.component[1].section.code.codeSystem = ConfigurationManager.AppSettings["code_codesystem"];
            body.component[1].section.code.codeSystemName = "LOINC";

            body.component[1].section.title = new ST();
            body.component[1].section.title.Text = new string[1];
            body.component[1].section.title.Text[0] = "Allergies, adverse reactions, alerts";
       
            body.component[1].section.text = new StrucDocText();
            body.component[1].section.text.list = new StrucDocList();
        

            if (objColl.Count() > 0)
            {
                int Icnt = 0;
                body.component[1].section.text.list.item = new StrucDocItem[objColl.Count()];
                while (Icnt < objColl.Count())
                {
              
                    body.component[1].section.text.list.item[Icnt] = new StrucDocItem();
                    body.component[1].section.text.list.item[Icnt].Text = new string[1];
                    body.component[1].section.text.list.item[Icnt].Text[0] = objColl[Icnt].AllergyType+" - "+ objColl[Icnt].Reaction;
                    Icnt++;
                }
                Icnt = 0;

                body.component[1].section.entry = new POCD_MT000040Entry[objColl.Count()];
                while (Icnt < objColl.Count())
                {
                  
                    body.component[1].section.entry[Icnt] = new POCD_MT000040Entry();
                    body.component[1].section.entry[Icnt].Observation = new POCD_MT000040Observation();
                    body.component[1].section.entry[Icnt].Observation.code = new CE();
                    body.component[1].section.entry[Icnt].Observation.code.code = "84100007";
                    body.component[1].section.entry[Icnt].Observation.code.codeSystem = ConfigurationManager.AppSettings["body_Code_codesystem"];
                    body.component[1].section.entry[Icnt].Observation.code.codeSystemName = "SNOMED CT";
                    body.component[1].section.entry[Icnt].Observation.code.displayName = "history taking (procedure)";
                
                    body.component[1].section.entry[Icnt].Observation.value = new CD();
                    body.component[1].section.entry[Icnt].Observation.value.value = "CD";
                    body.component[1].section.entry[Icnt].Observation.value.code = "247472004";
                    body.component[1].section.entry[Icnt].Observation.value.codeSystem = ConfigurationManager.AppSettings["body_Code_codesystem"];
                    body.component[1].section.entry[Icnt].Observation.value.codeSystemName = "SNOMED CT";
                    body.component[1].section.entry[Icnt].Observation.value.displayName = objColl[Icnt].Reaction;

                    body.component[1].section.entry[Icnt].Observation.entryRelationship = new POCD_MT000040EntryRelationship[1];
                    body.component[1].section.entry[Icnt].Observation.entryRelationship[0] = new POCD_MT000040EntryRelationship();
                    body.component[1].section.entry[Icnt].Observation.entryRelationship[0].typeCode = x_ActRelationshipEntryRelationship.MFST;
                    body.component[1].section.entry[Icnt].Observation.entryRelationship[0].Observation = new POCD_MT000040Observation();
                    body.component[1].section.entry[Icnt].Observation.entryRelationship[0].Observation.code = new CD();
                    body.component[1].section.entry[Icnt].Observation.entryRelationship[0].Observation.code.code = "84100007";
                    body.component[1].section.entry[Icnt].Observation.entryRelationship[0].Observation.code.codeSystem = ConfigurationManager.AppSettings["body_Code_codesystem"];
                    body.component[1].section.entry[Icnt].Observation.entryRelationship[0].Observation.code.codeSystemName = "SNOMED CT";
                    Icnt++;
                }

            }
                 
        }
        private void AddFamilyHistory(ref POCD_MT000040StructuredBody body, int patientid)
        {
            List<FamilyHistoryDetailInfo> objColl = null;
            if (patientid != 0)
            {
                var uri = Utility.GetServiceUrl("patientfamilyhistory");
                IgJObject postData = new IgJObject();
                postData.Add("PatientID", patientid);
                objColl = Utility.PostRequest<FamilyHistoryDetailInfo>(uri, postData.ToString(Formatting.None));
                if (objColl.Count > 0)
                {
                    body.component[2] = new POCD_MT000040Component3();
                    body.component[2].section = new POCD_MT000040Section();
                    body.component[2].section.code = new CE();
                    body.component[2].section.code.code = "11502-2";
                    body.component[2].section.code.codeSystem = ConfigurationManager.AppSettings["code_codesystem"];
                    body.component[2].section.code.codeSystemName = "LOINC";

                    body.component[2].section.text = new StrucDocText();
                    body.component[2].section.text.list = new StrucDocList();
                
                    if (objColl.Count() > 0)
                    {
                        int Icnt = 0;
                        body.component[2].section.text.list.item = new StrucDocItem[objColl.Count()];
                        while (Icnt < objColl.Count())
                        {

                            body.component[2].section.text.list.item[Icnt] = new StrucDocItem();
                            body.component[2].section.text.list.item[Icnt].Text = new string[1];
                            body.component[2].section.text.list.item[Icnt].Text[0] = objColl[Icnt].Relationship + " had " + objColl[Icnt].ConditionName;
                            Icnt++;
                        }
                        Icnt = 0;
                        body.component[2].section.entry = new POCD_MT000040Entry[objColl.Count()];
                        while (Icnt < objColl.Count())
                        {

                            body.component[2].section.entry[Icnt] = new POCD_MT000040Entry();
                            body.component[2].section.entry[Icnt].Observation = new POCD_MT000040Observation();
                            body.component[2].section.entry[Icnt].Observation.code = new CD();
                            body.component[2].section.entry[Icnt].Observation.code.code = "84100007";
                            body.component[2].section.entry[Icnt].Observation.code.codeSystem = ConfigurationManager.AppSettings["body_Code_codesystem"];
                            body.component[2].section.entry[Icnt].Observation.code.codeSystemName = "SNOMED CT";
                            body.component[2].section.entry[Icnt].Observation.code.displayName = "history taking (procedure)";
                            body.component[2].section.entry[Icnt].Observation.effectiveTime = new IVL_TS();
                            body.component[2].section.entry[Icnt].Observation.effectiveTime.value = DateTime.UtcNow.ToString();
                            body.component[2].section.entry[Icnt].Observation.value = new CD();
                            body.component[2].section.entry[Icnt].Observation.value.value = "CD";
                            body.component[2].section.entry[Icnt].Observation.value.code = objColl[0].PatientFamilyHistoryID.ToString();
                            body.component[2].section.entry[Icnt].Observation.value.codeSystem = ConfigurationManager.AppSettings["body_Code_codesystem"];
                            body.component[2].section.entry[Icnt].Observation.value.codeSystemName = "SNOMED CT";
                            body.component[2].section.entry[Icnt].Observation.value.displayName = objColl[0].ConditionName;
                            Icnt++;
                        }
                    }
                }
            }
        }
        private void AddSocialHistory(ref POCD_MT000040StructuredBody body, int patientid)
        {
            List<LifeStyle> objColl = null;
            if (patientid != 0)
            {
                string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("lifestyle");
                IgJObject postData = new IgJObject();
                postData.Add("Pin", PinValue);
                postData.Add("PatientID", patientid);
                objColl = Utility.PostRequest<LifeStyle>(uri, postData.ToString(Formatting.None));

                if (objColl.Count > 0)
                {
                    body.component[3] = new POCD_MT000040Component3();
                    body.component[3].section = new POCD_MT000040Section();
                    body.component[3].section.code = new CE();
                    body.component[3].section.code.code = "29762-2";
                    body.component[3].section.code.codeSystem = ConfigurationManager.AppSettings["code_codesystem"];
                    body.component[3].section.code.codeSystemName = "LOINC";
                    body.component[3].section.title = new ST();
                    body.component[3].section.title.Text = new string[1];
                    body.component[3].section.title.Text[0] = "Social History";


                    body.component[3].section.text = new StrucDocText();
                    body.component[3].section.text.list = new StrucDocList();
                    body.component[3].section.text.list.item = new StrucDocItem[2];
                    body.component[3].section.text.list.item[0] = new StrucDocItem();
                    body.component[3].section.text.list.item[0].Text = new string[1];
                    body.component[3].section.text.list.item[0].Text[0] = "Smoking :: " + objColl[0].ExerciseDaysPerWeek;
                    body.component[3].section.text.list.item[1] = new StrucDocItem();
                    body.component[3].section.text.list.item[1].Text = new string[1];
                    body.component[3].section.text.list.item[1].Text[0] = "Alcohol :: " + objColl[0].DrinksPerWeek;

                    body.component[3].section.entry = new POCD_MT000040Entry[2];
                    body.component[3].section.entry[0] = new POCD_MT000040Entry();
                    body.component[3].section.entry[0].Observation = new POCD_MT000040Observation();
                    body.component[3].section.entry[0].Observation.code = new CD();
                    body.component[3].section.entry[0].Observation.code.code = "229819007";
                    body.component[3].section.entry[0].Observation.code.codeSystem = ConfigurationManager.AppSettings["body_Code_codesystem"];
                    body.component[3].section.entry[0].Observation.code.codeSystemName = "SNOMED CT";
                    body.component[3].section.entry[0].Observation.code.displayName = "Tobacco use and exposure";
                    body.component[3].section.entry[0].Observation.effectiveTime = new IVL_TS();
                    body.component[3].section.entry[0].Observation.effectiveTime.value = DateTime.UtcNow.ToString();
                    body.component[3].section.entry[0].Observation.value = new CD();
                    body.component[3].section.entry[0].Observation.value.value = "CD";
                    body.component[3].section.entry[0].Observation.value.code = objColl[0].LifestyleId.ToString();
                    body.component[3].section.entry[0].Observation.value.codeSystem = ConfigurationManager.AppSettings["body_Code_codesystem"];
                    body.component[3].section.entry[0].Observation.value.codeSystemName = "SNOMED CT";
                    body.component[3].section.entry[0].Observation.value.displayName = objColl[0].ExerciseDaysPerWeek;
                    body.component[3].section.entry[1] = new POCD_MT000040Entry();
                    body.component[3].section.entry[1].Observation = new POCD_MT000040Observation();
                    body.component[3].section.entry[1].Observation.code = new CD();
                    body.component[3].section.entry[1].Observation.code.code = "160573003";
                    body.component[3].section.entry[1].Observation.code.codeSystem = ConfigurationManager.AppSettings["body_Code_codesystem"];
                    body.component[3].section.entry[1].Observation.code.codeSystemName = "SNOMED CT";
                    body.component[3].section.entry[1].Observation.code.displayName = "Alcohol intake";
                    body.component[3].section.entry[1].Observation.effectiveTime = new IVL_TS();
                    body.component[3].section.entry[1].Observation.effectiveTime.value = DateTime.UtcNow.ToString();
                    body.component[3].section.entry[1].Observation.value = new CD();
                    body.component[3].section.entry[1].Observation.value.value = "CD";
                    body.component[3].section.entry[1].Observation.value.code = objColl[0].LifestyleId.ToString();
                    body.component[3].section.entry[1].Observation.value.codeSystem = ConfigurationManager.AppSettings["body_Code_codesystem"];
                    body.component[3].section.entry[1].Observation.value.codeSystemName = "SNOMED CT";
                    body.component[3].section.entry[1].Observation.value.displayName = "Trivial drinker -" + objColl[0].ExerciseDaysPerWeek;
                }
            }
        }
        private void AddViatlHistory(ref POCD_MT000040StructuredBody body, int patientid)
        {
            List<VitalSignsInfo> objColl = null;
            if (patientid != 0)
            {
                //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("vitalsigns");
                IgJObject postData = new IgJObject();
                postData.Add("PatientID", patientid);
                objColl = Utility.PostRequest<VitalSignsInfo>(uri, postData.ToString(Formatting.None));
                if (objColl.Count > 0)
                {
                    body.component[4] = new POCD_MT000040Component3();
                    body.component[4].section = new POCD_MT000040Section();
                    body.component[4].section.code = new CE();
                    body.component[4].section.code.code = "11384-5";
                    body.component[4].section.code.codeSystem = ConfigurationManager.AppSettings["code_codesystem"];
                    body.component[4].section.code.codeSystemName = "LOINC";
                    body.component[4].section.title = new ST();
                    body.component[4].section.title.Text = new string[1];
                    body.component[4].section.title.Text[0] = "Physical Examination";
                    body.component[4].section.component = new POCD_MT000040Component5[1];
                    body.component[4].section.component[0] = new POCD_MT000040Component5();
                    body.component[4].section.component[0].section = new POCD_MT000040Section();
                    body.component[4].section.component[0].section.code = new CE();
                    body.component[4].section.component[0].section.code.code = "8716-3";
                    body.component[4].section.component[0].section.code.codeSystem =ConfigurationManager.AppSettings["code_codesystem"];
                    body.component[4].section.component[0].section.code.codeSystemName = "LOINC";
                    body.component[4].section.component[0].section.title = new ST();
                    body.component[4].section.component[0].section.title.Text = new string[1];
                    body.component[4].section.component[0].section.title.Text[0] = "Vital Signs";
                    body.component[4].section.component[0].section.text = new StrucDocText();
                    body.component[4].section.component[0].section.text.Items = new object[1];


                    CCD.StrucDocTable vitaltable = new StrucDocTable();
                    vitaltable.border = "1";
                    vitaltable.width = "100%";
                    CCD.StrucDocColgroup vitalcolgroup = new StrucDocColgroup();
                    vitalcolgroup.col = new StrucDocCol[12];
                    vitalcolgroup.col[0] = new StrucDocCol();
                    vitalcolgroup.col[1] = new StrucDocCol();
                    vitalcolgroup.col[2] = new StrucDocCol();
                    vitalcolgroup.col[3] = new StrucDocCol();
                    vitalcolgroup.col[4] = new StrucDocCol();
                    vitalcolgroup.col[5] = new StrucDocCol();
                    vitalcolgroup.col[6] = new StrucDocCol();
                    vitalcolgroup.col[7] = new StrucDocCol();
                    vitalcolgroup.col[8] = new StrucDocCol();
                    vitalcolgroup.col[9] = new StrucDocCol();
                    vitalcolgroup.col[10] = new StrucDocCol();
                    vitalcolgroup.col[11] = new StrucDocCol();

                    vitalcolgroup.col[0].width = "30%";
                    vitalcolgroup.col[1].width = "10%";
                    vitalcolgroup.col[2].width = "10%";
                    vitalcolgroup.col[3].width = "10%";
                    vitalcolgroup.col[4].width = "10%";
                    vitalcolgroup.col[5].width = "10%";
                    vitalcolgroup.col[6].width = "10%";
                    vitalcolgroup.col[7].width = "10%";
                    vitalcolgroup.col[8].width = "10%";
                    vitalcolgroup.col[9].width = "10%";
                    vitalcolgroup.col[10].width = "10%";
                    vitalcolgroup.col[11].width = "10%";

                    vitaltable.Items = new object[1];
                    vitaltable.Items[0] = new object();
                    vitaltable.Items[0] = vitalcolgroup;

                    vitaltable.thead = new StrucDocThead();
                    vitaltable.thead.tr = new StrucDocTr[1];
                    vitaltable.thead.tr[0] = new StrucDocTr();
                    vitaltable.thead.tr[0].Items = new object[12];

                    StrucDocTh th1 = new StrucDocTh();
                    StrucDocTh th2 = new StrucDocTh();
                    StrucDocTh th3 = new StrucDocTh();
                    StrucDocTh th4 = new StrucDocTh();
                    StrucDocTh th5 = new StrucDocTh();
                    StrucDocTh th6 = new StrucDocTh();
                    StrucDocTh th7 = new StrucDocTh();
                    StrucDocTh th8 = new StrucDocTh();
                    StrucDocTh th9 = new StrucDocTh();
                    StrucDocTh th10 = new StrucDocTh();
                    StrucDocTh th11 = new StrucDocTh();
                    StrucDocTh th12 = new StrucDocTh();

                    th1.Text = new string[1];
                    th1.Text[0] = "Date / Time";

                    th2.Text = new string[1];
                    th2.Text[0] = "Height";

                    th3.Text = new string[1];
                    th3.Text[0] = "Weight";

                    th4.Text = new string[1];
                    th4.Text[0] = "BMI";

                    th5.Text = new string[1];
                    th5.Text[0] = "BSA";

                    th6.Text = new string[1];
                    th6.Text[0] = "Temperature";

                    th7.Text = new string[1];
                    th7.Text[0] = "Pulse";

                    th8.Text = new string[1];
                    th8.Text[0] = "Rhythm";

                    th9.Text = new string[1];
                    th9.Text[0] = "Respirations";

                    th10.Text = new string[1];
                    th10.Text[0] = "Systolic";

                    th11.Text = new string[1];
                    th11.Text[0] = "Diastolic";

                    th12.Text = new string[1];
                    th12.Text[0] = "Position / Cuff";

                    vitaltable.thead.tr[0].Items[0] = th1;
                    vitaltable.thead.tr[0].Items[1] = th2;
                    vitaltable.thead.tr[0].Items[2] = th3;
                    vitaltable.thead.tr[0].Items[3] = th4;
                    vitaltable.thead.tr[0].Items[4] = th5;
                    vitaltable.thead.tr[0].Items[5] = th6;
                    vitaltable.thead.tr[0].Items[6] = th7;
                    vitaltable.thead.tr[0].Items[7] = th8;
                    vitaltable.thead.tr[0].Items[8] = th9;
                    vitaltable.thead.tr[0].Items[9] = th10;
                    vitaltable.thead.tr[0].Items[10] = th11;
                    vitaltable.thead.tr[0].Items[11] = th12;
                    vitaltable.tbody = new StrucDocTbody[1];
                    vitaltable.tbody[0] = new StrucDocTbody();

                    vitaltable.tbody[0].tr = new StrucDocTr[objColl.Count];
                    int iCnt = 0;

                    vitaltable.tbody[0].tr[iCnt] = new StrucDocTr();
                    vitaltable.tbody[0].tr[iCnt].Items = new object[12];


                    StrucDocTd td1 = new StrucDocTd();
                    td1.Items = new object[1];
                    StrucDocContent cont1 = new StrucDocContent();
                    cont1.Text = new string[1];
                    if (objColl[0].FromDate != null)
                    {
                        if (objColl[0].FromDate.ToString().Length > 0)
                        {
                            cont1.Text[0] = objColl[0].FromDate.ToString();
                            td1.Items[0] = cont1;
                        }
                    }
                    else
                    {
                        cont1.Text[0] = "";
                        td1.Items[0] = cont1;
                    }
                    vitaltable.tbody[0].tr[iCnt].Items[0] = td1;

                    StrucDocTd td2 = new StrucDocTd();
                    td2.Items = new object[1];
                    StrucDocContent cont2 = new StrucDocContent();
                    cont2.Text = new string[1];
                    if (objColl[0].Height.ToString().Length > 0)
                    {
                        cont2.Text[0] = objColl[0].Height.ToString() + " " + objColl[0].HeightUnits.ToString();
                        td2.Items[0] = cont2;
                    }
                    vitaltable.tbody[0].tr[iCnt].Items[1] = td2;

                    StrucDocTd td3 = new StrucDocTd();
                    td3.Items = new object[1];
                    StrucDocContent cont3 = new StrucDocContent();
                    cont3.Text = new string[1];
                    if (objColl[0].Weight.ToString().Length > 0)
                    {
                        cont3.Text[0] = objColl[0].Weight + " " + objColl[0].WeightUnits;
                        td3.Items[0] = cont3;
                    }
                    vitaltable.tbody[0].tr[iCnt].Items[2] = td3;

                    StrucDocTd td4 = new StrucDocTd();
                    td4.Items = new object[1];
                    StrucDocContent cont4 = new StrucDocContent();
                    cont4.Text = new string[1];
                    if (BMI.ToString().Length > 0)
                    {
                        cont4.Text[0] = BMI;
                        td4.Items[0] = cont4;
                    }
                    vitaltable.tbody[0].tr[iCnt].Items[3] = td4;

                    StrucDocTd td5 = new StrucDocTd();
                    td5.Items = new object[1];
                    StrucDocContent cont5 = new StrucDocContent();
                    cont5.Text = new string[1];
                    if ("BSA".Length > 0)
                    {
                        cont5.Text[0] = "0";
                        td5.Items[0] = cont5;
                    }
                    vitaltable.tbody[0].tr[iCnt].Items[4] = td5;

                    StrucDocTd td6 = new StrucDocTd();
                    td6.Items = new object[1];
                    StrucDocContent cont6 = new StrucDocContent();
                    cont6.Text = new string[1];
                    if (objColl[0].Temperature.ToString().Length > 0)
                    {
                        cont6.Text[0] = objColl[0].Temperature + " " + objColl[0].Temperature;
                        td6.Items[0] = cont6;
                    }
                    vitaltable.tbody[0].tr[iCnt].Items[5] = td6;

                    StrucDocTd td7 = new StrucDocTd();
                    td7.Items = new object[1];
                    StrucDocContent cont7 = new StrucDocContent();
                    cont7.Text = new string[1];
                    if (objColl[0].Pulse.ToString().Length > 0)
                    {
                        cont7.Text[0] = objColl[0].Pulse.ToString() + "BPM";
                        td7.Items[0] = cont7;
                    }
                    vitaltable.tbody[0].tr[iCnt].Items[6] = td7;


                    StrucDocTd td8 = new StrucDocTd();
                    td8.Items = new object[1];
                    StrucDocContent cont8 = new StrucDocContent();
                    cont8.Text = new string[1];
                    if ("Rhythm".ToString().Length > 0)
                    {
                        cont8.Text[0] = "";
                        td8.Items[0] = cont8;
                    }
                    vitaltable.tbody[0].tr[iCnt].Items[7] = td8;

                    StrucDocTd td9 = new StrucDocTd();
                    td9.Items = new object[1];
                    StrucDocContent cont9 = new StrucDocContent();
                    cont9.Text = new string[1];
                    if (objColl[0].Respiration.ToString().Length > 0)
                    {
                        cont9.Text[0] = objColl[0].Respiration.ToString() + "BPM";
                        td9.Items[0] = cont9;
                    }
                    vitaltable.tbody[0].tr[iCnt].Items[8] = td9;

                    StrucDocTd td10 = new StrucDocTd();
                    td10.Items = new object[1];
                    StrucDocContent cont10 = new StrucDocContent();
                    cont10.Text = new string[1];
                    if ("Systolic".ToString().Length > 0)
                    {
                        cont10.Text[0] = "";
                        td10.Items[0] = cont10;
                    }
                    vitaltable.tbody[0].tr[iCnt].Items[9] = td10;


                    StrucDocTd td11 = new StrucDocTd();
                    td11.Items = new object[1];
                    StrucDocContent cont11 = new StrucDocContent();
                    cont11.Text = new string[1];
                    if ("Diastolic".ToString().Length > 0)
                    {
                        cont11.Text[0] = "";
                        td11.Items[0] = cont11;
                    }
                    vitaltable.tbody[0].tr[iCnt].Items[10] = td11;

                    StrucDocTd td12 = new StrucDocTd();
                    td12.Items = new object[1];
                    StrucDocContent cont12 = new StrucDocContent();
                    cont12.Text = new string[1];
                    if ("Position / Cuff".ToString().Length > 0)
                    {
                        cont12.Text[0] = "";
                        td12.Items[0] = cont12;
                    }
                    vitaltable.tbody[0].tr[iCnt].Items[11] = td12;
                    body.component[4].section.component[0].section.text.Items[0] = vitaltable;
                }

            }
        }
        private void AddLabResult(ref POCD_MT000040StructuredBody body, int patientid)
        {
            List<LabImaging> objColl = null;
            if (patientid != 0)
            {
                var uri = Utility.GetServiceUrl("lab");
                IgJObject postData = new IgJObject();
                postData.Add("PatientID", patientid);
                objColl = Utility.PostRequest<LabImaging>(uri, postData.ToString(Formatting.None));
                body.component[5] = new POCD_MT000040Component3();
                body.component[5].typeCode = new ActRelationshipHasComponent();
                body.component[5].contextConductionInd = true;
                body.component[5].section = new POCD_MT000040Section();
                body.component[5].section.code = new CE();
                body.component[5].section.code.code = "11502-2";
                body.component[5].section.code.codeSystem = ConfigurationManager.AppSettings["body_Code_codesystem"];
                body.component[5].section.code.codeSystemName = "LOINC";

                body.component[5].section.title = new ST();
                body.component[5].section.title.Text = new string[1];
                body.component[5].section.title.Text[0] = "Labs";

                body.component[5].section.text = new StrucDocText();
                body.component[5].section.text.list = new StrucDocList();
                if (objColl.Count() > 0)
                {
                    int Icnt = 0;
                    body.component[5].section.text.list.item = new StrucDocItem[objColl.Count()];
                    while (Icnt <= objColl.Count())
                    {

                    
                       
                        body.component[5].section.text.list.item[Icnt] = new StrucDocItem();
                        body.component[5].section.text.list.item[Icnt].Text = new string[1];
                        body.component[5].section.text.list.item[Icnt].Text[0] = objColl[Icnt].Results + " " + objColl[Icnt].Date + ":"+ objColl[Icnt].Reason; ;
                        Icnt++;
                    }

                    Icnt = 0;
                    body.component[5].section.entry = new POCD_MT000040Entry[objColl.Count()];
                    while (Icnt <= objColl.Count())
                    {
                    
                        body.component[5].section.entry[Icnt] = new POCD_MT000040Entry();
                        body.component[5].section.entry[Icnt].Observation = new POCD_MT000040Observation();
                        body.component[5].section.entry[Icnt].Observation.id = new II[1];
                        body.component[5].section.entry[Icnt].Observation.id[0] = new II();
                        body.component[5].section.entry[Icnt].Observation.id[0].root = "10.23.4573.15877";
                        body.component[5].section.entry[Icnt].Observation.code = new CD();
                        body.component[5].section.entry[Icnt].Observation.code.code = "282290005";
                        body.component[5].section.entry[Icnt].Observation.code.codeSystem = ConfigurationManager.AppSettings["body_Code_codesystem"];
                        body.component[5].section.entry[Icnt].Observation.code.codeSystemName = "SNOMED CT";
                        body.component[5].section.entry[Icnt].Observation.code.displayName = "Imaging interpretation";
                        body.component[5].section.entry[Icnt].Observation.value = new CD();
                        body.component[5].section.entry[Icnt].Observation.value.value = "CD";
                        body.component[5].section.entry[Icnt].Observation.value.code = "249674001";
                        body.component[5].section.entry[Icnt].Observation.value.codeSystem = ConfigurationManager.AppSettings["body_Code_codesystem"];
                        body.component[5].section.entry[Icnt].Observation.value.codeSystemName = "SNOMED CT";
                        body.component[5].section.entry[Icnt].Observation.value.displayName = objColl[Icnt].Results;
                        body.component[5].section.entry[Icnt].Observation.value.originalText = new ED();
                        body.component[5].section.entry[Icnt].Observation.value.originalText.Text = new string[0];
                        body.component[5].section.entry[Icnt].Observation.value.originalText.Text[0] = objColl[Icnt].Reason;
                        body.component[5].section.entry[Icnt].Observation.reference = new POCD_MT000040Reference[1];
                        body.component[5].section.entry[Icnt].Observation.reference[0] = new POCD_MT000040Reference();
                        body.component[5].section.entry[Icnt].Observation.reference[0].typeCode = x_ActRelationshipExternalReference.SPRT;
                        body.component[5].section.entry[Icnt].Observation.reference[0].ExternalObservation = new POCD_MT000040ExternalObservation();
                        body.component[5].section.entry[Icnt].Observation.reference[0].ExternalObservation.classCode = "DGIMG";
                        body.component[5].section.entry[Icnt].Observation.reference[0].ExternalObservation.id = new II[1];
                        body.component[5].section.entry[Icnt].Observation.reference[0].ExternalObservation.id[0] = new II();
                        body.component[5].section.entry[Icnt].Observation.reference[0].ExternalObservation.id[0].root = "123.456.2557";
                        body.component[5].section.entry[Icnt].Observation.reference[0].ExternalObservation.code = new CD();
                        body.component[5].section.entry[Icnt].Observation.reference[0].ExternalObservation.code.code = "56350004";
                        body.component[5].section.entry[Icnt].Observation.reference[0].ExternalObservation.code.codeSystem = ConfigurationManager.AppSettings["body_Code_codesystem"];
                        body.component[5].section.entry[Icnt].Observation.reference[0].ExternalObservation.code.codeSystemName = "SNOMED CT";
                        body.component[5].section.entry[Icnt].Observation.reference[0].ExternalObservation.code.displayName = objColl[Icnt].Results;
                        Icnt++;
                    }

                }

            }
        }


    }
}