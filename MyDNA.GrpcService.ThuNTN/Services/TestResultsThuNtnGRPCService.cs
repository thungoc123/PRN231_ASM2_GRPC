using Grpc.Core;
using MyDNA.GrpcService.ThuNTN.Protos;
using System.Text.Json.Serialization;
using System.Text.Json;
using MyDNA.Services.ThuNTN;
namespace MyDNA.GrpcService.ThuNTN.Services
{
    public class TestResultsThuNtnGRPCService : TestResultsThuNtnGRPC.TestResultsThuNtnGRPCBase
    {
        private readonly IServiceProviders _serviceProviders;

        public TestResultsThuNtnGRPCService(IServiceProviders serviceProviders) => _serviceProviders = serviceProviders;

        public override async Task<TestResultsThuNtnList> GetAllAsync(EmptyRequest request, ServerCallContext context)
        {
            var result = new TestResultsThuNtnList();

            try
            {
                var cashDepositSlips = await _serviceProviders.testResultsThuNtnService.GetAllAsync();

                var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };


                var cashDepositSlipJsonString = JsonSerializer.Serialize(cashDepositSlips, opt);


                var items = JsonSerializer.Deserialize<List<TestResultsThuNtn>>(cashDepositSlipJsonString, opt);


                result.Items.AddRange(items);
            }
            catch (Exception ex) { }

            return result;
        }

        public override async Task<TestResultsThuNtn> GetByIdAsync(TestResultThuNtnIdRequest request, ServerCallContext context)
        {
            var result = new TestResultsThuNtn();

            try
            {
                var testResultThuNtn = await _serviceProviders.testResultsThuNtnService.GetByIdAsync(request.TestResultThuNtnid);

                var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

                var cashDepositSlipJsonString = JsonSerializer.Serialize(testResultThuNtn, opt);

                result = JsonSerializer.Deserialize<TestResultsThuNtn>(cashDepositSlipJsonString, opt);
            }
            catch (Exception ex) { }

            return result;
        }

        public override async Task<MutationResult> CreateAsync(TestResultsThuNtn request, ServerCallContext context)
        {
            try
            {

                var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };


                var protoJsonString = JsonSerializer.Serialize(request, opt);


                var item = JsonSerializer.Deserialize<MyDNA.Repositories.ThuNTN.Models.TestResultsThuNtn>(protoJsonString, opt);

                var result = await _serviceProviders.testResultsThuNtnService.CreateAsync(item);

                return new MutationResult() { Result = result };
            }
            catch (Exception ex) { }

            return new MutationResult() { Result = 0 };
        }

       




    }


}
