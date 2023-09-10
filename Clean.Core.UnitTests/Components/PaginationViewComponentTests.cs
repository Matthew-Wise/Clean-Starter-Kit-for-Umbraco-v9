using AutoFixture.NUnit3;
using Clean.Core.Components;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Clean.Core.UnitTests.Components
{
	public class PaginationViewComponentTests
	{
		private PaginationViewComponent _sut = null!;

		[SetUp]
		public void Setup()
		{
			_sut = new();
		}

		[Test, AutoData]
		public void Default_Values_MapTo_ViewModel(int totalItems, string url)
		{
			var componentResult = _sut.Invoke(totalItems, url) as ViewViewComponentResult;
			componentResult.Should().NotBeNull();
			componentResult.ViewData.Model.Should().BeEquivalentTo(new
			{
				TotalItems = totalItems,
				Url = url,
				PageNumber = 1,
				PageSize = 10,
				PageNumbersEitherSide = 1
			});
		}
	}
}