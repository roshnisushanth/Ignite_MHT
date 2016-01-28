using IGNITE_MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_DAL.Interfaces
{
    public interface IAuditManager
    {
        AuditWrapper GetAllAudits(long currentuserid, string usertype, int PageIndex, int PageSize);
    }
}
