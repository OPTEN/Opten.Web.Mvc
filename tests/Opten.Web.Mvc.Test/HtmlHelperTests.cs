using NUnit.Framework;
using System.Web.Mvc;

namespace Opten.Web.Mvc.Test
{
	[TestFixture]
	public class HtmlHelperTests
	{
		
		[Test]
		public void Html_If()
		{
			HtmlHelper helper = new HtmlHelper(new ViewContext(), new ViewPage());
			
			Car car = null;
			
			Assert.That(MvcHtmlString.Empty, Is.EqualTo(helper.If(car != null, () => car.Wheels.ToString())));
		}

		private class Car
		{

			public int Wheels { get; set; }

		}

	}
}