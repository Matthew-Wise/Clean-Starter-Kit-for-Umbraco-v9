using Clean.Core.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

namespace Clean.Core.UnitTests.Controllers;

public class SearchControllerTests
{
	[Test]
	public void Setup()
	{
		var controller = new SearchController(
			new NullLogger<SearchController>()
			, Mock.Of<ICompositeViewEngine>()
			, Mock.Of<IUmbracoContextAccessor>()
			, Mock.Of<IPublishedContentQuery>()
			, Mock.Of<IPublishedValueFallback>());

		
		var  actionResult = controller.Search(null) as ViewResult;
		actionResult.Should().NotBeNull();
		actionResult.Model.Should().BeEquivalentTo(new
		{
			Results = new List<PublishedSearchResult>(),
			SearchQuery = (string?)null
		});
	}
}