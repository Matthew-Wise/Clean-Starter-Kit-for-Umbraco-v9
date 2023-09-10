using Clean.Site;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Clean.IntegrationTests.TestSetup;

public class CleanWebApplicationFactory : WebApplicationFactory<Program>
{
	private const string InMemoryConnectionString = "Data Source=IntegrationTests;Mode=Memory;Cache=Shared";
	private readonly SqliteConnection _imConnection;
	
	public CleanWebApplicationFactory()
	{
		_imConnection = new SqliteConnection(InMemoryConnectionString);
		_imConnection.Open();
	}
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{		
		var projectDir = Directory.GetCurrentDirectory();
		var configPath = Path.Combine(projectDir, "appsettings.Tests.json");
		builder.ConfigureAppConfiguration(conf =>
		{
			conf.AddJsonFile(configPath, true);
			conf.AddInMemoryCollection(new KeyValuePair<string, string>[]
			{
				new("ConnectionStrings:umbracoDbDSN", InMemoryConnectionString),
				new("ConnectionStrings:umbracoDbDSN_ProviderName", "Microsoft.Data.Sqlite")
			}!);
		});
		
        builder.UseEnvironment(Environments.Development);
    }
	
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		// When this application factory is disposed, close the connection to the in-memory database
		//    This will destroy the in-memory database
		_imConnection.Close();
		_imConnection.Dispose();
	}
	
}