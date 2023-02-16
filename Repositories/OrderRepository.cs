using DemoAPI.Helpers;
using DemoAPI.Migrations;
using DemoAPI.Models.Data;
using DemoAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        protected readonly DataContext _context;

        public OrderRepository(DataContext context) => _context = context;

        public async Task<Order> Create(Order order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Delete(Order order)
        {
            try
            {
                if (order is null) return false;
                _context.Set<Order>().Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Order GetById(int id)
        {
            try
            {
                return _context.Orders.Find(id);
            } catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Update(Order order)
        {
            try
            {
                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
