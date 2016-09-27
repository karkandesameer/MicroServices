using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerProjectOrder.Model.Response
{
    public class CustomerProjectOrdersResponse : BaseResponse
    {
        public List<CustomerProjectOrderModel> CustomerProjectOrders { get; set; } 
    }
}
