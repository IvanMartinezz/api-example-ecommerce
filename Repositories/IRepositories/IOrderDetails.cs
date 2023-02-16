using DemoAPI.Models.Data;

namespace DemoAPI.Repository
{
    public interface IOrderDetails
    {
        Task<OrderDetails> Create(OrderDetails orderDetails);
        Task<bool> Delete(OrderDetails orderDetails);
        OrderDetails GetById(int id);
        Task<bool> Update(OrderDetails orderDetails);

    }
}
