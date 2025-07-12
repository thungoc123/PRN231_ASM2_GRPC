// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using MyDNA.GrpcService.ThuNTN.Protos;

Console.WriteLine("Hello, World!");
using var channel = GrpcChannel.ForAddress("https://localhost:7189");


var clientTestResultsThuNtnGRPC = new TestResultsThuNtnGRPC.TestResultsThuNtnGRPCClient(channel);

Console.WriteLine("\r\nclientTestResultsThuNtnGRPC.GetAll(EmpltyRequest):");
var testResult = clientTestResultsThuNtnGRPC.GetAllAsync(new EmptyRequest() { });
if (testResult != null && testResult.Items.Count > 0)
{
    foreach (var item in testResult.Items)
    {
        Console.WriteLine(string.Format("{0} - {1}", item.ResultVersion, item.ResultSummary));
    }
}

Console.WriteLine("\r\nclientTestResultsThuNtnGRPC.GetById(TestResultThuNtnIdRequest={1}):");
var testResultbyID = clientTestResultsThuNtnGRPC.GetByIdAsync(new TestResultThuNtnIdRequest() { TestResultThuNtnid = 1 });
Console.WriteLine(string.Format("{0} - {1}", testResultbyID.OrderId, testResultbyID.ResultSummary));