using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Talabat
{
    public class OrderService : IOrderService
    {
/*        private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
        private readonly IGenericRepository<Order> _orderRepo;*/
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork/*IGenericRepository<DeliveryMethod> deliveryMethod,IGenericRepository<Order> orderRepo*/)
        {
/*            _deliveryMethodRepo = deliveryMethod;
            _orderRepo = orderRepo;*/
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrder(string buyerEmail, string Id, int deliveryMethodId, OrderAddress shipToAddress)
        {
            //var deliveryMethod = await _deliveryMethodRepo.GetByIdAsync(deliveryMethodId); //before UnitOfWork
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId); //after UnitOfWork
            var orderItems = new List<OrderItem>();
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            var order = new Order(buyerEmail, shipToAddress, deliveryMethod, orderItems, subTotal);
            //await _orderRepo.Add(order); //before UOW:UnitOfWork
            await _unitOfWork.Repository<Order>().Add(order); //after UOW
            int result = await _unitOfWork.Complete(); //add await becuase it's async function
            if (result <= 0) return null;
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemAndDeliveryMethodSpecification(buyerEmail); //must add new
            var result = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return result;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
           var result =  await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return result;
        }

        public async Task<Order> GetOrdersByIdForUserAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderWithItemAndDeliveryMethodSpecification(orderId, buyerEmail);
            var result = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            return result;
        }
    }
}
