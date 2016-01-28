using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IGNITE_MODEL.SuperUser
{
  
     public class MUReport
    {
         // Get or Set MeasureNumber
        [XmlElement("MeasureNumber")]
         public string MeasureNumber { get; set; }

         //Get or Set Demominator count
         [XmlElement("TotalPopulation")]
         public int Denominator_Count { get; set; }

         // Get or Set Numerator count
          [XmlElement("EligiblePopulation")]
         public int Numerator_Count { get; set; }

         // Get or Set Percentage
          [XmlElement("Performance")]
         public string Percentage { get; set; }

         // Get or Set Meet Criteria 
         [XmlElement("MeetCriteria")]
          public string Meet { get; set; }
    }

     public class MUReportDetails
     {
         /// <summary>
         /// Get or set the patient name
         /// </summary>
         public string PatientName { get; set; }

         /// <summary>
         /// Get or set the data type
         /// </summary>
         public string DataType { get; set; }

         /// <summary>
         /// Get or set data value
         /// </summary>
         public string DataValue { get; set; }

         /// <summary>
         /// Get or set activity date
         /// </summary>
         public string DateOfActivity { get; set; }

         /// <summary>
         /// Get or set activity time
         /// </summary>
         public string TimeOfActivity { get; set; }

         /// <summary>
         /// Get or set Ip address
         /// </summary>
         public string IpAddress { get; set; }
     }
     public class MUReportList
     {

         public List<MUReport> MUReport_List { get; set; }
     }

    public class ProviderList
    {
        public List<Contact> Provider_List { get; set; }
    }


    public class MUReportDetailList
    {
        public List<MUReportDetails> MUReportDetail_List { get; set; }
    }
}
