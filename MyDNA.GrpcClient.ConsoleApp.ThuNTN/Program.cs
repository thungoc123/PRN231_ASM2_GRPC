//// See https://aka.ms/new-console-template for more information
//using Grpc.Net.Client;
//using MyDNA.GrpcService.ThuNTN.Protos;

//Console.WriteLine("Hello, World!");
//using var channel = GrpcChannel.ForAddress("https://localhost:7189");


//var clientTestResultsThuNtnGRPC = new TestResultsThuNtnGRPC.TestResultsThuNtnGRPCClient(channel);

//Console.WriteLine("\r\nclientTestResultsThuNtnGRPC.GetAll(EmpltyRequest):");
//var testResult = clientTestResultsThuNtnGRPC.GetAllAsync(new EmptyRequest() { });
//if (testResult != null && testResult.Items.Count > 0)
//{
//    foreach (var item in testResult.Items)
//    {
//        Console.WriteLine(string.Format("{0} - {1}", item.ResultVersion, item.ResultSummary));
//    }
//}

//Console.WriteLine("\r\nclientTestResultsThuNtnGRPC.GetById(TestResultThuNtnIdRequest={1}):");
//Console.WriteLine("Please enter a TestResultThuNtn ID to retrieve:");
//var testResultbyID = clientTestResultsThuNtnGRPC.GetByIdAsync(new TestResultThuNtnIdRequest { TestResultThuNtnid = 2 });

//Console.WriteLine(string.Format("{0} - {1}", testResultbyID.OrderId, testResultbyID.ResultSummary));


using Grpc.Net.Client;
using MyDNA.GrpcService.ThuNTN.Protos;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
using var channel = GrpcChannel.ForAddress("https://localhost:7189");
var client = new TestResultsThuNtnGRPC.TestResultsThuNtnGRPCClient(channel);

while (true)
{
    Console.WriteLine("\n======= TEST RESULT MANAGEMENT MENU =======");
    Console.WriteLine("1. View all test results");
    Console.WriteLine("2. View a test result by ID");
    Console.WriteLine("3. Add new test result");
    Console.WriteLine("4. Update existing test result");
    Console.WriteLine("5. Delete a test result");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            GetAll();
            break;
        case "2":
            GetById();
            break;
        case "3":
            Create();
            break;
        case "4":
            Update();
            break;
        case "5":
            Delete();
            break;
        case "0":
            return;
        default:
            Console.WriteLine("⚠️ Invalid selection. Please try again.");
            break;
    }
}

void GetAll()
{
    var response = client.GetAllAsync(new EmptyRequest());

    Console.WriteLine("\n📋 LIST OF TEST RESULTS:");
    foreach (var item in response.Items)
    {
        Console.WriteLine("===========================================");
        Console.WriteLine($"ID:             {item.TestResultThuNtnid}");
        Console.WriteLine($"Order ID:       {item.OrderId}");
        Console.WriteLine($"Version:        {item.ResultVersion}");
        Console.WriteLine($"Summary:        {item.ResultSummary}");
        Console.WriteLine($"Detail:         {item.ResultDetail}");
        Console.WriteLine($"File URL:       {item.ResultFileUrl}");
        Console.WriteLine($"Issued By:      {item.IssuedBy}");
        Console.WriteLine($"Completed At:   {item.CompletedAt}");
        Console.WriteLine($"Created At:     {item.CreatedAt}");
        Console.WriteLine($"Status:         {item.ResultStatus}");
    }

}

void GetById()
{
    Console.Write("Enter ID to retrieve: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var result = client.GetByIdAsync(new TestResultThuNtnIdRequest { TestResultThuNtnid = id });

        if (result.TestResultThuNtnid == 0)
        {
            Console.WriteLine("❌ Record not found.");
        }
        else
        {
            Console.WriteLine("\n✅ Test Result Details:");
            Console.WriteLine("===========================================");
            Console.WriteLine($"ID:             {result.TestResultThuNtnid}");
            Console.WriteLine($"Order ID:       {result.OrderId}");
            Console.WriteLine($"Version:        {result.ResultVersion}");
            Console.WriteLine($"Summary:        {result.ResultSummary}");
            Console.WriteLine($"Detail:         {result.ResultDetail}");
            Console.WriteLine($"File URL:       {result.ResultFileUrl}");
            Console.WriteLine($"Issued By:      {result.IssuedBy}");
            Console.WriteLine($"Completed At:   {result.CompletedAt}");
            Console.WriteLine($"Created At:     {result.CreatedAt}");
            Console.WriteLine($"Status:         {result.ResultStatus}");
        }
    }
    else
    {
        Console.WriteLine("⚠️ Invalid ID input.");
    }
}


void Create()
{
    var item = InputTestResult();
    var response = client.CreateAsync(item);
    Console.WriteLine(response.Result > 0 ? "✅ Successfully created!" : "❌ Creation failed.");
}

void Update()
{
    Console.Write("Enter ID to update: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var item = InputTestResult();
        item.TestResultThuNtnid = id;
        var response = client.UpdateAsync(item);
        Console.WriteLine(response.Result > 0 ? "✅ Successfully updated!" : "❌ Update failed.");
    }
    else Console.WriteLine("⚠️ Invalid ID input.");
}

void Delete()
{
    Console.Write("Enter ID to delete: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var response = client.DeleteAsync(new TestResultThuNtnIdRequest { TestResultThuNtnid = id });
        Console.WriteLine(response.Result > 0 ? "✅ Successfully deleted!" : "❌ Deletion failed.");
    }
    else Console.WriteLine("⚠️ Invalid ID input.");
}

TestResultsThuNtn InputTestResult()
{
    var item = new TestResultsThuNtn();

    Console.Write("Order ID: ");
    item.OrderId = int.Parse(Console.ReadLine() ?? "0");

    Console.Write("Result version: ");
    item.ResultVersion = int.Parse(Console.ReadLine() ?? "1");

    Console.Write("Result summary: ");
    item.ResultSummary = Console.ReadLine() ?? "";

    Console.Write("Result detail: ");
    item.ResultDetail = Console.ReadLine() ?? "";

    Console.Write("Result file URL: ");
    item.ResultFileUrl = Console.ReadLine() ?? "";

    Console.Write("Issued by: ");
    item.IssuedBy = Console.ReadLine() ?? "";

    Console.Write("Completed at (yyyy-MM-ddTHH:mm:ss): ");
    item.CompletedAt = Console.ReadLine() ?? DateTime.Now.ToString("s");

    item.CreatedAt = DateTime.Now.ToString("s");
    item.ResultStatus = "Done";

    return item;
}
