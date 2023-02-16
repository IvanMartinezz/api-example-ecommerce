using DemoAPI.Models.Data;

namespace DemoAPI.Repository
{
    public interface IOrderRepository
    {
        Task<Order> Create(Order order);
        Task<bool> Delete(Order order);
        Order GetById(int id);
        Task<bool> Update(Order order);
    }
}
