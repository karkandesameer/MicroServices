using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerProjectOrder.Model.Response
{
   public class CustomerProjectOrderResponse : BaseResponse
    {
        public CustomerProjectOrderModel CustomerProjectOrder { get; set; }
    }
}
