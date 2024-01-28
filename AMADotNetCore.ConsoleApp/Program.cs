
// See https://aka.ms/new-console-template for more information


using AMADotNetCore.ConsoleApp.AdoDotNetExamples;
using AMADotNetCore.ConsoleApp.DapperExamples;
using AMADotNetCore.ConsoleApp.EFCoreExamples;
using AMADotNetCore.ConsoleApp.HttpClientExamples;
using AMADotNetCore.ConsoleApp.RestClientExamples;

Console.WriteLine("Hello, World!");

//Ctrl + .
//Ctrl + D
//alt + Up /Down
//F10 - summary
//F11 - detail
//Ctrl + K ,C

/*SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
sqlConnectionStringBuilder.DataSource = "DESKTOP-JR2QGGJ"; //server name
sqlConnectionStringBuilder.InitialCatalog = "AMADotNetCore"; //db name
sqlConnectionStringBuilder.UserID = "sa";
sqlConnectionStringBuilder.Password = "sa@123";*/


//SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
//{
//    DataSource = ".",
//    InitialCatalog = "AMADotNetCore",
//    UserID = "sa",
//    Password = "sa@123"
//};
//SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
//Console.WriteLine("Connection Opening..");
//connection.Open();
//Console.WriteLine("Connection Opened..");

//string query = @"SELECT [Blog_Id]
//      ,[Blog_Title]
//      ,[Blog_Author]
//      ,[Blog_Content]
//  FROM [dbo].[Tbl_Blog]";
//SqlCommand command = new SqlCommand(query, connection);
//SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);

//DataTable dt = new DataTable();
//sqlDataAdapter.Fill(dt);


//connection.Close();
//Console.WriteLine("Connection Closed..");

//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine("Id => " + dr["Blog_Id"]);
//    Console.WriteLine($"Title => {dr["Blog_Title"]}");
//    Console.WriteLine("Author => " + dr["Blog_Author"]);
//    Console.WriteLine("Content => " + dr["Blog_Content"]);
//}

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Run();
//Console.ReadKey();


//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();

//EFCoreExample efcoreExample = new EFCoreExample();
//efcoreExample.Run();

//HttpClientExample httpExample = new HttpClientExample();
//await httpExample.Run();

RestClientExample restClientExample = new RestClientExample();
await restClientExample.Run();