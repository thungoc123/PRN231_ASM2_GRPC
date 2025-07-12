using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDNA.Services.ThuNTN
{
    public interface IServiceProviders
    {
        SystemUserAccountService SystemUserAccountService { get; }
        TestOrdersThuNtnService TestOrdersThuNtnService { get; }
        ITestResultsThuNtnService testResultsThuNtnService { get; }

    }
    public class ServiceProviders : IServiceProviders
    {
        private SystemUserAccountService _systemUserAccountService;
        private TestOrdersThuNtnService _testOrdersThuNtnService;
        private ITestResultsThuNtnService _testResultsThuNtnService;
        public ServiceProviders()
        {
           
        }
        public SystemUserAccountService SystemUserAccountService
        {
            get { return _systemUserAccountService ??= new SystemUserAccountService(); }
        }

        public TestOrdersThuNtnService TestOrdersThuNtnService
        {
            get { return _testOrdersThuNtnService ??= new TestOrdersThuNtnService(); }
        }


        public ITestResultsThuNtnService testResultsThuNtnService
        {
            get { return _testResultsThuNtnService ??= new TestResultsThuNtnService(); }
        }
    }
}
