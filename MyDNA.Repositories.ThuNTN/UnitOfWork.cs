using MyDNA.Repositories.ThuNTN.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDNA.Repositories.ThuNTN
{
    public interface IUnitOfWork : IDisposable
    {
        SystemUserAccountRepository SystemUserAccountRepository { get; }
        TestOrdersThuNtnRepository TestOrdersThuNtnRepository { get; }
        TestResultsThuNtnRepository TestResultsThuNtnRepository { get; }

        int SaveChangesWithTransaction();
        Task<int> SaveChangesWithTransactionAsync();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SU25_PRN231_SE171992_G1_MyDNAContext  _context;
        private SystemUserAccountRepository _systemUserAccountRepository;


    public UnitOfWork() => _context ??= new SU25_PRN231_SE171992_G1_MyDNAContext();

        public SystemUserAccountRepository SystemUserAccountRepository
        {
            get { return _systemUserAccountRepository ??= new SystemUserAccountRepository(_context); }
        }

        private TestOrdersThuNtnRepository _testOrdersThuNtnRepository;
        public TestOrdersThuNtnRepository TestOrdersThuNtnRepository
        {
            get { return _testOrdersThuNtnRepository ??= new TestOrdersThuNtnRepository(_context); }
        }
        private TestResultsThuNtnRepository _testResultsThuNtnRepository;
        public TestResultsThuNtnRepository TestResultsThuNtnRepository
        {
            get { return _testResultsThuNtnRepository ??= new TestResultsThuNtnRepository(_context); }
        }
        public void Dispose()
        {
            _context?.Dispose();
        }

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }
    }
}
