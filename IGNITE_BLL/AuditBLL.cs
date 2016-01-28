using IGNITE_DAL.DataObjects;
using IGNITE_DAL.Interfaces;
using IGNITE_MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_BLL
{
    public class AuditBLL
    {
        private readonly IAuditManager auditManager = new AuditManager();

        public AuditWrapper GetAllAudits(long currentuserid, string usertype, int PageIndex, int PageSize)
        {
            return auditManager.GetAllAudits(currentuserid, usertype, PageIndex, PageSize);
        }
    }
}
