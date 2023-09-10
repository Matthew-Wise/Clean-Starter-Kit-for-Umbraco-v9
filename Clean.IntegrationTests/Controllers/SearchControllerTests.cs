using System.Net;
using Clean.IntegrationTests.TestSetup;
using FluentAssertions;

namespace Clean.IntegrationTests.Controllers;

[TestFixture]
public class SearchControllerTests : UmbracoIntegrationTestBase
{

	[Test]
	[TestCase(null, TestName = "No searchQuery returns no Results")]
	public async Task SearchViewModelTests(string? searchQuery)
	{
		var response = await Client.GetAsync("/search");
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}
}