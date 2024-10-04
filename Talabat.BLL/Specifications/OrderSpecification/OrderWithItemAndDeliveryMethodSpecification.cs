using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat
{
    public class OrderWithItemAndDeliveryMethodSpecification:BaseSpecification<Order>
    {
        //to get all orders with user
        public OrderWithItemAndDeliveryMethodSpecification(string BuyerEmail)
            :base(O=>O.BuyerEmail==BuyerEmail)
        {
            AddInclude(O=>O.Items);
            AddInclude(O => O.DeliveryMethod);

            AddOrderByDescending(O => O.OrderDate);
        }

        public OrderWithItemAndDeliveryMethodSpecification(int OrderId,string BuyerEmail)
            :base(O=>(O.Id==OrderId)&&(O.BuyerEmail==BuyerEmail))
        {
            AddInclude(O => O.Items);
            AddInclude(O => O.DeliveryMethod);

        }
        
    }
}
