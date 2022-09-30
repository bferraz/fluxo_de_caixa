using API.Caixa.Domain.Entities;
using API.Caixa.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Caixa.Infra.Data
{
    public class CashierContext : DbContext, IUnitOfWork
    {
        public CashierContext(DbContextOptions<CashierContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Entry> Entries { get; set; }

        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;

            return success;
        }
    }
}
