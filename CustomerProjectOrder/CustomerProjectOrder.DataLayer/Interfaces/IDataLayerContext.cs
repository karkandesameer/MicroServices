using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerProjectOrder.DataLayer.Entities;

namespace CustomerProjectOrder.DataLayer.Interfaces
{
    public interface IDataLayerContext
    {
        CustomerProjectOrderHeader GetProjectByNumber(string companyCode, string projectNumber);
        List<CustomerProjectOrderHeader> GetProjectByCompanyCode(string companyCode);
        List<CustomerProjectOrderHeader> GetProjectByName(string companyCode, string projectName);
        List<CustomerProjectOrderHeader> GetProjectByDuration(string companyCode, string startDate, string endDate);
        CustomerProjectOrderHeader GetProjectByCustomerPONo(string companyCode, string customerPONo);
        CustomerProjectOrderHeader GetProjectByAccount(string companyCode, string account);
    }
}
