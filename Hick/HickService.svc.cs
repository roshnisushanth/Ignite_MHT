using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace Hick
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HickService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HickService.svc or HickService.svc.cs at the Solution Explorer and start debugging.

    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class HickService 
    {
        [OperationContract]
        public string Send(long currentuserid,string fileurl)
        {
            string result = string.Empty;
            try
            {
                result = "sucess";  
            }
            catch (Exception ex)
            {
                result = "error";  
            }
            return result;
        }
    }
}
