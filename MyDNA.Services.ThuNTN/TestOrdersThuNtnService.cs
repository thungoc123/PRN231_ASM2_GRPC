using MyDNA.Repositories.ThuNTN;
using MyDNA.Repositories.ThuNTN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDNA.Services.ThuNTN
{
    public class TestOrdersThuNtnService
    {
        //private readonly TestOrdersThuNtnRepository _testOrdersThuNtnRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TestOrdersThuNtnService() =>

            _unitOfWork ??= new UnitOfWork();

        //public TestOrdersThuNtnService(TestOrdersThuNtnRepository testOrdersThuNtnRepository)
        //{
        //    _unitOfWork.TestOrdersThuNtnRepository = testOrdersThuNtnRepository;
        //}

        public async Task<List<TestOrdersThuNtn>> GetAllAsync()
        {
            return await _unitOfWork.TestOrdersThuNtnRepository.GetAllAsync();
        }
    }
}
