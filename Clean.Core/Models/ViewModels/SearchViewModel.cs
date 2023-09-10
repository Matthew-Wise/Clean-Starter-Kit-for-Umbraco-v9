using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Clean.Core.Models.ViewModels;

public class SearchViewModel : Search
{
	public SearchViewModel(Search content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
	{
	}

	public List<PublishedSearchResult> Results { get; set; } = new();
	public string? SearchQuery { get; set; }
}