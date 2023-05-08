using Scarlet.DataAccess.Data;
using Scarlet.DataAccess.Repository.IRepository;
using Scarlet.Models;

namespace Scarlet.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFormDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (orderFormDb != null)
            {
                orderFormDb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    orderFormDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
        {
            var orderFormDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (orderFormDb != null)
            {
                if (!string.IsNullOrEmpty(sessionId))
                {
                    orderFormDb.SessionId = sessionId;
                }
                if (!string.IsNullOrEmpty(paymentIntentId))
                {
                    orderFormDb.PaymentIntentId = paymentIntentId;
                    orderFormDb.PaymentDate = DateTime.Now;
                }
            }
        }
    }
}
