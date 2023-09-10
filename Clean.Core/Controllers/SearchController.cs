using System.Linq;
using Clean.Core.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Clean.Core.Controllers;

public class SearchController : RenderController
{
	private readonly IPublishedContentQuery _publishedContentQuery;

	private readonly IPublishedValueFallback _publishedValueFallback;
	
	public SearchController(ILogger<SearchController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor, IPublishedContentQuery publishedContentQuery, IPublishedValueFallback publishedValueFallback) : base(logger, compositeViewEngine, umbracoContextAccessor)
	{
		_publishedContentQuery = publishedContentQuery;
		_publishedValueFallback = publishedValueFallback;
	}

	public IActionResult Search([FromQuery(Name = "q")] string? searchQuery)
	{
		if (CurrentPage is not Search searchPage)
		{
			return NotFound();
		}
		
		var model = new SearchViewModel(searchPage, _publishedValueFallback)
		{
			SearchQuery = searchQuery
		};
		
		if (string.IsNullOrWhiteSpace(searchQuery))
		{
			return CurrentTemplate(model);
		}

		model.Results = _publishedContentQuery.Search(searchQuery).ToList();
		return CurrentTemplate(model);
	}
}