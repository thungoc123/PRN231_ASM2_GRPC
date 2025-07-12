using MyDNA.Repositories.ThuNTN;
using MyDNA.Repositories.ThuNTN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDNA.Services.ThuNTN
{
    public class SystemUserAccountService
    {
        //private readonly SystemUserAccountRepository _systemUserAccountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SystemUserAccountService() => _unitOfWork ??= new UnitOfWork();

        //public SystemUserAccountService(SystemUserAccountRepository systemUserAccountRepository)
        //{
        //    _systemUserAccountRepository = systemUserAccountRepository;
        //}

        public async Task<SystemUserAccount> GetUserAccountAsync(string username, string password)
        {
            return await _unitOfWork.SystemUserAccountRepository.GetUserAccountAsync(username, password);
        }
    }
}
