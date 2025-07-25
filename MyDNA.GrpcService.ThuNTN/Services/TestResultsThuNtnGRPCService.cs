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
            var opt = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            try
            {
                var protoJsonString = JsonSerializer.Serialize(request, opt);
                Console.WriteLine("Serialized request JSON: " + protoJsonString);

                var item = JsonSerializer.Deserialize<MyDNA.Repositories.ThuNTN.Models.TestResultsThuNtn>(protoJsonString, opt);

                if (item == null)
                {
                    Console.WriteLine("❌ Deserialize thất bại: item null");
                    return new MutationResult() { Result = 0 };
                }

                var dataImport = new MyDNA.Repositories.ThuNTN.Models.TestResultsThuNtn()
                {
                    OrderId = item.OrderId,
                    ResultVersion = item.ResultVersion,
                    ResultSummary = item.ResultSummary,
                    ResultDetail = item.ResultDetail,
                    ResultFileUrl = item.ResultFileUrl,
                    IssuedBy = item.IssuedBy,
                    CompletedAt = item.CompletedAt,
                    CreatedAt = DateTime.Now,
                    ResultStatus = item.ResultStatus
                };

                var result = await _serviceProviders.testResultsThuNtnService.CreateAsync(dataImport);
                Console.WriteLine("✅ Service tạo dữ liệu trả về: " + result);

                return new MutationResult() { Result = result };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi khi tạo dữ liệu: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return new MutationResult() { Result = 0 };
            }
        }


        public override async Task<MutationResult> UpdateAsync(TestResultsThuNtn request, ServerCallContext context)
        {
            var opt = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            try
            {
                var protoJsonString = JsonSerializer.Serialize(request, opt);
                Console.WriteLine("Serialized update JSON: " + protoJsonString);

                var item = JsonSerializer.Deserialize<MyDNA.Repositories.ThuNTN.Models.TestResultsThuNtn>(protoJsonString, opt);

                if (item == null || item.TestResultThuNtnid <= 0)
                {
                    Console.WriteLine("❌ Invalid or null item for update.");
                    return new MutationResult { Result = 0 };
                }

                var result = await _serviceProviders.testResultsThuNtnService.UpdateAsync(item);
                Console.WriteLine("✅ Update service returned: " + result);

                return new MutationResult { Result = result };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error during update: {ex.Message}");
                return new MutationResult { Result = 0 };
            }
        }


        public override async Task<MutationResult> DeleteAsync(TestResultThuNtnIdRequest request, ServerCallContext context)
        {
            try
            {
                if (request.TestResultThuNtnid <= 0)
                {
                    Console.WriteLine("❌ Invalid ID for deletion.");
                    return new MutationResult { Result = 0 };
                }

                var result = await _serviceProviders.testResultsThuNtnService.DeleteAsync(request.TestResultThuNtnid);
                var res = result ? 1 : 0;
                Console.WriteLine("✅ Delete service returned: " + result);

                return new MutationResult { Result = res };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error during deletion: {ex.Message}");
                return new MutationResult { Result = 0 };
            }
        }



    }


}
