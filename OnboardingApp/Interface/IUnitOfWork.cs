using Microsoft.EntityFrameworkCore.Storage;
using OnboardingApp.Model;
using System.Threading;
using System.Threading.Tasks;

namespace OnboardingApp.Interface
{
    public interface IUnitOfWork
    {
        IRepository<Book> BookRepository { get; }
        IRepository<Borrower> BorrowerRepository { get; }
        void Save();
        Task SaveAsync();
        PerpustakaanDBContext GetContext();
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
