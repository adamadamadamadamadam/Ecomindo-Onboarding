using Microsoft.EntityFrameworkCore.Storage;
using OnboardingApp.Interface;
using OnboardingApp.Model;
using System.Threading.Tasks;

namespace OnboardingApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private PerpustakaanDBContext dbContext;
        public IRepository<Book> BookRepository { get; }

        public IRepository<Borrower> BorrowerRepository { get; }

        public UnitOfWork(PerpustakaanDBContext context)
        {
            dbContext = context;

            BookRepository = new Repository<Book>(dbContext);
            BorrowerRepository = new Repository<Borrower>(dbContext);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return dbContext.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await dbContext.Database.BeginTransactionAsync();
        }

        public PerpustakaanDBContext GetContext()
        {
            return dbContext;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
