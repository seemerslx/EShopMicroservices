using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext(DbContextOptions<DiscountContext> opts) : DbContext(opts)
    {
        public DbSet<CouponModel> Coupons => Set<CouponModel>();
    }
}
