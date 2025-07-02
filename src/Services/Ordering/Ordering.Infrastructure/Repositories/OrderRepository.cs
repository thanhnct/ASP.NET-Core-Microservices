using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBaseAsync<Order, long, OrderContext>, IOrderRepository
    {
        public OrderRepository(OrderContext db, IUnitOfWork<OrderContext> unitOfWork) : base(db, unitOfWork)
        {
        }

        public async Task<IEnumerable<Order>> GetOrderByUserName(string userName) 
            => await FindByCondition(o => o.UserName.Equals(userName)).ToListAsync();
    }
}
