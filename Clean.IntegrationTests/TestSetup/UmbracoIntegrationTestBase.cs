using Clean.Site;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.IntegrationTests.TestSetup;

[TestFixture]
public abstract class UmbracoIntegrationTestBase
{
	protected CleanWebApplicationFactory WebsiteFactory { get; private set; } = null!;
	protected AsyncServiceScope Scope { get; private set; }
	
	protected IServiceProvider ServiceProvider => Scope.ServiceProvider;

	protected virtual CleanWebApplicationFactory CreateApplicationFactory()
	{
		return new CleanWebApplicationFactory();
	}

	[SetUp]
	public virtual void Setup()
	{
		WebsiteFactory = CreateApplicationFactory();
		Scope = new AsyncServiceScope( WebsiteFactory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope());
    }

	//[TearDown]
	//public virtual void TearDown()
	//{
	//	Scope.DisposeIfDisposable();
	//	WebsiteFactory.Dispose();
	//}

	protected virtual HttpClient Client
		=> WebsiteFactory.CreateClient();
}